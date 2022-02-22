using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MscrmTools.PolymorphicLookupCreator.AppCode;
using MscrmTools.PolymorphicLookupCreator.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MscrmTools.PolymorphicLookupCreator
{
    public partial class PluginControl : PluginControlBase, IGitHubPlugin, IPayPalPlugin
    {
        private LookupAttributeMetadata currentAmd;
        private EntityMetadata currentEmd;
        private int currentSortColumn = -1;
        private MetadataManager manager;
        private List<EntityInfo> metadata;
        private List<ListViewItem> referencedTableItems;
        private List<AppCode.SolutionInfo> solutions;

        public PluginControl()
        {
            InitializeComponent();
        }

        public string DonationDescription => "Donation for Polymorphic Lookup Manager";

        public string EmailAccount => "tanguy92@hotmail.com";

        public string RepositoryName => "MscrmTools.PolymorphicLookupCreator";

        public string UserName => "MscrmTools";

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            ResetUi(null, null);
        }

        private void cbbReferencingAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbReferencingAttribute.SelectedIndex == 0)
            {
                foreach (var item in lvReferencedEntities.Items.Cast<ListViewItem>())
                {
                    item.Checked = false;
                    item.Tag = null;
                }

                txtSchemaName.Text = string.Empty;
                txtSchemaName.ReadOnly = false;
                txtDisplayName.TextChanged -= txtDisplayName_TextChanged;
                txtDisplayName.Text = string.Empty;
                txtDisplayName.ReadOnly = false;
                txtDisplayName.TextChanged += txtDisplayName_TextChanged;

                tsbCreate.Enabled = true;
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

                currentAmd = null;

                return;
            }
            if (cbbReferencingEntity.SelectedItem == null || cbbReferencingAttribute.SelectedItem == null) return;

            currentEmd = metadata.First(x => x.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString()).Metadata;
            currentAmd = (LookupAttributeMetadata)currentEmd.Attributes.First(x => x.SchemaName == cbbReferencingAttribute.SelectedItem.ToString());

            txtSchemaName.Text = string.Join("_", currentAmd.SchemaName.Split('_').Skip(1));
            txtPrefix.Text = currentAmd.SchemaName.Split('_')[0] + "_";
            txtDisplayName.Text = currentAmd.DisplayName?.UserLocalizedLabel?.Label;
            txtSchemaName.ReadOnly = true;
            txtDisplayName.ReadOnly = true;

            tsbCreate.Enabled = false;
            tsbEdit.Enabled = true;
            tsbDelete.Enabled = true;

            foreach (var item in lvReferencedEntities.Items.Cast<ListViewItem>())
            {
                item.Checked = currentAmd.Targets.Contains(item.Text.ToLower());
                if (!item.Checked) continue;

                var referencedEmd = metadata.First(x => x.Metadata.LogicalName == item.Text.ToLower()).Metadata;
                var omr = currentEmd.ManyToOneRelationships.FirstOrDefault(r => r.ReferencedEntity.ToLower() == item.Text.ToLower() && r.ReferencingAttribute.ToLower() == currentAmd?.SchemaName?.ToLower());
                if (omr != null)
                {
                    item.Tag = new RelationshipInfo(referencedEmd, currentEmd, omr, txtPrefix.Text);
                }
                else
                {
                    if (txtPrefix.Text.Length == 0 || txtSchemaName.Text.Length == 0)
                    {
                        MessageBox.Show(this, @"Please define the Lookup schema name before configuring new relationship", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    omr = new OneToManyRelationshipMetadata
                    {
                        ReferencedEntity = item.Text.ToLower(),
                        SchemaName = $"{txtPrefix.Text}{currentEmd.LogicalName}_{referencedEmd.LogicalName}_{txtPrefix.Text}{txtSchemaName.Text}",
                        IsValidForAdvancedFind = true
                    };
                    item.Tag = new RelationshipInfo(referencedEmd, currentEmd, omr, txtPrefix.Text) { IsNew = true };
                }
            }
        }

        private void cbbReferencingEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbReferencingEntity.SelectedItem == null) return;

            currentEmd = metadata.First(x => x.Metadata.SchemaName == cbbReferencingEntity.SelectedItem.ToString()).Metadata;

            cbbReferencingAttribute.Items.Clear();
            cbbReferencingAttribute.Items.Add("<Create new polymorphic lookup>");
            cbbReferencingAttribute.Items.AddRange(currentEmd.Attributes.Select(a => a.SchemaName).ToArray());
        }

        private void cbbSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrefix.Text = $"{((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<AliasedValue>("pub.customizationprefix").Value}_";
        }

        private void lvReferencedEntities_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (currentSortColumn == -1)
            {
                lvReferencedEntities.Sorting = SortOrder.Descending;
                currentSortColumn = e.Column;
            }
            else if (e.Column != currentSortColumn)
            {
                lvReferencedEntities.Sorting = SortOrder.Ascending;
                currentSortColumn = e.Column;
            }
            else
            {
                lvReferencedEntities.Sorting = lvReferencedEntities.Sorting == SortOrder.Descending ? SortOrder.Ascending : SortOrder.Descending;
            }
            lvReferencedEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvReferencedEntities.Sorting);
            lvReferencedEntities.Sort();
        }

        private void lvReferencedEntities_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!e.Item.Checked) return;

            var lvi = e.Item;
            var referencedEmd = metadata.First(x => x.Metadata.LogicalName == lvi.Text.ToString().ToLower()).Metadata;

            if (txtPrefix.Text.Length == 0 || txtSchemaName.Text.Length == 0)
            {
                MessageBox.Show(this, @"Please define the Lookup schema name before configuring new relationship", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var omr = currentEmd.ManyToOneRelationships.FirstOrDefault(r => r.ReferencedEntity?.ToLower() == lvi.Text.ToLower()
            && r.ReferencingAttribute.ToLower() == $"{txtPrefix.Text}{txtSchemaName.Text}".ToLower());
            if (omr != null)
            {
                lvi.Tag = new RelationshipInfo(referencedEmd, currentEmd, omr, txtPrefix.Text);
            }
            else
            {
                omr = new OneToManyRelationshipMetadata
                {
                    ReferencedEntity = lvi.Text.ToLower(),
                    SchemaName = $"{txtPrefix.Text}{currentEmd.LogicalName}_{referencedEmd.LogicalName}_{txtPrefix.Text}{txtSchemaName.Text}",
                    IsValidForAdvancedFind = true
                };
                lvi.Tag = new RelationshipInfo(referencedEmd, currentEmd, omr, txtPrefix.Text) { IsNew = true };
            }
        }

        private void lvReferencedEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvReferencedEntities.SelectedItems.Count == 0) return;

            var lvi = lvReferencedEntities.SelectedItems.Cast<ListViewItem>().First();
            var referencedEmd = metadata.First(x => x.Metadata.LogicalName == lvi.Text.ToString().ToLower()).Metadata;

            if (txtPrefix.Text.Length == 0 || txtSchemaName.Text.Length == 0)
            {
                MessageBox.Show(this, @"Please define the Lookup schema name before configuring new relationship", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvi.Tag == null)
            {
                var omr = currentEmd.ManyToOneRelationships.FirstOrDefault(r => r.ReferencedEntity?.ToLower() == lvi.Text.ToLower()
                && r.ReferencingAttribute.ToLower() == $"{txtPrefix.Text}{txtSchemaName.Text}".ToLower());
                if (omr != null)
                {
                    lvi.Tag = new RelationshipInfo(referencedEmd, currentEmd, omr, txtPrefix.Text);
                }
                else
                {
                    omr = new OneToManyRelationshipMetadata
                    {
                        ReferencedEntity = lvi.Text.ToLower(),
                        SchemaName = $"{txtPrefix.Text}{currentEmd.LogicalName}_{referencedEmd.LogicalName}_{txtPrefix.Text}{txtSchemaName.Text}",
                        IsValidForAdvancedFind = true
                    };
                    lvi.Tag = new RelationshipInfo(referencedEmd, currentEmd, omr, txtPrefix.Text) { IsNew = true };
                }
            }

            var ctrl = new RelationshipPanel((RelationshipInfo)lvi.Tag, manager.LanguageCode)
            {
                Dock = DockStyle.Fill
            };
            gbRelationship.Controls.Clear();
            gbRelationship.Controls.Add(ctrl);
        }

        private void PluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("Polymorphic Lookups are in preview.", new Uri("https://powerapps.microsoft.com/en-us/blog/announcement-multi-table-lookups-are-now-available-as-a-preview/"));
        }

        private void PluginControl_Resize(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Height >= 240)
            {
                tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, tableLayoutPanel1.Height - 240);
            }
        }

        private void ResetUi(string action, string arg, bool partial = false)
        {
            // Empty controls
            gbRelationship.Controls.Clear();

            if (!partial)
            {
                cbbSolutions.Items.Clear();
                cbbReferencingEntity.Items.Clear();
            }

            if (action != "UPDATE")
            {
                cbbReferencingAttribute.Items.Clear();
            }

            if (!partial)
            {
                lvReferencedEntities.Items.Clear();
                txtPrefix.Text = string.Empty;
                gbRelationship.Controls.Clear();
            }

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
                    manager = new MetadataManager(Service);
                    metadata = manager.GetAvailableEntitiesForRelationship(ConnectionDetail);
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
                        if (!partial)
                        {
                            cbbSolutions.Items.AddRange(solutions.ToArray());

                            var a = metadata.Where(e => e.Metadata.CanBePrimaryEntityInRelationship.Value).Select(e => e.Metadata.SchemaName).ToArray();
                            cbbReferencingEntity.Items.AddRange(a);

                            referencedTableItems = new List<ListViewItem>();
                            referencedTableItems.AddRange(metadata.Where(e => e.Metadata.CanBeRelatedEntityInRelationship.Value).Select(e => new ListViewItem(e.Metadata.SchemaName)
                            {
                                Text = e.Metadata.SchemaName,
                                SubItems =
                                {
                                    e.Metadata.SchemaName,
                                    e.Metadata.DisplayName?.UserLocalizedLabel?.Label ?? "N/A"
                                }
                            }));

                            lvReferencedEntities.Items.AddRange(referencedTableItems.ToArray());
                        }
                        else
                        {
                            if (action == "UPDATE" || action == "CREATE")
                            {
                                foreach (var item in lvReferencedEntities.Items.Cast<ListViewItem>())
                                {
                                    item.Tag = null;
                                }
                            }
                            else
                            {
                                foreach (var item in lvReferencedEntities.Items.Cast<ListViewItem>())
                                {
                                    item.Checked = false;
                                }
                            }

                            cbbReferencingEntity_SelectedIndexChanged(cbbReferencingEntity, new EventArgs());

                            if (action == "CREATE" || action == "UPDATE")
                            {
                                cbbReferencingAttribute.SelectedIndex = cbbReferencingAttribute.FindStringExact(arg);
                            }
                        }
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
            var referenced = lvReferencedEntities.CheckedItems.Cast<ListViewItem>().Select(i => i.Text.ToLower()).ToArray();
            var rels = lvReferencedEntities.CheckedItems.Cast<ListViewItem>().Select(i => (RelationshipInfo)i.Tag).ToArray();
            var prefix = txtPrefix.Text;
            var display = txtDisplayName.Text;
            var schema = $"{txtPrefix.Text}{txtSchemaName.Text}";
            var solutionUniqueName = ((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<string>("uniquename");

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating Polymorphic Lookup",
                Work = (bw, evt) =>
                {
                    manager.CreatePolymorphicLookup(prefix, display, schema, referencing, referenced, rels, solutionUniqueName);
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

                    ResetUi("CREATE", schema, true);
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
                    manager.DeleteAttribute(amd.LogicalName, emd.Metadata.LogicalName);
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
                        ResetUi("DELETE", null, true);
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
                .Select(x => x.Text.ToLower())
                .ToList();

            if (newTargets.Count == 0)
            {
                MessageBox.Show(this, @"Cannot remove all relationships from a polymorphic lookup. Select at least one referenced entity or delete the polymorphic lookup column", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toDeleteList = currentTargets.Except(newTargets).ToList();
            var toAddList = newTargets.Except(currentTargets).ToList();
            var toCreateList = lvReferencedEntities.CheckedItems.Cast<ListViewItem>()
                .Where(i => toAddList.Contains(i.Text.ToLower()) && i.Tag != null && ((RelationshipInfo)i.Tag).IsNew)
                .Select(i => ((RelationshipInfo)i.Tag).Relation)
                .ToList();
            var toUpdateList = lvReferencedEntities.CheckedItems.Cast<ListViewItem>()
                .Where(i => i.Tag != null && !((RelationshipInfo)i.Tag).IsNew && ((RelationshipInfo)i.Tag).IsUpdated)
                .Select(i => ((RelationshipInfo)i.Tag).Relation)
                .ToList();

            if (toDeleteList.Count == 0 && toAddList.Count == 0 && toUpdateList.Count == 0)
            {
                MessageBox.Show(this, @"No changes detected", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this, $"Do you conform you want to {(toAddList.Count == 0 ? "" : $"\nAdd relations to the following entities:\n- {string.Join("\n- ", toAddList)}")}{(toDeleteList.Count == 0 ? "" : $"\nRemove relations for the following entities:\n- {string.Join("\n- ", toDeleteList)}")}{(toUpdateList.Count == 0 ? "" : $"\nUpdate relations for the following entities:\n- {string.Join("\n- ", toUpdateList.Select(r => r.ReferencedEntity))}")}", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            var prefix = txtPrefix.Text;
            var solutionUniqueName = ((AppCode.SolutionInfo)cbbSolutions.SelectedItem).Solution.GetAttributeValue<string>("uniquename");

            WorkAsync(new WorkAsyncInfo
            {
                Message = null,
                Work = (bw, evt) =>
                {
                    if (toAddList.Count > 0)
                    {
                        bw.ReportProgress(0, "Adding relationship(s)...");
                        foreach (var toCreate in toCreateList)
                        {
                            manager.AddRelationship(prefix, amd.LogicalName, emd.Metadata, metadata.First(x => x.Metadata.LogicalName == toCreate.ReferencedEntity).Metadata, toCreate, solutionUniqueName);
                        }
                    }

                    if (toDeleteList.Count > 0)
                    {
                        bw.ReportProgress(0, "Removing relationship(s)...");
                        foreach (var toDelete in toDeleteList)
                        {
                            manager.DeleteRelationship(amd.LogicalName, emd.Metadata, metadata.First(x => x.Metadata.LogicalName == toDelete).Metadata.LogicalName);
                        }
                    }

                    if (toUpdateList.Count > 0)
                    {
                        bw.ReportProgress(0, "Updating relationship(s)...");
                        foreach (var toUpdate in toUpdateList)
                        {
                            manager.UpdateRelationship(toUpdate);
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
                        ResetUi("UPDATE", amd.SchemaName, true);
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

            txtSchemaName.Text = $"{newTemp}{(newTemp.EndsWith("Id") ? "" : "Id")}";
        }

        private void txtTableSearch_TextChanged(object sender, EventArgs e)
        {
            var text = txtTableSearch.Text.ToLower();
            var items = referencedTableItems.Where(i => text.Length == 0
                    || i.Text.ToLower().Contains(text)
                    || i.SubItems[1].Text.ToLower().Contains(text));

            lvReferencedEntities.Items.Clear();
            lvReferencedEntities.Items.AddRange(items.ToArray());
        }
    }
}