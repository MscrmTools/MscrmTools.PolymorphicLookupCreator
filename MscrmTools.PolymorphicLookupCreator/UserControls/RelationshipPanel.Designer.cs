
namespace MscrmTools.PolymorphicLookupCreator.UserControls
{
    partial class RelationshipPanel
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtSchemaName = new System.Windows.Forms.TextBox();
            this.lblSchemaName = new System.Windows.Forms.Label();
            this.cbbCascadeMergeBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeMergeBehavior = new System.Windows.Forms.Label();
            this.cbbCascadeDeleteBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeDeleteBehavior = new System.Windows.Forms.Label();
            this.cbbCascadeReparentBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeReparentBehavior = new System.Windows.Forms.Label();
            this.cbbCascadeUnshareBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeUnshareBehavior = new System.Windows.Forms.Label();
            this.cbbCascadeShareBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeShareBehavior = new System.Windows.Forms.Label();
            this.cbbCascadeAssignBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeAssignBehavior = new System.Windows.Forms.Label();
            this.cbbCascadeBehavior = new System.Windows.Forms.ComboBox();
            this.lblCascadeBehavior = new System.Windows.Forms.Label();
            this.nudDisplayOrder = new System.Windows.Forms.NumericUpDown();
            this.lblDisplayOrder = new System.Windows.Forms.Label();
            this.cbbDisplayZone = new System.Windows.Forms.ComboBox();
            this.lblDisplayZone = new System.Windows.Forms.Label();
            this.txtCustomLabel = new System.Windows.Forms.TextBox();
            this.lblCustomlabel = new System.Windows.Forms.Label();
            this.cbbDisplayBehavior = new System.Windows.Forms.ComboBox();
            this.lblDisplayBehavior = new System.Windows.Forms.Label();
            this.chkIsValidForAdvancedFind = new System.Windows.Forms.CheckBox();
            this.lblIsValidForAdvancedFind = new System.Windows.Forms.Label();
            this.txtEntity = new System.Windows.Forms.TextBox();
            this.lblTargetEntity = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.txtSchemaName);
            this.pnlMain.Controls.Add(this.lblSchemaName);
            this.pnlMain.Controls.Add(this.cbbCascadeMergeBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeMergeBehavior);
            this.pnlMain.Controls.Add(this.cbbCascadeDeleteBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeDeleteBehavior);
            this.pnlMain.Controls.Add(this.cbbCascadeReparentBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeReparentBehavior);
            this.pnlMain.Controls.Add(this.cbbCascadeUnshareBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeUnshareBehavior);
            this.pnlMain.Controls.Add(this.cbbCascadeShareBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeShareBehavior);
            this.pnlMain.Controls.Add(this.cbbCascadeAssignBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeAssignBehavior);
            this.pnlMain.Controls.Add(this.cbbCascadeBehavior);
            this.pnlMain.Controls.Add(this.lblCascadeBehavior);
            this.pnlMain.Controls.Add(this.nudDisplayOrder);
            this.pnlMain.Controls.Add(this.lblDisplayOrder);
            this.pnlMain.Controls.Add(this.cbbDisplayZone);
            this.pnlMain.Controls.Add(this.lblDisplayZone);
            this.pnlMain.Controls.Add(this.txtCustomLabel);
            this.pnlMain.Controls.Add(this.lblCustomlabel);
            this.pnlMain.Controls.Add(this.cbbDisplayBehavior);
            this.pnlMain.Controls.Add(this.lblDisplayBehavior);
            this.pnlMain.Controls.Add(this.chkIsValidForAdvancedFind);
            this.pnlMain.Controls.Add(this.lblIsValidForAdvancedFind);
            this.pnlMain.Controls.Add(this.txtEntity);
            this.pnlMain.Controls.Add(this.lblTargetEntity);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(574, 1077);
            this.pnlMain.TabIndex = 0;
            // 
            // txtSchemaName
            // 
            this.txtSchemaName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSchemaName.Location = new System.Drawing.Point(0, 1020);
            this.txtSchemaName.MaxLength = 100;
            this.txtSchemaName.Name = "txtSchemaName";
            this.txtSchemaName.Size = new System.Drawing.Size(574, 26);
            this.txtSchemaName.TabIndex = 53;
            // 
            // lblSchemaName
            // 
            this.lblSchemaName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSchemaName.Location = new System.Drawing.Point(0, 970);
            this.lblSchemaName.Name = "lblSchemaName";
            this.lblSchemaName.Size = new System.Drawing.Size(574, 50);
            this.lblSchemaName.TabIndex = 52;
            this.lblSchemaName.Text = "SchemaName";
            this.lblSchemaName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeMergeBehavior
            // 
            this.cbbCascadeMergeBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeMergeBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeMergeBehavior.FormattingEnabled = true;
            this.cbbCascadeMergeBehavior.Items.AddRange(new object[] {
            "Cascade",
            "Active",
            "Owner",
            "None"});
            this.cbbCascadeMergeBehavior.Location = new System.Drawing.Point(0, 942);
            this.cbbCascadeMergeBehavior.Name = "cbbCascadeMergeBehavior";
            this.cbbCascadeMergeBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeMergeBehavior.TabIndex = 51;
            // 
            // lblCascadeMergeBehavior
            // 
            this.lblCascadeMergeBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeMergeBehavior.Location = new System.Drawing.Point(0, 892);
            this.lblCascadeMergeBehavior.Name = "lblCascadeMergeBehavior";
            this.lblCascadeMergeBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeMergeBehavior.TabIndex = 50;
            this.lblCascadeMergeBehavior.Text = "Merge behavior";
            this.lblCascadeMergeBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeDeleteBehavior
            // 
            this.cbbCascadeDeleteBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeDeleteBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeDeleteBehavior.FormattingEnabled = true;
            this.cbbCascadeDeleteBehavior.Items.AddRange(new object[] {
            "All",
            "Remove link",
            "Restrict",
            "None"});
            this.cbbCascadeDeleteBehavior.Location = new System.Drawing.Point(0, 864);
            this.cbbCascadeDeleteBehavior.Name = "cbbCascadeDeleteBehavior";
            this.cbbCascadeDeleteBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeDeleteBehavior.TabIndex = 49;
            // 
            // lblCascadeDeleteBehavior
            // 
            this.lblCascadeDeleteBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeDeleteBehavior.Location = new System.Drawing.Point(0, 814);
            this.lblCascadeDeleteBehavior.Name = "lblCascadeDeleteBehavior";
            this.lblCascadeDeleteBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeDeleteBehavior.TabIndex = 48;
            this.lblCascadeDeleteBehavior.Text = "Delete behavior";
            this.lblCascadeDeleteBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeReparentBehavior
            // 
            this.cbbCascadeReparentBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeReparentBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeReparentBehavior.FormattingEnabled = true;
            this.cbbCascadeReparentBehavior.Items.AddRange(new object[] {
            "Cascade",
            "Active",
            "Owner",
            "None"});
            this.cbbCascadeReparentBehavior.Location = new System.Drawing.Point(0, 786);
            this.cbbCascadeReparentBehavior.Name = "cbbCascadeReparentBehavior";
            this.cbbCascadeReparentBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeReparentBehavior.TabIndex = 47;
            // 
            // lblCascadeReparentBehavior
            // 
            this.lblCascadeReparentBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeReparentBehavior.Location = new System.Drawing.Point(0, 736);
            this.lblCascadeReparentBehavior.Name = "lblCascadeReparentBehavior";
            this.lblCascadeReparentBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeReparentBehavior.TabIndex = 46;
            this.lblCascadeReparentBehavior.Text = "Reparent behavior";
            this.lblCascadeReparentBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeUnshareBehavior
            // 
            this.cbbCascadeUnshareBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeUnshareBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeUnshareBehavior.FormattingEnabled = true;
            this.cbbCascadeUnshareBehavior.Items.AddRange(new object[] {
            "Cascade",
            "Active",
            "Owner",
            "None"});
            this.cbbCascadeUnshareBehavior.Location = new System.Drawing.Point(0, 708);
            this.cbbCascadeUnshareBehavior.Name = "cbbCascadeUnshareBehavior";
            this.cbbCascadeUnshareBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeUnshareBehavior.TabIndex = 45;
            // 
            // lblCascadeUnshareBehavior
            // 
            this.lblCascadeUnshareBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeUnshareBehavior.Location = new System.Drawing.Point(0, 658);
            this.lblCascadeUnshareBehavior.Name = "lblCascadeUnshareBehavior";
            this.lblCascadeUnshareBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeUnshareBehavior.TabIndex = 44;
            this.lblCascadeUnshareBehavior.Text = "Unshare behavior";
            this.lblCascadeUnshareBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeShareBehavior
            // 
            this.cbbCascadeShareBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeShareBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeShareBehavior.FormattingEnabled = true;
            this.cbbCascadeShareBehavior.Items.AddRange(new object[] {
            "Cascade",
            "Active",
            "Owner",
            "None"});
            this.cbbCascadeShareBehavior.Location = new System.Drawing.Point(0, 630);
            this.cbbCascadeShareBehavior.Name = "cbbCascadeShareBehavior";
            this.cbbCascadeShareBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeShareBehavior.TabIndex = 43;
            // 
            // lblCascadeShareBehavior
            // 
            this.lblCascadeShareBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeShareBehavior.Location = new System.Drawing.Point(0, 580);
            this.lblCascadeShareBehavior.Name = "lblCascadeShareBehavior";
            this.lblCascadeShareBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeShareBehavior.TabIndex = 42;
            this.lblCascadeShareBehavior.Text = "Share behavior";
            this.lblCascadeShareBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeAssignBehavior
            // 
            this.cbbCascadeAssignBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeAssignBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeAssignBehavior.FormattingEnabled = true;
            this.cbbCascadeAssignBehavior.Items.AddRange(new object[] {
            "Cascade",
            "Active",
            "Owner",
            "None"});
            this.cbbCascadeAssignBehavior.Location = new System.Drawing.Point(0, 552);
            this.cbbCascadeAssignBehavior.Name = "cbbCascadeAssignBehavior";
            this.cbbCascadeAssignBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeAssignBehavior.TabIndex = 41;
            // 
            // lblCascadeAssignBehavior
            // 
            this.lblCascadeAssignBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeAssignBehavior.Location = new System.Drawing.Point(0, 502);
            this.lblCascadeAssignBehavior.Name = "lblCascadeAssignBehavior";
            this.lblCascadeAssignBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeAssignBehavior.TabIndex = 40;
            this.lblCascadeAssignBehavior.Text = "Assign behavior";
            this.lblCascadeAssignBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbCascadeBehavior
            // 
            this.cbbCascadeBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCascadeBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCascadeBehavior.FormattingEnabled = true;
            this.cbbCascadeBehavior.Items.AddRange(new object[] {
            "Parental",
            "Referential",
            "Referential, restrict delete",
            "Custom"});
            this.cbbCascadeBehavior.Location = new System.Drawing.Point(0, 474);
            this.cbbCascadeBehavior.Name = "cbbCascadeBehavior";
            this.cbbCascadeBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbCascadeBehavior.TabIndex = 39;
            // 
            // lblCascadeBehavior
            // 
            this.lblCascadeBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCascadeBehavior.Location = new System.Drawing.Point(0, 424);
            this.lblCascadeBehavior.Name = "lblCascadeBehavior";
            this.lblCascadeBehavior.Size = new System.Drawing.Size(574, 50);
            this.lblCascadeBehavior.TabIndex = 38;
            this.lblCascadeBehavior.Text = "Cascade behavior";
            this.lblCascadeBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudDisplayOrder
            // 
            this.nudDisplayOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.nudDisplayOrder.Location = new System.Drawing.Point(0, 398);
            this.nudDisplayOrder.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudDisplayOrder.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDisplayOrder.Name = "nudDisplayOrder";
            this.nudDisplayOrder.Size = new System.Drawing.Size(574, 26);
            this.nudDisplayOrder.TabIndex = 37;
            this.nudDisplayOrder.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // lblDisplayOrder
            // 
            this.lblDisplayOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisplayOrder.Location = new System.Drawing.Point(0, 357);
            this.lblDisplayOrder.Name = "lblDisplayOrder";
            this.lblDisplayOrder.Size = new System.Drawing.Size(574, 41);
            this.lblDisplayOrder.TabIndex = 36;
            this.lblDisplayOrder.Text = "Display order";
            this.lblDisplayOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbDisplayZone
            // 
            this.cbbDisplayZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbDisplayZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDisplayZone.FormattingEnabled = true;
            this.cbbDisplayZone.Items.AddRange(new object[] {
            "Details",
            "Sales",
            "Service",
            "Marketing"});
            this.cbbDisplayZone.Location = new System.Drawing.Point(0, 329);
            this.cbbDisplayZone.Name = "cbbDisplayZone";
            this.cbbDisplayZone.Size = new System.Drawing.Size(574, 28);
            this.cbbDisplayZone.TabIndex = 35;
            // 
            // lblDisplayZone
            // 
            this.lblDisplayZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisplayZone.Location = new System.Drawing.Point(0, 279);
            this.lblDisplayZone.Name = "lblDisplayZone";
            this.lblDisplayZone.Size = new System.Drawing.Size(574, 50);
            this.lblDisplayZone.TabIndex = 34;
            this.lblDisplayZone.Text = "Display zone";
            this.lblDisplayZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomLabel
            // 
            this.txtCustomLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCustomLabel.Location = new System.Drawing.Point(0, 253);
            this.txtCustomLabel.Name = "txtCustomLabel";
            this.txtCustomLabel.Size = new System.Drawing.Size(574, 26);
            this.txtCustomLabel.TabIndex = 33;
            this.txtCustomLabel.Visible = false;
            // 
            // lblCustomlabel
            // 
            this.lblCustomlabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomlabel.Location = new System.Drawing.Point(0, 212);
            this.lblCustomlabel.Name = "lblCustomlabel";
            this.lblCustomlabel.Size = new System.Drawing.Size(574, 41);
            this.lblCustomlabel.TabIndex = 32;
            this.lblCustomlabel.Text = "Custom label";
            this.lblCustomlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCustomlabel.Visible = false;
            // 
            // cbbDisplayBehavior
            // 
            this.cbbDisplayBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbDisplayBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDisplayBehavior.FormattingEnabled = true;
            this.cbbDisplayBehavior.Items.AddRange(new object[] {
            "Use plural name",
            "Custom label",
            "Do not display"});
            this.cbbDisplayBehavior.Location = new System.Drawing.Point(0, 184);
            this.cbbDisplayBehavior.Name = "cbbDisplayBehavior";
            this.cbbDisplayBehavior.Size = new System.Drawing.Size(574, 28);
            this.cbbDisplayBehavior.TabIndex = 31;
            // 
            // lblDisplayBehavior
            // 
            this.lblDisplayBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisplayBehavior.Location = new System.Drawing.Point(0, 143);
            this.lblDisplayBehavior.Name = "lblDisplayBehavior";
            this.lblDisplayBehavior.Size = new System.Drawing.Size(574, 41);
            this.lblDisplayBehavior.TabIndex = 30;
            this.lblDisplayBehavior.Text = "Display behavior";
            this.lblDisplayBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIsValidForAdvancedFind
            // 
            this.chkIsValidForAdvancedFind.Checked = true;
            this.chkIsValidForAdvancedFind.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsValidForAdvancedFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkIsValidForAdvancedFind.Location = new System.Drawing.Point(0, 108);
            this.chkIsValidForAdvancedFind.Name = "chkIsValidForAdvancedFind";
            this.chkIsValidForAdvancedFind.Size = new System.Drawing.Size(574, 35);
            this.chkIsValidForAdvancedFind.TabIndex = 29;
            this.chkIsValidForAdvancedFind.UseVisualStyleBackColor = true;
            // 
            // lblIsValidForAdvancedFind
            // 
            this.lblIsValidForAdvancedFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIsValidForAdvancedFind.Location = new System.Drawing.Point(0, 67);
            this.lblIsValidForAdvancedFind.Name = "lblIsValidForAdvancedFind";
            this.lblIsValidForAdvancedFind.Size = new System.Drawing.Size(574, 41);
            this.lblIsValidForAdvancedFind.TabIndex = 28;
            this.lblIsValidForAdvancedFind.Text = "Is Valid for Advanced Find";
            this.lblIsValidForAdvancedFind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEntity
            // 
            this.txtEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEntity.Location = new System.Drawing.Point(0, 41);
            this.txtEntity.Name = "txtEntity";
            this.txtEntity.ReadOnly = true;
            this.txtEntity.Size = new System.Drawing.Size(574, 26);
            this.txtEntity.TabIndex = 27;
            // 
            // lblTargetEntity
            // 
            this.lblTargetEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTargetEntity.Location = new System.Drawing.Point(0, 0);
            this.lblTargetEntity.Name = "lblTargetEntity";
            this.lblTargetEntity.Size = new System.Drawing.Size(574, 41);
            this.lblTargetEntity.TabIndex = 26;
            this.lblTargetEntity.Text = "Entity";
            this.lblTargetEntity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RelationshipPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "RelationshipPanel";
            this.Size = new System.Drawing.Size(574, 1077);
            this.Load += new System.EventHandler(this.RelationshipPanel_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ComboBox cbbCascadeMergeBehavior;
        private System.Windows.Forms.Label lblCascadeMergeBehavior;
        private System.Windows.Forms.ComboBox cbbCascadeDeleteBehavior;
        private System.Windows.Forms.Label lblCascadeDeleteBehavior;
        private System.Windows.Forms.ComboBox cbbCascadeReparentBehavior;
        private System.Windows.Forms.Label lblCascadeReparentBehavior;
        private System.Windows.Forms.ComboBox cbbCascadeUnshareBehavior;
        private System.Windows.Forms.Label lblCascadeUnshareBehavior;
        private System.Windows.Forms.ComboBox cbbCascadeShareBehavior;
        private System.Windows.Forms.Label lblCascadeShareBehavior;
        private System.Windows.Forms.ComboBox cbbCascadeAssignBehavior;
        private System.Windows.Forms.Label lblCascadeAssignBehavior;
        private System.Windows.Forms.ComboBox cbbCascadeBehavior;
        private System.Windows.Forms.Label lblCascadeBehavior;
        private System.Windows.Forms.NumericUpDown nudDisplayOrder;
        private System.Windows.Forms.Label lblDisplayOrder;
        private System.Windows.Forms.ComboBox cbbDisplayZone;
        private System.Windows.Forms.Label lblDisplayZone;
        private System.Windows.Forms.TextBox txtCustomLabel;
        private System.Windows.Forms.Label lblCustomlabel;
        private System.Windows.Forms.ComboBox cbbDisplayBehavior;
        private System.Windows.Forms.Label lblDisplayBehavior;
        private System.Windows.Forms.CheckBox chkIsValidForAdvancedFind;
        private System.Windows.Forms.Label lblIsValidForAdvancedFind;
        private System.Windows.Forms.TextBox txtEntity;
        private System.Windows.Forms.Label lblTargetEntity;
        private System.Windows.Forms.TextBox txtSchemaName;
        private System.Windows.Forms.Label lblSchemaName;
    }
}
