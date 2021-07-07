using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using MscrmTools.PolymorphicLookupCreator.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MscrmTools.PolymorphicLookupCreator
{
    public partial class PluginControl : PluginControlBase
    {
        private List<EntityInfo> metadata;
        private List<AppCode.SolutionInfo> solutions;

        public PluginControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            ResetUi();
        }

        private void cbbSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrefix.Text = $"{((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<AliasedValue>("pub.customizationprefix").Value}_";
        }

        private void PluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("Polymorphic Lookups are in preview. Use them with caution.", new Uri("https://powerapps.microsoft.com/en-us/blog/announcement-multi-table-lookups-are-now-available-as-a-preview/"));
        }

        private void ResetUi()
        {
            // Empty controls
            cbbSolutions.Items.Clear();
            cbbReferencingEntity.Items.Clear();
            lvReferencedEntities.Items.Clear();
            txtDisplayName.Text = string.Empty;
            txtPrefix.Text = string.Empty;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading solutions...",
                Work = (bw, evt) =>
                {
                    // Reload solutions
                    var sm = new SolutionManager(Service);
                    solutions = sm.Load();

                    bw.ReportProgress(0, "Loading metadata...");

                    // Reload metadata
                    var mm = new MetadataManager(Service);
                    metadata = mm.GetAvailableEntitiesForRelationship(ConnectionDetail);
                },
                PostWorkCallBack = (evt) =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured:

    {evt.Error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cbbSolutions.Items.AddRange(solutions.ToArray());

                        var a = metadata.Where(e => e.Metadata.CanBePrimaryEntityInRelationship.Value).Select(e => e.Metadata.SchemaName).ToArray();
                        cbbReferencingEntity.Items.AddRange(a);
                        lvReferencedEntities.Items.AddRange(metadata.Where(e => e.Metadata.CanBeRelatedEntityInRelationship.Value).Select(e => new ListViewItem(e.Metadata.SchemaName)
                        {
                            Text = e.Metadata.SchemaName,
                            SubItems =
                {
                   e.Metadata.SchemaName
                },
                            Tag = e
                        }).ToArray());
                    }
                },
                ProgressChanged = (evt) =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            }
            );
        }

        private void tsbCreate_Click(object sender, EventArgs e)
        {
            if (cbbSolutions.SelectedItem == null)
            {
                MessageBox.Show(this, @"Please select a solution", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbbReferencingEntity.SelectedItem == null)
            {
                MessageBox.Show(this, @"Please select a referencing entity", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvReferencedEntities.CheckedItems.Count < 2)
            {
                MessageBox.Show(this, @"Please select at least two referenced entities", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDisplayName.Text.Length == 0)
            {
                MessageBox.Show(this, @"Please define a display name for the Lookup column", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSchemaName.Text.Length == 0)
            {
                MessageBox.Show(this, @"Please define a schema name for the Lookup column", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var referencing = metadata.First(m => m.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString()).Metadata.LogicalName;
            var referenced = lvReferencedEntities.CheckedItems.Cast<ListViewItem>().Select(i => ((EntityInfo)i.Tag).Metadata.LogicalName).ToArray();
            var prefix = txtPrefix.Text;
            var display = txtDisplayName.Text;
            var schema = $"{txtPrefix.Text}{txtSchemaName.Text}";
            var solutionUniqueName = ((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<string>("uniquename");

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating Polymorphic Lookup",
                Work = (bw, evt) =>
                {
                    var mm = new MetadataManager(Service);
                    var attributeId = mm.CreatePolymorphicLookup(prefix, display, schema, referencing, referenced);

                    bw.ReportProgress(0, "Adding Lookup to solution...");
                    var sm = new SolutionManager(Service);
                    sm.AddLookupToSolution(attributeId, solutionUniqueName);
                },
                PostWorkCallBack = (evt) =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured when creating Polymorphic lookup column:

{evt.Error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, @"Polymorphic Lookup has been created and added to the solution!", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                },
                ProgressChanged = (evt) =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            });
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            string normalized = txtDisplayName.Text.Normalize(NormalizationForm.FormKD);
            Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage,
                                                    new EncoderReplacementFallback(""),
                                                    new DecoderReplacementFallback(""));
            byte[] bytes = removal.GetBytes(normalized);
            string temp = Encoding.ASCII.GetString(bytes);
            string newTemp = string.Empty;
            bool capitalize = true;
            foreach (var c in temp)
            {
                if (c != ' ')
                {
                    newTemp += capitalize ? c.ToString().ToUpper() : c.ToString();
                    capitalize = false;
                }
                else
                {
                    capitalize = true;
                }
            }

            txtSchemaName.Text = $"{newTemp}Id";
        }
    }
}