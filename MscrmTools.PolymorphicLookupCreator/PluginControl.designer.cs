
using System;

namespace MscrmTools.PolymorphicLookupCreator
{
    partial class PluginControl
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
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbCreate = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSolution = new System.Windows.Forms.Label();
            this.lblReferencingEntity = new System.Windows.Forms.Label();
            this.lblLookupDisplayName = new System.Windows.Forms.Label();
            this.lblLookupSchemaName = new System.Windows.Forms.Label();
            this.lblReferencedEntities = new System.Windows.Forms.Label();
            this.cbbReferencingEntity = new System.Windows.Forms.ComboBox();
            this.pnlReferencedEntities = new System.Windows.Forms.Panel();
            this.lvReferencedEntities = new System.Windows.Forms.ListView();
            this.chSchemaName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTableSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblReferencingAttribute = new System.Windows.Forms.Label();
            this.cbbReferencingAttribute = new System.Windows.Forms.ComboBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.pnlSchemaName = new System.Windows.Forms.Panel();
            this.txtSchemaName = new System.Windows.Forms.TextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.gbRelationship = new System.Windows.Forms.GroupBox();
            this.pnlSolutionPicker = new System.Windows.Forms.Panel();
            this.btnBrowseSolutions = new System.Windows.Forms.Button();
            this.txtSolution = new System.Windows.Forms.TextBox();
            this.toolStripMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlReferencedEntities.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSchemaName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pnlSolutionPicker.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCreate,
            this.tsbEdit,
            this.tsbDelete});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1526, 34);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "tsMain";
            // 
            // tsbCreate
            // 
            this.tsbCreate.Enabled = false;
            this.tsbCreate.Image = global::MscrmTools.PolymorphicLookupCreator.Properties.Resources.lightning_add;
            this.tsbCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreate.Name = "tsbCreate";
            this.tsbCreate.Size = new System.Drawing.Size(259, 29);
            this.tsbCreate.Text = "Create Polymorphic Lookup";
            this.tsbCreate.Click += new System.EventHandler(this.tsbCreate_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.Enabled = false;
            this.tsbEdit.Image = global::MscrmTools.PolymorphicLookupCreator.Properties.Resources.lightning_go;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(160, 29);
            this.tsbEdit.Text = "Apply Changes";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = global::MscrmTools.PolymorphicLookupCreator.Properties.Resources.lightning_delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(90, 29);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.Controls.Add(this.lblSolution, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblReferencingEntity, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblLookupDisplayName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblLookupSchemaName, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblReferencedEntities, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbbReferencingEntity, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlReferencedEntities, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblReferencingAttribute, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbbReferencingAttribute, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtDisplayName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pnlSchemaName, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.pnlSolutionPicker, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1063, 1004);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lblSolution
            // 
            this.lblSolution.AutoSize = true;
            this.lblSolution.Location = new System.Drawing.Point(3, 0);
            this.lblSolution.Name = "lblSolution";
            this.lblSolution.Size = new System.Drawing.Size(67, 20);
            this.lblSolution.TabIndex = 0;
            this.lblSolution.Text = "Solution";
            // 
            // lblReferencingEntity
            // 
            this.lblReferencingEntity.AutoSize = true;
            this.lblReferencingEntity.Location = new System.Drawing.Point(3, 40);
            this.lblReferencingEntity.Name = "lblReferencingEntity";
            this.lblReferencingEntity.Size = new System.Drawing.Size(139, 20);
            this.lblReferencingEntity.TabIndex = 1;
            this.lblReferencingEntity.Text = "Referencing Table";
            // 
            // lblLookupDisplayName
            // 
            this.lblLookupDisplayName.AutoSize = true;
            this.lblLookupDisplayName.Location = new System.Drawing.Point(3, 120);
            this.lblLookupDisplayName.Name = "lblLookupDisplayName";
            this.lblLookupDisplayName.Size = new System.Drawing.Size(161, 20);
            this.lblLookupDisplayName.TabIndex = 3;
            this.lblLookupDisplayName.Text = "Lookup Display name";
            // 
            // lblLookupSchemaName
            // 
            this.lblLookupSchemaName.AutoSize = true;
            this.lblLookupSchemaName.Location = new System.Drawing.Point(3, 160);
            this.lblLookupSchemaName.Name = "lblLookupSchemaName";
            this.lblLookupSchemaName.Size = new System.Drawing.Size(169, 20);
            this.lblLookupSchemaName.TabIndex = 4;
            this.lblLookupSchemaName.Text = "Lookup Schema name";
            // 
            // lblReferencedEntities
            // 
            this.lblReferencedEntities.AutoSize = true;
            this.lblReferencedEntities.Location = new System.Drawing.Point(3, 200);
            this.lblReferencedEntities.Name = "lblReferencedEntities";
            this.lblReferencedEntities.Size = new System.Drawing.Size(144, 20);
            this.lblReferencedEntities.TabIndex = 2;
            this.lblReferencedEntities.Text = "Referenced Tables";
            // 
            // cbbReferencingEntity
            // 
            this.cbbReferencingEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbReferencingEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbReferencingEntity.FormattingEnabled = true;
            this.cbbReferencingEntity.Location = new System.Drawing.Point(203, 43);
            this.cbbReferencingEntity.Name = "cbbReferencingEntity";
            this.cbbReferencingEntity.Size = new System.Drawing.Size(857, 28);
            this.cbbReferencingEntity.TabIndex = 7;
            this.cbbReferencingEntity.SelectedIndexChanged += new System.EventHandler(this.cbbReferencingEntity_SelectedIndexChanged);
            // 
            // pnlReferencedEntities
            // 
            this.pnlReferencedEntities.Controls.Add(this.lvReferencedEntities);
            this.pnlReferencedEntities.Controls.Add(this.panel1);
            this.pnlReferencedEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReferencedEntities.Location = new System.Drawing.Point(203, 203);
            this.pnlReferencedEntities.Name = "pnlReferencedEntities";
            this.pnlReferencedEntities.Size = new System.Drawing.Size(857, 798);
            this.pnlReferencedEntities.TabIndex = 14;
            // 
            // lvReferencedEntities
            // 
            this.lvReferencedEntities.CheckBoxes = true;
            this.lvReferencedEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSchemaName,
            this.chDisplayName});
            this.lvReferencedEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvReferencedEntities.FullRowSelect = true;
            this.lvReferencedEntities.HideSelection = false;
            this.lvReferencedEntities.Location = new System.Drawing.Point(0, 32);
            this.lvReferencedEntities.Name = "lvReferencedEntities";
            this.lvReferencedEntities.Size = new System.Drawing.Size(857, 766);
            this.lvReferencedEntities.TabIndex = 12;
            this.lvReferencedEntities.UseCompatibleStateImageBehavior = false;
            this.lvReferencedEntities.View = System.Windows.Forms.View.Details;
            this.lvReferencedEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvReferencedEntities_ColumnClick);
            this.lvReferencedEntities.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvReferencedEntities_ItemChecked);
            this.lvReferencedEntities.SelectedIndexChanged += new System.EventHandler(this.lvReferencedEntities_SelectedIndexChanged);
            // 
            // chSchemaName
            // 
            this.chSchemaName.Text = "Schema name";
            this.chSchemaName.Width = 300;
            // 
            // chDisplayName
            // 
            this.chDisplayName.Text = "Display name";
            this.chDisplayName.Width = 200;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTableSearch);
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 32);
            this.panel1.TabIndex = 0;
            // 
            // txtTableSearch
            // 
            this.txtTableSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTableSearch.Location = new System.Drawing.Point(86, 0);
            this.txtTableSearch.Name = "txtTableSearch";
            this.txtTableSearch.Size = new System.Drawing.Size(771, 26);
            this.txtTableSearch.TabIndex = 1;
            this.txtTableSearch.TextChanged += new System.EventHandler(this.txtTableSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Location = new System.Drawing.Point(0, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 32);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReferencingAttribute
            // 
            this.lblReferencingAttribute.AutoSize = true;
            this.lblReferencingAttribute.Location = new System.Drawing.Point(3, 80);
            this.lblReferencingAttribute.Name = "lblReferencingAttribute";
            this.lblReferencingAttribute.Size = new System.Drawing.Size(70, 20);
            this.lblReferencingAttribute.TabIndex = 12;
            this.lblReferencingAttribute.Text = "Attribute";
            // 
            // cbbReferencingAttribute
            // 
            this.cbbReferencingAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbReferencingAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbReferencingAttribute.FormattingEnabled = true;
            this.cbbReferencingAttribute.Location = new System.Drawing.Point(203, 83);
            this.cbbReferencingAttribute.Name = "cbbReferencingAttribute";
            this.cbbReferencingAttribute.Size = new System.Drawing.Size(857, 28);
            this.cbbReferencingAttribute.TabIndex = 13;
            this.cbbReferencingAttribute.SelectedIndexChanged += new System.EventHandler(this.cbbReferencingAttribute_SelectedIndexChanged);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDisplayName.Location = new System.Drawing.Point(203, 123);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(857, 26);
            this.txtDisplayName.TabIndex = 8;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // pnlSchemaName
            // 
            this.pnlSchemaName.Controls.Add(this.txtSchemaName);
            this.pnlSchemaName.Controls.Add(this.txtPrefix);
            this.pnlSchemaName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSchemaName.Location = new System.Drawing.Point(203, 163);
            this.pnlSchemaName.Name = "pnlSchemaName";
            this.pnlSchemaName.Size = new System.Drawing.Size(857, 34);
            this.pnlSchemaName.TabIndex = 11;
            // 
            // txtSchemaName
            // 
            this.txtSchemaName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchemaName.Location = new System.Drawing.Point(100, 0);
            this.txtSchemaName.Name = "txtSchemaName";
            this.txtSchemaName.Size = new System.Drawing.Size(757, 26);
            this.txtSchemaName.TabIndex = 1;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPrefix.Location = new System.Drawing.Point(0, 0);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.ReadOnly = true;
            this.txtPrefix.Size = new System.Drawing.Size(100, 26);
            this.txtPrefix.TabIndex = 0;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 34);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.gbRelationship);
            this.scMain.Size = new System.Drawing.Size(1526, 1004);
            this.scMain.SplitterDistance = 1063;
            this.scMain.TabIndex = 6;
            // 
            // gbRelationship
            // 
            this.gbRelationship.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRelationship.Location = new System.Drawing.Point(0, 0);
            this.gbRelationship.Name = "gbRelationship";
            this.gbRelationship.Size = new System.Drawing.Size(459, 1004);
            this.gbRelationship.TabIndex = 0;
            this.gbRelationship.TabStop = false;
            this.gbRelationship.Text = "Relationship info (Select a relationship to display)";
            // 
            // pnlSolutionPicker
            // 
            this.pnlSolutionPicker.Controls.Add(this.txtSolution);
            this.pnlSolutionPicker.Controls.Add(this.btnBrowseSolutions);
            this.pnlSolutionPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSolutionPicker.Location = new System.Drawing.Point(203, 3);
            this.pnlSolutionPicker.Name = "pnlSolutionPicker";
            this.pnlSolutionPicker.Size = new System.Drawing.Size(857, 34);
            this.pnlSolutionPicker.TabIndex = 15;
            // 
            // btnBrowseSolutions
            // 
            this.btnBrowseSolutions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseSolutions.Location = new System.Drawing.Point(810, 0);
            this.btnBrowseSolutions.Name = "btnBrowseSolutions";
            this.btnBrowseSolutions.Size = new System.Drawing.Size(47, 34);
            this.btnBrowseSolutions.TabIndex = 0;
            this.btnBrowseSolutions.Text = "...";
            this.btnBrowseSolutions.UseVisualStyleBackColor = true;
            this.btnBrowseSolutions.Click += new System.EventHandler(this.btnBrowseSolutions_Click);
            // 
            // txtSolution
            // 
            this.txtSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSolution.Location = new System.Drawing.Point(0, 0);
            this.txtSolution.Name = "txtSolution";
            this.txtSolution.ReadOnly = true;
            this.txtSolution.Size = new System.Drawing.Size(810, 26);
            this.txtSolution.TabIndex = 1;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1526, 1038);
            this.Load += new System.EventHandler(this.PluginControl_Load);
            this.Resize += new System.EventHandler(this.PluginControl_Resize);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlReferencedEntities.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSchemaName.ResumeLayout(false);
            this.pnlSchemaName.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.pnlSolutionPicker.ResumeLayout(false);
            this.pnlSolutionPicker.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblSolution;
        private System.Windows.Forms.Label lblReferencingEntity;
        private System.Windows.Forms.Label lblReferencedEntities;
        private System.Windows.Forms.Label lblLookupDisplayName;
        private System.Windows.Forms.Label lblLookupSchemaName;
        private System.Windows.Forms.ComboBox cbbReferencingEntity;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Panel pnlSchemaName;
        private System.Windows.Forms.TextBox txtSchemaName;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.ToolStripButton tsbCreate;
        private System.Windows.Forms.Label lblReferencingAttribute;
        private System.Windows.Forms.ComboBox cbbReferencingAttribute;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.GroupBox gbRelationship;
        private System.Windows.Forms.Panel pnlReferencedEntities;
        private System.Windows.Forms.ListView lvReferencedEntities;
        private System.Windows.Forms.ColumnHeader chSchemaName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTableSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ColumnHeader chDisplayName;
        private System.Windows.Forms.Panel pnlSolutionPicker;
        private System.Windows.Forms.TextBox txtSolution;
        private System.Windows.Forms.Button btnBrowseSolutions;
    }
}
