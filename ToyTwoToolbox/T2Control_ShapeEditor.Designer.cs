namespace ToyTwoToolbox {
    partial class T2Control_ShapeEditor {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.t2TTabControl4 = new ToyTwoToolbox.T2TTabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.fieldShapeName = new ToyTwoToolbox.T2Control_EditableLabel();
            this.t2TTabControl5 = new ToyTwoToolbox.T2TTabControl();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.butRemovePrim = new System.Windows.Forms.Button();
            this.butAddPrim = new System.Windows.Forms.Button();
            this.numericPatchType = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericPatchMaterialID = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.comboPrimitive = new System.Windows.Forms.ComboBox();
            this.dgvShapeData = new ToyTwoToolbox.T2Control_DGV();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alpha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.U = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.V = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.replaceSelectedValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillSelectedWithRandomNumbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectInvertedSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllCellsInColumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.butSendToMultiMat = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboMaterial = new System.Windows.Forms.ComboBox();
            this.butNewShapeMaterial = new System.Windows.Forms.Button();
            this.butRemoveShapeMaterial = new System.Windows.Forms.Button();
            this.groupMaterialProperties = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.t2Control_TextureSelector1 = new ToyTwoToolbox.T2Control_TextureSelector();
            this.labelMaterialMetadataDesc = new System.Windows.Forms.Label();
            this.numericMaterialMetadata = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.butAmbColorPicker = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numericCharShapeID2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericCharShapeID = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.radioPatch = new System.Windows.Forms.RadioButton();
            this.radioPrim = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.t2TTabControl4.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.t2TTabControl5.SuspendLayout();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPatchType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPatchMaterialID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShapeData)).BeginInit();
            this.contextDGV.SuspendLayout();
            this.tabPage13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupMaterialProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaterialMetadata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCharShapeID2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCharShapeID)).BeginInit();
            this.SuspendLayout();
            // 
            // t2TTabControl4
            // 
            this.t2TTabControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.t2TTabControl4.ControlBox = false;
            this.t2TTabControl4.Controls.Add(this.tabPage12);
            this.t2TTabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t2TTabControl4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.t2TTabControl4.Location = new System.Drawing.Point(0, 0);
            this.t2TTabControl4.Name = "t2TTabControl4";
            this.t2TTabControl4.SelectedIndex = 0;
            this.t2TTabControl4.Size = new System.Drawing.Size(766, 344);
            this.t2TTabControl4.TabIndex = 4;
            // 
            // tabPage12
            // 
            this.tabPage12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabPage12.Controls.Add(this.fieldShapeName);
            this.tabPage12.Controls.Add(this.t2TTabControl5);
            this.tabPage12.Controls.Add(this.numericCharShapeID2);
            this.tabPage12.Controls.Add(this.label2);
            this.tabPage12.Controls.Add(this.label5);
            this.tabPage12.Controls.Add(this.numericCharShapeID);
            this.tabPage12.Controls.Add(this.label4);
            this.tabPage12.Controls.Add(this.radioPatch);
            this.tabPage12.Controls.Add(this.radioPrim);
            this.tabPage12.Controls.Add(this.label3);
            this.tabPage12.Location = new System.Drawing.Point(4, 25);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(758, 315);
            this.tabPage12.TabIndex = 3;
            this.tabPage12.Text = "Selected Shape";
            // 
            // fieldShapeName
            // 
            this.fieldShapeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldShapeName.BackColor = System.Drawing.Color.Transparent;
            this.fieldShapeName.labelColor = System.Drawing.Color.Empty;
            this.fieldShapeName.Location = new System.Drawing.Point(82, 9);
            this.fieldShapeName.MaxWidth = 0;
            this.fieldShapeName.Name = "fieldShapeName";
            this.fieldShapeName.Overflow = false;
            this.fieldShapeName.Size = new System.Drawing.Size(177, 20);
            this.fieldShapeName.TabIndex = 12;
            this.fieldShapeName.ReportTextUpdate += new ToyTwoToolbox.T2Control_EditableLabel.TextUpdatedEventHandler(this.fieldShapeName_ReportTextUpdate);
            // 
            // t2TTabControl5
            // 
            this.t2TTabControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t2TTabControl5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.t2TTabControl5.ControlBox = false;
            this.t2TTabControl5.Controls.Add(this.tabPage11);
            this.t2TTabControl5.Controls.Add(this.tabPage13);
            this.t2TTabControl5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.t2TTabControl5.Location = new System.Drawing.Point(2, 36);
            this.t2TTabControl5.Name = "t2TTabControl5";
            this.t2TTabControl5.SelectedIndex = 0;
            this.t2TTabControl5.Size = new System.Drawing.Size(756, 277);
            this.t2TTabControl5.TabIndex = 6;
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabPage11.Controls.Add(this.splitContainer7);
            this.tabPage11.Location = new System.Drawing.Point(4, 25);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(748, 248);
            this.tabPage11.TabIndex = 3;
            this.tabPage11.Text = "Shape Data";
            // 
            // splitContainer7
            // 
            this.splitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer7.IsSplitterFixed = true;
            this.splitContainer7.Location = new System.Drawing.Point(3, 3);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.butRemovePrim);
            this.splitContainer7.Panel1.Controls.Add(this.butAddPrim);
            this.splitContainer7.Panel1.Controls.Add(this.numericPatchType);
            this.splitContainer7.Panel1.Controls.Add(this.label1);
            this.splitContainer7.Panel1.Controls.Add(this.numericPatchMaterialID);
            this.splitContainer7.Panel1.Controls.Add(this.label21);
            this.splitContainer7.Panel1.Controls.Add(this.label20);
            this.splitContainer7.Panel1.Controls.Add(this.comboPrimitive);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.dgvShapeData);
            this.splitContainer7.Size = new System.Drawing.Size(742, 242);
            this.splitContainer7.SplitterDistance = 43;
            this.splitContainer7.TabIndex = 5;
            // 
            // butRemovePrim
            // 
            this.butRemovePrim.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.aclui_126;
            this.butRemovePrim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRemovePrim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butRemovePrim.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butRemovePrim.Location = new System.Drawing.Point(247, 8);
            this.butRemovePrim.Name = "butRemovePrim";
            this.butRemovePrim.Size = new System.Drawing.Size(23, 23);
            this.butRemovePrim.TabIndex = 16;
            this.butRemovePrim.UseVisualStyleBackColor = true;
            this.butRemovePrim.Click += new System.EventHandler(this.butRemovePrim_Click);
            // 
            // butAddPrim
            // 
            this.butAddPrim.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.Default;
            this.butAddPrim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butAddPrim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAddPrim.Location = new System.Drawing.Point(218, 8);
            this.butAddPrim.Name = "butAddPrim";
            this.butAddPrim.Size = new System.Drawing.Size(23, 23);
            this.butAddPrim.TabIndex = 15;
            this.butAddPrim.UseVisualStyleBackColor = true;
            this.butAddPrim.Click += new System.EventHandler(this.butAddPrim_Click);
            // 
            // numericPatchType
            // 
            this.numericPatchType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericPatchType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.numericPatchType.Location = new System.Drawing.Point(471, 11);
            this.numericPatchType.Name = "numericPatchType";
            this.numericPatchType.Size = new System.Drawing.Size(52, 20);
            this.numericPatchType.TabIndex = 10;
            this.numericPatchType.ValueChanged += new System.EventHandler(this.numericPatchType_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Prim Type:";
            // 
            // numericPatchMaterialID
            // 
            this.numericPatchMaterialID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericPatchMaterialID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.numericPatchMaterialID.Location = new System.Drawing.Point(338, 11);
            this.numericPatchMaterialID.Name = "numericPatchMaterialID";
            this.numericPatchMaterialID.Size = new System.Drawing.Size(52, 20);
            this.numericPatchMaterialID.TabIndex = 8;
            this.numericPatchMaterialID.ValueChanged += new System.EventHandler(this.numericPatchMaterialID_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(275, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 5;
            this.label21.Text = "Material ID:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(23, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Primitive:";
            // 
            // comboPrimitive
            // 
            this.comboPrimitive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboPrimitive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPrimitive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboPrimitive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboPrimitive.FormattingEnabled = true;
            this.comboPrimitive.Location = new System.Drawing.Point(79, 10);
            this.comboPrimitive.Name = "comboPrimitive";
            this.comboPrimitive.Size = new System.Drawing.Size(133, 21);
            this.comboPrimitive.TabIndex = 4;
            this.comboPrimitive.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboPrimitive.ParentChanged += new System.EventHandler(this.comboPrimitive_ParentChanged);
            // 
            // dgvShapeData
            // 
            this.dgvShapeData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShapeData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dgvShapeData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvShapeData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShapeData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShapeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShapeData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.X,
            this.Y,
            this.Z,
            this.UX,
            this.UY,
            this.UZ,
            this.Alpha,
            this.VColor,
            this.U,
            this.V});
            this.dgvShapeData.ContextMenuStrip = this.contextDGV;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShapeData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvShapeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShapeData.EnableHeadersVisualStyles = false;
            this.dgvShapeData.GridColor = System.Drawing.Color.DimGray;
            this.dgvShapeData.ignoreCellValueChanged = true;
            this.dgvShapeData.Location = new System.Drawing.Point(0, 0);
            this.dgvShapeData.Name = "dgvShapeData";
            this.dgvShapeData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShapeData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvShapeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvShapeData.Size = new System.Drawing.Size(740, 193);
            this.dgvShapeData.TabIndex = 0;
            this.dgvShapeData.Visible = false;
            this.dgvShapeData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvShapeData_CellBeginEdit);
            this.dgvShapeData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShapeData_CellValueChanged);
            this.dgvShapeData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShapeData_CellValueChanged);
            this.dgvShapeData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShapeData_CellValueChanged);
            this.dgvShapeData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvShapeData_RowsAdded);
            this.dgvShapeData.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvShapeData_RowsRemoved);
            this.dgvShapeData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvShapeData_MouseUp);
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Z
            // 
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UX
            // 
            this.UX.HeaderText = "UX";
            this.UX.Name = "UX";
            this.UX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UY
            // 
            this.UY.HeaderText = "UY";
            this.UY.Name = "UY";
            this.UY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UZ
            // 
            this.UZ.HeaderText = "UZ";
            this.UZ.Name = "UZ";
            this.UZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Alpha
            // 
            this.Alpha.HeaderText = "Alpha";
            this.Alpha.Name = "Alpha";
            this.Alpha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VColor
            // 
            this.VColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VColor.HeaderText = "Color";
            this.VColor.Name = "VColor";
            this.VColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // U
            // 
            this.U.HeaderText = "U";
            this.U.Name = "U";
            this.U.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // V
            // 
            this.V.HeaderText = "V";
            this.V.Name = "V";
            this.V.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextDGV
            // 
            this.contextDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceSelectedValuesToolStripMenuItem,
            this.fillSelectedWithRandomNumbersToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.selectInvertedSelectionToolStripMenuItem,
            this.selectAllCellsToolStripMenuItem,
            this.selectAllCellsInColumToolStripMenuItem});
            this.contextDGV.Name = "contextDGV";
            this.contextDGV.Size = new System.Drawing.Size(257, 164);
            this.contextDGV.Opening += new System.ComponentModel.CancelEventHandler(this.contextDGV_Opening);
            // 
            // replaceSelectedValuesToolStripMenuItem
            // 
            this.replaceSelectedValuesToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources._126_Edit_16x16_72;
            this.replaceSelectedValuesToolStripMenuItem.Name = "replaceSelectedValuesToolStripMenuItem";
            this.replaceSelectedValuesToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.replaceSelectedValuesToolStripMenuItem.Text = "&Replace selected values...";
            this.replaceSelectedValuesToolStripMenuItem.Click += new System.EventHandler(this.replaceSelectedValuesToolStripMenuItem_Click);
            // 
            // fillSelectedWithRandomNumbersToolStripMenuItem
            // 
            this.fillSelectedWithRandomNumbersToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.eicn_dgm_grandom;
            this.fillSelectedWithRandomNumbersToolStripMenuItem.Name = "fillSelectedWithRandomNumbersToolStripMenuItem";
            this.fillSelectedWithRandomNumbersToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.fillSelectedWithRandomNumbersToolStripMenuItem.Text = "&Fill selected with random numbers";
            this.fillSelectedWithRandomNumbersToolStripMenuItem.Click += new System.EventHandler(this.fillSelectedWithRandomNumbersToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.CopyHS;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.Paste;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(253, 6);
            // 
            // selectInvertedSelectionToolStripMenuItem
            // 
            this.selectInvertedSelectionToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.eicn_dgv_selinv;
            this.selectInvertedSelectionToolStripMenuItem.Name = "selectInvertedSelectionToolStripMenuItem";
            this.selectInvertedSelectionToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.selectInvertedSelectionToolStripMenuItem.Text = "Select inverted selection";
            this.selectInvertedSelectionToolStripMenuItem.Click += new System.EventHandler(this.selectInvertedSelectionToolStripMenuItem_Click);
            // 
            // selectAllCellsToolStripMenuItem
            // 
            this.selectAllCellsToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.eicn_dgv_selall;
            this.selectAllCellsToolStripMenuItem.Name = "selectAllCellsToolStripMenuItem";
            this.selectAllCellsToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.selectAllCellsToolStripMenuItem.Text = "Select all cells";
            this.selectAllCellsToolStripMenuItem.Click += new System.EventHandler(this.selectAllCellsToolStripMenuItem_Click);
            // 
            // selectAllCellsInColumToolStripMenuItem
            // 
            this.selectAllCellsInColumToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.eicn_dgv_selcol;
            this.selectAllCellsInColumToolStripMenuItem.Name = "selectAllCellsInColumToolStripMenuItem";
            this.selectAllCellsInColumToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.selectAllCellsInColumToolStripMenuItem.Text = "Select all cells in colum";
            this.selectAllCellsInColumToolStripMenuItem.Click += new System.EventHandler(this.selectAllCellsInColumToolStripMenuItem_Click);
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabPage13.Controls.Add(this.splitContainer1);
            this.tabPage13.Location = new System.Drawing.Point(4, 25);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(748, 248);
            this.tabPage13.TabIndex = 4;
            this.tabPage13.Text = "Materials";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.butSendToMultiMat);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.comboMaterial);
            this.splitContainer1.Panel1.Controls.Add(this.butNewShapeMaterial);
            this.splitContainer1.Panel1.Controls.Add(this.butRemoveShapeMaterial);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupMaterialProperties);
            this.splitContainer1.Size = new System.Drawing.Size(742, 242);
            this.splitContainer1.SplitterDistance = 43;
            this.splitContainer1.TabIndex = 12;
            // 
            // butSendToMultiMat
            // 
            this.butSendToMultiMat.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.buzzUps;
            this.butSendToMultiMat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSendToMultiMat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSendToMultiMat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butSendToMultiMat.Location = new System.Drawing.Point(276, 8);
            this.butSendToMultiMat.Name = "butSendToMultiMat";
            this.butSendToMultiMat.Size = new System.Drawing.Size(23, 23);
            this.butSendToMultiMat.TabIndex = 16;
            this.butSendToMultiMat.UseVisualStyleBackColor = true;
            this.butSendToMultiMat.Click += new System.EventHandler(this.butSendToMultiMat_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Material Slot:";
            // 
            // comboMaterial
            // 
            this.comboMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboMaterial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboMaterial.FormattingEnabled = true;
            this.comboMaterial.Location = new System.Drawing.Point(79, 10);
            this.comboMaterial.Name = "comboMaterial";
            this.comboMaterial.Size = new System.Drawing.Size(133, 21);
            this.comboMaterial.TabIndex = 15;
            this.comboMaterial.SelectedIndexChanged += new System.EventHandler(this.comboMaterial_SelectedIndexChanged);
            // 
            // butNewShapeMaterial
            // 
            this.butNewShapeMaterial.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.Default;
            this.butNewShapeMaterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butNewShapeMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butNewShapeMaterial.Location = new System.Drawing.Point(218, 8);
            this.butNewShapeMaterial.Name = "butNewShapeMaterial";
            this.butNewShapeMaterial.Size = new System.Drawing.Size(23, 23);
            this.butNewShapeMaterial.TabIndex = 13;
            this.butNewShapeMaterial.UseVisualStyleBackColor = true;
            this.butNewShapeMaterial.Click += new System.EventHandler(this.butNewShapeMaterial_Click);
            // 
            // butRemoveShapeMaterial
            // 
            this.butRemoveShapeMaterial.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.aclui_126;
            this.butRemoveShapeMaterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRemoveShapeMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butRemoveShapeMaterial.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butRemoveShapeMaterial.Location = new System.Drawing.Point(247, 8);
            this.butRemoveShapeMaterial.Name = "butRemoveShapeMaterial";
            this.butRemoveShapeMaterial.Size = new System.Drawing.Size(23, 23);
            this.butRemoveShapeMaterial.TabIndex = 14;
            this.butRemoveShapeMaterial.UseVisualStyleBackColor = true;
            // 
            // groupMaterialProperties
            // 
            this.groupMaterialProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMaterialProperties.Controls.Add(this.label8);
            this.groupMaterialProperties.Controls.Add(this.t2Control_TextureSelector1);
            this.groupMaterialProperties.Controls.Add(this.labelMaterialMetadataDesc);
            this.groupMaterialProperties.Controls.Add(this.numericMaterialMetadata);
            this.groupMaterialProperties.Controls.Add(this.label9);
            this.groupMaterialProperties.Controls.Add(this.butAmbColorPicker);
            this.groupMaterialProperties.Controls.Add(this.label7);
            this.groupMaterialProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupMaterialProperties.Location = new System.Drawing.Point(8, 3);
            this.groupMaterialProperties.Name = "groupMaterialProperties";
            this.groupMaterialProperties.Size = new System.Drawing.Size(729, 187);
            this.groupMaterialProperties.TabIndex = 6;
            this.groupMaterialProperties.TabStop = false;
            this.groupMaterialProperties.Text = "Material properties";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Material Texture:";
            // 
            // t2Control_TextureSelector1
            // 
            this.t2Control_TextureSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t2Control_TextureSelector1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.t2Control_TextureSelector1.ForeColor = System.Drawing.Color.White;
            this.t2Control_TextureSelector1.Location = new System.Drawing.Point(6, 32);
            this.t2Control_TextureSelector1.Name = "t2Control_TextureSelector1";
            this.t2Control_TextureSelector1.SelectedIndex = -1;
            this.t2Control_TextureSelector1.Size = new System.Drawing.Size(215, 149);
            this.t2Control_TextureSelector1.TabIndex = 10;
            this.t2Control_TextureSelector1.SelectedIndexChanged += new ToyTwoToolbox.T2Control_TextureSelector.SelectedIndexChangedEventHandler(this.t2Control_TextureSelector1_SelectedIndexChanged);
            // 
            // labelMaterialMetadataDesc
            // 
            this.labelMaterialMetadataDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaterialMetadataDesc.AutoSize = true;
            this.labelMaterialMetadataDesc.Location = new System.Drawing.Point(440, 51);
            this.labelMaterialMetadataDesc.Name = "labelMaterialMetadataDesc";
            this.labelMaterialMetadataDesc.Size = new System.Drawing.Size(124, 13);
            this.labelMaterialMetadataDesc.TabIndex = 9;
            this.labelMaterialMetadataDesc.Text = " - Unknown Metadata ID";
            // 
            // numericMaterialMetadata
            // 
            this.numericMaterialMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMaterialMetadata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericMaterialMetadata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.numericMaterialMetadata.Location = new System.Drawing.Point(349, 48);
            this.numericMaterialMetadata.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericMaterialMetadata.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericMaterialMetadata.Name = "numericMaterialMetadata";
            this.numericMaterialMetadata.Size = new System.Drawing.Size(85, 20);
            this.numericMaterialMetadata.TabIndex = 8;
            this.numericMaterialMetadata.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericMaterialMetadata.ValueChanged += new System.EventHandler(this.numericMaterialMetadata_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(237, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Material Metadata:";
            // 
            // butAmbColorPicker
            // 
            this.butAmbColorPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAmbColorPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAmbColorPicker.Location = new System.Drawing.Point(349, 19);
            this.butAmbColorPicker.Name = "butAmbColorPicker";
            this.butAmbColorPicker.Size = new System.Drawing.Size(44, 23);
            this.butAmbColorPicker.TabIndex = 4;
            this.butAmbColorPicker.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Material AMB Color:";
            // 
            // numericCharShapeID2
            // 
            this.numericCharShapeID2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericCharShapeID2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericCharShapeID2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.numericCharShapeID2.Location = new System.Drawing.Point(458, 10);
            this.numericCharShapeID2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericCharShapeID2.Name = "numericCharShapeID2";
            this.numericCharShapeID2.Size = new System.Drawing.Size(55, 20);
            this.numericCharShapeID2.TabIndex = 5;
            this.numericCharShapeID2.ValueChanged += new System.EventHandler(this.numericCharShapeID2_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(526, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Prim Format:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(388, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Shape ID 2:";
            // 
            // numericCharShapeID
            // 
            this.numericCharShapeID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericCharShapeID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericCharShapeID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.numericCharShapeID.Location = new System.Drawing.Point(326, 10);
            this.numericCharShapeID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericCharShapeID.Name = "numericCharShapeID";
            this.numericCharShapeID.Size = new System.Drawing.Size(55, 20);
            this.numericCharShapeID.TabIndex = 3;
            this.numericCharShapeID.ValueChanged += new System.EventHandler(this.numericCharShapeID_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Shape ID:";
            // 
            // radioPatch
            // 
            this.radioPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioPatch.AutoSize = true;
            this.radioPatch.Location = new System.Drawing.Point(648, 10);
            this.radioPatch.Name = "radioPatch";
            this.radioPatch.Size = new System.Drawing.Size(53, 17);
            this.radioPatch.TabIndex = 7;
            this.radioPatch.Text = "Patch";
            this.radioPatch.UseVisualStyleBackColor = true;
            this.radioPatch.Click += new System.EventHandler(this.radioPatch_Click);
            // 
            // radioPrim
            // 
            this.radioPrim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioPrim.AutoSize = true;
            this.radioPrim.Checked = true;
            this.radioPrim.Location = new System.Drawing.Point(597, 10);
            this.radioPrim.Name = "radioPrim";
            this.radioPrim.Size = new System.Drawing.Size(45, 17);
            this.radioPrim.TabIndex = 6;
            this.radioPrim.TabStop = true;
            this.radioPrim.Text = "Prim";
            this.radioPrim.UseVisualStyleBackColor = true;
            this.radioPrim.Click += new System.EventHandler(this.radioPrim_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Shape name:";
            // 
            // T2Control_ShapeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.t2TTabControl4);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Name = "T2Control_ShapeEditor";
            this.Size = new System.Drawing.Size(766, 344);
            this.t2TTabControl4.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.t2TTabControl5.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel1.PerformLayout();
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericPatchType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPatchMaterialID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShapeData)).EndInit();
            this.contextDGV.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupMaterialProperties.ResumeLayout(false);
            this.groupMaterialProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaterialMetadata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCharShapeID2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCharShapeID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private T2TTabControl t2TTabControl4;
        private System.Windows.Forms.TabPage tabPage12;
        private T2TTabControl t2TTabControl5;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.RadioButton radioPatch;
        private System.Windows.Forms.RadioButton radioPrim;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox comboPrimitive;
        private T2Control_DGV dgvShapeData;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.Button butRemoveShapeMaterial;
        private System.Windows.Forms.Button butNewShapeMaterial;
        private System.Windows.Forms.GroupBox groupMaterialProperties;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Button butAmbColorPicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericCharShapeID2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericCharShapeID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericPatchMaterialID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericPatchType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butRemovePrim;
        private System.Windows.Forms.Button butAddPrim;
        private T2Control_EditableLabel fieldShapeName;
        private System.Windows.Forms.ComboBox comboMaterial;
        private System.Windows.Forms.Label labelMaterialMetadataDesc;
        private System.Windows.Forms.NumericUpDown numericMaterialMetadata;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn UX;
        private System.Windows.Forms.DataGridViewTextBoxColumn UY;
        private System.Windows.Forms.DataGridViewTextBoxColumn UZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alpha;
        private System.Windows.Forms.DataGridViewButtonColumn VColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn U;
        private System.Windows.Forms.DataGridViewTextBoxColumn V;
        private System.Windows.Forms.Button butSendToMultiMat;
        private T2Control_TextureSelector t2Control_TextureSelector1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextDGV;
        private System.Windows.Forms.ToolStripMenuItem replaceSelectedValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillSelectedWithRandomNumbersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectInvertedSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllCellsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllCellsInColumToolStripMenuItem;
    }
}
