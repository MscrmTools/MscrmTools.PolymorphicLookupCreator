﻿using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
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

        private void cbbReferencingAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbReferencingAttribute.SelectedIndex == 0)
            {
                foreach (var item in lvReferencedEntities.Items.Cast<ListViewItem>())
                {
                    item.Checked = false;
                }

                txtSchemaName.Text = string.Empty;
                txtDisplayName.Text = string.Empty;
                txtSchemaName.ReadOnly = false;
                txtDisplayName.ReadOnly = false;

                tsbCreate.Enabled = true;
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

                return;
            }
            if (cbbReferencingEntity.SelectedItem == null || cbbReferencingAttribute.SelectedItem == null) return;

            var emd = metadata.First(x => x.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString());
            var amd = (LookupAttributeMetadata)emd.Metadata.Attributes.First(x => x.SchemaName == cbbReferencingAttribute.SelectedItem.ToString());

            txtSchemaName.Text = string.Join("_", amd.SchemaName.Split('_').Skip(1));
            txtPrefix.Text = amd.SchemaName.Split('_')[0];
            txtDisplayName.Text = amd.DisplayName?.UserLocalizedLabel?.Label;
            txtSchemaName.ReadOnly = true;
            txtDisplayName.ReadOnly = true;

            tsbCreate.Enabled = false;
            tsbEdit.Enabled = true;
            tsbDelete.Enabled = true;

            foreach (var item in lvReferencedEntities.Items.Cast<ListViewItem>())
            {
                item.Checked = amd.Targets.Contains(((EntityInfo)item.Tag).Metadata.LogicalName);
            }
        }

        private void cbbReferencingEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbReferencingEntity.SelectedItem == null) return;

            var emd = metadata.First(x => x.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString());

            cbbReferencingAttribute.Items.Clear();
            cbbReferencingAttribute.Items.Add("<Create new polymorphic lookup>");
            cbbReferencingAttribute.Items.AddRange(emd.Metadata.Attributes.Select(a => a.SchemaName).ToArray());
        }

        private void cbbSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrefix.Text = $"{((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<AliasedValue>("pub.customizationprefix").Value}_";
        }

        private void PluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("Polymorphic Lookups are in preview. Use them with caution.", new Uri("https://powerapps.microsoft.com/en-us/blog/announcement-multi-table-lookups-are-now-available-as-a-preview/"));
        }

        private void PluginControl_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, tableLayoutPanel1.Height - 240);
        }

        private void ResetUi()
        {
            // Empty controls
            cbbSolutions.Items.Clear();
            cbbReferencingEntity.Items.Clear();
            cbbReferencingAttribute.Items.Clear();
            lvReferencedEntities.Items.Clear();
            txtPrefix.Text = string.Empty;

            txtDisplayName.TextChanged -= txtDisplayName_TextChanged;
            txtDisplayName.Text = string.Empty;
            txtSchemaName.Text = string.Empty;
            txtDisplayName.TextChanged += txtDisplayName_TextChanged;

            tsbCreate.Enabled = false;
            tsbEdit.Enabled = false;
            tsbDelete.Enabled = false;

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
                    mm.CreatePolymorphicLookup(prefix, display, schema, referencing, referenced, solutionUniqueName);
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

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(this, @"Are you sure you want to delete this Lookup column?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }
            var emd = metadata.First(x => x.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString());
            var amd = (LookupAttributeMetadata)emd.Metadata.Attributes.First(x => x.SchemaName == cbbReferencingAttribute.SelectedItem.ToString());

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Deleting Lookup column...",
                Work = (bw, evt) =>
                {
                    var mm = new MetadataManager(Service);
                    mm.DeleteAttribute(amd.LogicalName, emd.Metadata.LogicalName);
                },
                PostWorkCallBack = (evt) =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured when deleting Lookup column:

{evt.Error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ResetUi();
                    }
                },
                ProgressChanged = (evt) =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            });
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (cbbReferencingEntity.SelectedItem == null || cbbReferencingAttribute.SelectedItem == null) return;

            var emd = metadata.First(x => x.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString());
            var amd = (LookupAttributeMetadata)emd.Metadata.Attributes.First(x => x.SchemaName == cbbReferencingAttribute.SelectedItem.ToString());

            var currentTargets = amd.Targets.ToList();

            var newTargets = lvReferencedEntities.CheckedItems.Cast<ListViewItem>()
                .Select(x => ((EntityInfo)x.Tag).Metadata.LogicalName)
                .ToList();

            if (newTargets.Count == 0)
            {
                MessageBox.Show(this, @"Cannot remove all relationships from a polymorphic lookup. Select at least one referenced entity or delete the polymorphic lookup column", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toDeleteList = currentTargets.Except(newTargets).ToList();
            var toAddList = newTargets.Except(currentTargets).ToList();

            if (toDeleteList.Count == 0 && toAddList.Count == 0)
            {
                MessageBox.Show(this, @"No changes detected", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this, $"Do you conform you want to {(toAddList.Count == 0 ? "" : $"\nAdd relations to the following entities:\n- {string.Join("\n- ", toAddList)}")}{(toDeleteList.Count == 0 ? "" : $"\nRemove relations for the following entities:\n- {string.Join("\n- ", toDeleteList)}")}", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            var prefix = txtPrefix.Text;
            var solutionUniqueName = ((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<string>("uniquename");

            WorkAsync(new WorkAsyncInfo
            {
                Message = null,
                Work = (bw, evt) =>
                {
                    var mm = new MetadataManager(Service);

                    if (toAddList.Count > 0)
                    {
                        bw.ReportProgress(0, "Adding relationship(s)...");
                        foreach (var toAdd in toAddList)
                        {
                            mm.AddRelationship(prefix, amd.LogicalName, emd.Metadata, metadata.First(x => x.Metadata.LogicalName == toAdd).Metadata, solutionUniqueName);
                        }
                    }

                    if (toDeleteList.Count > 0)
                    {
                        bw.ReportProgress(0, "Removing relationship(s)...");
                        foreach (var toDelete in toDeleteList)
                        {
                            mm.DeleteRelationship(amd.LogicalName, emd.Metadata, metadata.First(x => x.Metadata.LogicalName == toDelete).Metadata.LogicalName);
                        }
                    }
                },
                PostWorkCallBack = (evt) =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, $@"An error occured when editing Polymorphic lookup relationships:

{evt.Error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ResetUi();
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