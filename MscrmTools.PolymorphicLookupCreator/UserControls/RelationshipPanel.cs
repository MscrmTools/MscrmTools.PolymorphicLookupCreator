using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MscrmTools.PolymorphicLookupCreator.AppCode;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MscrmTools.PolymorphicLookupCreator.UserControls
{
    public partial class RelationshipPanel : UserControl
    {
        private readonly int _lcid;
        private readonly RelationshipInfo _ri;

        public RelationshipPanel(RelationshipInfo ri, int lcid)
        {
            _ri = ri;
            _lcid = lcid;

            InitializeComponent();

            foreach (var cbb in Controls.OfType<ComboBox>())
            {
                cbb.SelectedIndex = 0;
            }
        }

        private void cbbCascadeBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isCustom = cbbCascadeBehavior.SelectedIndex == 3;

            cbbCascadeAssignBehavior.Visible = isCustom;
            cbbCascadeDeleteBehavior.Visible = isCustom;
            cbbCascadeMergeBehavior.Visible = isCustom;
            cbbCascadeReparentBehavior.Visible = isCustom;
            cbbCascadeShareBehavior.Visible = isCustom;
            cbbCascadeUnshareBehavior.Visible = isCustom;

            lblCascadeAssignBehavior.Visible = isCustom;
            lblCascadeDeleteBehavior.Visible = isCustom;
            lblCascadeMergeBehavior.Visible = isCustom;
            lblCascadeReparentBehavior.Visible = isCustom;
            lblCascadeShareBehavior.Visible = isCustom;
            lblCascadeUnshareBehavior.Visible = isCustom;

            var cc = new CascadeConfiguration();
            switch (cbbCascadeBehavior.SelectedItem.ToString())
            {
                case "Parental":
                    cc.Assign = CascadeType.Cascade;
                    cc.Delete = CascadeType.Cascade;
                    cc.Merge = CascadeType.Cascade;
                    cc.Reparent = CascadeType.Cascade;
                    cc.RollupView = CascadeType.NoCascade;
                    cc.Share = CascadeType.Cascade;
                    cc.Unshare = CascadeType.Cascade;
                    break;

                case "Referential, restrict delete":
                    cc.Assign = CascadeType.NoCascade;
                    cc.Delete = CascadeType.Restrict;
                    cc.Merge = CascadeType.NoCascade;
                    cc.Reparent = CascadeType.NoCascade;
                    cc.RollupView = CascadeType.NoCascade;
                    cc.Share = CascadeType.NoCascade;
                    cc.Unshare = CascadeType.NoCascade;
                    break;

                case "Custom":
                    cc.RollupView = CascadeType.NoCascade;
                    cc.Assign = GetCascade(cbbCascadeAssignBehavior.SelectedItem.ToString());
                    cc.Share = GetCascade(cbbCascadeShareBehavior.SelectedItem.ToString());
                    cc.Unshare = GetCascade(cbbCascadeUnshareBehavior.SelectedItem.ToString());
                    cc.Reparent = GetCascade(cbbCascadeReparentBehavior.SelectedItem.ToString());
                    cc.Delete = GetCascade(cbbCascadeDeleteBehavior.SelectedItem.ToString());
                    cc.Merge = GetCascade(cbbCascadeMergeBehavior.SelectedItem.ToString());
                    break;

                default:
                    cc.Assign = CascadeType.NoCascade;
                    cc.Delete = CascadeType.RemoveLink;
                    cc.Merge = CascadeType.NoCascade;
                    cc.Reparent = CascadeType.NoCascade;
                    cc.RollupView = CascadeType.NoCascade;
                    cc.Share = CascadeType.NoCascade;
                    cc.Unshare = CascadeType.NoCascade;
                    break;
            }

            _ri.Relation.CascadeConfiguration = cc;
        }

        private void cbbCascadeBehaviors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (ComboBox)sender;

            if (_ri.Relation.CascadeConfiguration == null)
            {
                _ri.Relation.CascadeConfiguration = new CascadeConfiguration();
            }

            if (cbb == cbbCascadeAssignBehavior)
            {
                _ri.Relation.CascadeConfiguration.Assign = GetCascade(cbb.SelectedItem.ToString());
            }
            if (cbb == cbbCascadeDeleteBehavior)
            {
                _ri.Relation.CascadeConfiguration.Delete = GetCascade(cbb.SelectedItem.ToString());
            }
            if (cbb == cbbCascadeMergeBehavior)
            {
                _ri.Relation.CascadeConfiguration.Merge = GetCascade(cbb.SelectedItem.ToString());
            }
            if (cbb == cbbCascadeReparentBehavior)
            {
                _ri.Relation.CascadeConfiguration.Reparent = GetCascade(cbb.SelectedItem.ToString());
            }
            if (cbb == cbbCascadeShareBehavior)
            {
                _ri.Relation.CascadeConfiguration.Share = GetCascade(cbb.SelectedItem.ToString());
            }
            if (cbb == cbbCascadeUnshareBehavior)
            {
                _ri.Relation.CascadeConfiguration.Unshare = GetCascade(cbb.SelectedItem.ToString());
            }
        }

        private void cbbDisplayBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomLabel.Visible = cbbDisplayBehavior.SelectedIndex == 1;
            lblCustomlabel.Visible = cbbDisplayBehavior.SelectedIndex == 1;

            if (_ri.Relation.AssociatedMenuConfiguration == null)
            {
                _ri.Relation.AssociatedMenuConfiguration = new AssociatedMenuConfiguration();
            }

            _ri.Relation.AssociatedMenuConfiguration.Behavior = cbbDisplayBehavior.SelectedIndex == 0 ?
                AssociatedMenuBehavior.UseCollectionName : cbbDisplayBehavior.SelectedIndex == 1 ?
                AssociatedMenuBehavior.UseLabel : AssociatedMenuBehavior.DoNotDisplay;
        }

        private void cbbDisplayZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ri.Relation.AssociatedMenuConfiguration.Group = cbbDisplayZone.SelectedIndex == 0 ?
                AssociatedMenuGroup.Details : cbbDisplayZone.SelectedIndex == 1 ?
                AssociatedMenuGroup.Sales : cbbDisplayZone.SelectedIndex == 2 ?
                AssociatedMenuGroup.Service : AssociatedMenuGroup.Marketing;
        }

        private void chkIsValidForAdvancedFind_CheckedChanged(object sender, EventArgs e)
        {
            _ri.Relation.IsValidForAdvancedFind = chkIsValidForAdvancedFind.Checked;
        }

        private CascadeType? GetCascade(string value)
        {
            switch (value)
            {
                case "None":
                    return CascadeType.NoCascade;

                case "Active":
                    return CascadeType.Active;

                case "Owner":
                    return CascadeType.UserOwned;

                case "Restrict":
                    return CascadeType.Restrict;

                case "Remove link":
                    return CascadeType.RemoveLink;

                default:
                    return CascadeType.Cascade;
            }
        }

        private string GetCascadeText(CascadeType type, bool isDeleteBehavior = false)
        {
            switch (type)
            {
                case CascadeType.Active:
                    return "Active";

                case CascadeType.NoCascade:
                    return "None";

                case CascadeType.UserOwned:
                    return "Owner";

                case CascadeType.RemoveLink:
                    return "Remove link";

                case CascadeType.Restrict:
                    return "Restrict";

                default:
                    return isDeleteBehavior ? "All" : "Cascade";
            }
        }

        private void nudDisplayOrder_ValueChanged(object sender, EventArgs e)
        {
            if (_ri.Relation.AssociatedMenuConfiguration == null)
            {
                _ri.Relation.AssociatedMenuConfiguration = new AssociatedMenuConfiguration();
            }

            _ri.Relation.AssociatedMenuConfiguration.Order = Convert.ToInt32(nudDisplayOrder.Value);
        }

        private void RelationshipPanel_Load(object sender, EventArgs e)
        {
            txtEntity.Text = _ri.Relation.ReferencedEntity;
            chkIsValidForAdvancedFind.Checked = _ri.Relation.IsValidForAdvancedFind ?? true;

            switch (_ri.Relation.AssociatedMenuConfiguration?.Behavior)
            {
                case AssociatedMenuBehavior.UseLabel:
                    cbbDisplayBehavior.SelectedIndex = 1;
                    cbbDisplayBehavior_SelectedIndexChanged(cbbDisplayBehavior, new EventArgs());
                    txtCustomLabel.Text = _ri.Relation.AssociatedMenuConfiguration?.Label?.UserLocalizedLabel?.Label;
                    break;

                case AssociatedMenuBehavior.DoNotDisplay:
                    cbbDisplayBehavior.SelectedIndex = 2;
                    break;

                default:
                    cbbDisplayBehavior.SelectedIndex = 0;
                    break;
            }

            switch (_ri.Relation.AssociatedMenuConfiguration?.Group)
            {
                case AssociatedMenuGroup.Sales:
                    cbbDisplayZone.SelectedIndex = 1;
                    break;

                case AssociatedMenuGroup.Service:
                    cbbDisplayZone.SelectedIndex = 2;
                    break;

                case AssociatedMenuGroup.Marketing:
                    cbbDisplayZone.SelectedIndex = 3;
                    break;

                default:
                    cbbDisplayZone.SelectedIndex = 0;
                    break;
            }

            cbbCascadeAssignBehavior.SelectedItem = GetCascadeText(_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade);
            cbbCascadeShareBehavior.SelectedItem = GetCascadeText(_ri.Relation.CascadeConfiguration?.Share ?? CascadeType.Cascade);
            cbbCascadeUnshareBehavior.SelectedItem = GetCascadeText(_ri.Relation.CascadeConfiguration?.Unshare ?? CascadeType.Cascade);
            cbbCascadeReparentBehavior.SelectedItem = GetCascadeText(_ri.Relation.CascadeConfiguration?.Reparent ?? CascadeType.Cascade);
            cbbCascadeDeleteBehavior.SelectedItem = GetCascadeText(_ri.Relation.CascadeConfiguration?.Delete ?? CascadeType.Cascade, true);
            cbbCascadeMergeBehavior.SelectedItem = GetCascadeText(_ri.Relation.CascadeConfiguration?.Merge ?? CascadeType.Cascade);

            if ((_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade) == CascadeType.Cascade
                && (_ri.Relation.CascadeConfiguration?.Share ?? CascadeType.Cascade) == CascadeType.Cascade
                && (_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade) == CascadeType.Cascade
                && (_ri.Relation.CascadeConfiguration?.Unshare ?? CascadeType.Cascade) == CascadeType.Cascade
                && (_ri.Relation.CascadeConfiguration?.Reparent ?? CascadeType.Cascade) == CascadeType.Cascade
                && (_ri.Relation.CascadeConfiguration?.Delete ?? CascadeType.Cascade) == CascadeType.Cascade
                && (_ri.Relation.CascadeConfiguration?.Merge ?? CascadeType.Cascade) == CascadeType.Cascade)
            {
                cbbCascadeBehavior.SelectedItem = "Parental";
            }
            else if ((_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade) == CascadeType.NoCascade
               && (_ri.Relation.CascadeConfiguration?.Share ?? CascadeType.Cascade) == CascadeType.NoCascade
               && (_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade) == CascadeType.NoCascade
               && (_ri.Relation.CascadeConfiguration?.Unshare ?? CascadeType.Cascade) == CascadeType.NoCascade
               && (_ri.Relation.CascadeConfiguration?.Reparent ?? CascadeType.Cascade) == CascadeType.NoCascade
               && (_ri.Relation.CascadeConfiguration?.Delete ?? CascadeType.Cascade) == CascadeType.RemoveLink
               && (_ri.Relation.CascadeConfiguration?.Merge ?? CascadeType.Cascade) == CascadeType.Cascade)
            {
                cbbCascadeBehavior.SelectedItem = "Referential";
            }
            else if ((_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade) == CascadeType.NoCascade
                && (_ri.Relation.CascadeConfiguration?.Share ?? CascadeType.Cascade) == CascadeType.NoCascade
                && (_ri.Relation.CascadeConfiguration?.Assign ?? CascadeType.Cascade) == CascadeType.NoCascade
                && (_ri.Relation.CascadeConfiguration?.Unshare ?? CascadeType.Cascade) == CascadeType.NoCascade
                && (_ri.Relation.CascadeConfiguration?.Reparent ?? CascadeType.Cascade) == CascadeType.NoCascade
                && (_ri.Relation.CascadeConfiguration?.Delete ?? CascadeType.Cascade) == CascadeType.Restrict
                && (_ri.Relation.CascadeConfiguration?.Merge ?? CascadeType.Cascade) == CascadeType.Cascade)
            {
                cbbCascadeBehavior.SelectedItem = "Referential, restrict delete";
            }
            else
            {
                cbbCascadeBehavior.SelectedItem = "Custom";
            }

            cbbCascadeBehavior_SelectedIndexChanged(cbbCascadeBehavior, new EventArgs());

            nudDisplayOrder.Value = _ri.Relation.AssociatedMenuConfiguration?.Order ?? 10000;
            txtSchemaName.Text = _ri.Relation.SchemaName?.Length > 100 ? _ri.Relation.SchemaName?.Substring(0, 100) : _ri.Relation.SchemaName;

            if (!_ri.IsNew) txtSchemaName.ReadOnly = true;

            this.cbbCascadeDeleteBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehaviors_SelectedIndexChanged);
            this.cbbCascadeMergeBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehaviors_SelectedIndexChanged);
            this.txtSchemaName.TextChanged += new EventHandler(this.txtSchemaName_TextChanged);
            this.cbbCascadeReparentBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehaviors_SelectedIndexChanged);
            this.cbbCascadeUnshareBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehaviors_SelectedIndexChanged);
            this.cbbCascadeShareBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehaviors_SelectedIndexChanged);
            this.cbbCascadeAssignBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehaviors_SelectedIndexChanged);
            this.cbbCascadeBehavior.SelectedIndexChanged += new EventHandler(this.cbbCascadeBehavior_SelectedIndexChanged);
            this.nudDisplayOrder.ValueChanged += new EventHandler(this.nudDisplayOrder_ValueChanged);
            this.cbbDisplayZone.SelectedIndexChanged += new EventHandler(this.cbbDisplayZone_SelectedIndexChanged);
            this.txtCustomLabel.TextChanged += new EventHandler(this.txtCustomLabel_TextChanged);
            this.cbbDisplayBehavior.SelectedIndexChanged += new EventHandler(this.cbbDisplayBehavior_SelectedIndexChanged);
            this.chkIsValidForAdvancedFind.CheckedChanged += new EventHandler(this.chkIsValidForAdvancedFind_CheckedChanged);
        }

        private void txtCustomLabel_TextChanged(object sender, EventArgs e)
        {
            if (_ri.Relation.AssociatedMenuConfiguration == null)
            {
                _ri.Relation.AssociatedMenuConfiguration = new AssociatedMenuConfiguration();
            }

            if (_ri.Relation.AssociatedMenuConfiguration.Label == null)
            {
                _ri.Relation.AssociatedMenuConfiguration.Label = new Microsoft.Xrm.Sdk.Label();
            }

            var defaultLabel = _ri.Relation.AssociatedMenuConfiguration.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == _lcid);
            if (defaultLabel == null)
            {
                defaultLabel = new LocalizedLabel(txtCustomLabel.Text, _lcid);
                _ri.Relation.AssociatedMenuConfiguration.Label.LocalizedLabels.Add(defaultLabel);
            }
            else
            {
                defaultLabel.Label = txtCustomLabel.Text;
            }
        }

        private void txtSchemaName_TextChanged(object sender, EventArgs e)
        {
            _ri.Relation.SchemaName = txtSchemaName.Text;
        }
    }
}