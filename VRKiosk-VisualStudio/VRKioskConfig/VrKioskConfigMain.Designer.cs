namespace VRKioskConfig
{
    partial class VrKioskMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VrKioskMain));
            this.grpAppsList = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvAppList = new System.Windows.Forms.DataGridView();
            this.grpEditApp = new System.Windows.Forms.GroupBox();
            this.btnEditFilePath = new System.Windows.Forms.Button();
            this.btnSendKeysForACtrl = new System.Windows.Forms.Button();
            this.btnSendKeysASpace = new System.Windows.Forms.Button();
            this.lblSendKeysForA = new System.Windows.Forms.Label();
            this.txtSendKeysForA = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnTabSendKey = new System.Windows.Forms.Button();
            this.btnEnterSendKey = new System.Windows.Forms.Button();
            this.lblSendKeysDialog = new System.Windows.Forms.Label();
            this.txtSendKeysForDialog = new System.Windows.Forms.TextBox();
            this.btnUnity = new System.Windows.Forms.Button();
            this.btnUE4 = new System.Windows.Forms.Button();
            this.lblParams = new System.Windows.Forms.Label();
            this.txtParams = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtAppName = new System.Windows.Forms.TextBox();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.btnSaveCategories = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.btnAddApp = new System.Windows.Forms.Button();
            this.btnSortByNameAsc = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveChangesToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpAppsActions = new System.Windows.Forms.GroupBox();
            this.btnSortDateAsc = new System.Windows.Forms.Button();
            this.btnSortDesc = new System.Windows.Forms.Button();
            this.grpFilterCategories = new System.Windows.Forms.GroupBox();
            this.btnDiscardCatChgs = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bottomLeftPicBox = new System.Windows.Forms.PictureBox();
            this.picForSelApp = new System.Windows.Forms.PictureBox();
            this.grpAppsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppList)).BeginInit();
            this.grpEditApp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.grpAppsActions.SuspendLayout();
            this.grpFilterCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeftPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForSelApp)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAppsList
            // 
            this.grpAppsList.BackColor = System.Drawing.Color.Transparent;
            this.grpAppsList.Controls.Add(this.txtSearch);
            this.grpAppsList.Controls.Add(this.lblSearch);
            this.grpAppsList.Controls.Add(this.dgvAppList);
            this.grpAppsList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAppsList.Location = new System.Drawing.Point(12, 3);
            this.grpAppsList.Name = "grpAppsList";
            this.grpAppsList.Size = new System.Drawing.Size(516, 557);
            this.grpAppsList.TabIndex = 1;
            this.grpAppsList.TabStop = false;
            this.grpAppsList.Text = "Apps List";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(9, 38);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(179, 23);
            this.txtSearch.TabIndex = 24;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(6, 23);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 15);
            this.lblSearch.TabIndex = 25;
            this.lblSearch.Text = "Search:";
            // 
            // dgvAppList
            // 
            this.dgvAppList.AllowDrop = true;
            this.dgvAppList.AllowUserToResizeColumns = false;
            this.dgvAppList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAppList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAppList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAppList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAppList.Location = new System.Drawing.Point(6, 65);
            this.dgvAppList.Name = "dgvAppList";
            this.dgvAppList.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAppList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAppList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppList.Size = new System.Drawing.Size(504, 480);
            this.dgvAppList.TabIndex = 0;
            this.dgvAppList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAppList_CellClick);
            this.dgvAppList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAppList_CellContentClick);
            this.dgvAppList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvAppList_DataError);
            this.dgvAppList.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAppList_RowHeaderMouseClick);
            this.dgvAppList.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvAppList_DragDrop);
            this.dgvAppList.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvAppList_DragEnter);
            this.dgvAppList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAppList_KeyUpDown);
            this.dgvAppList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvAppList_KeyUpDown);
            // 
            // grpEditApp
            // 
            this.grpEditApp.BackColor = System.Drawing.Color.Transparent;
            this.grpEditApp.Controls.Add(this.btnEditFilePath);
            this.grpEditApp.Controls.Add(this.btnSendKeysForACtrl);
            this.grpEditApp.Controls.Add(this.btnSendKeysASpace);
            this.grpEditApp.Controls.Add(this.lblSendKeysForA);
            this.grpEditApp.Controls.Add(this.txtSendKeysForA);
            this.grpEditApp.Controls.Add(this.lblNotes);
            this.grpEditApp.Controls.Add(this.txtNotes);
            this.grpEditApp.Controls.Add(this.btnTabSendKey);
            this.grpEditApp.Controls.Add(this.btnEnterSendKey);
            this.grpEditApp.Controls.Add(this.lblSendKeysDialog);
            this.grpEditApp.Controls.Add(this.txtSendKeysForDialog);
            this.grpEditApp.Controls.Add(this.btnUnity);
            this.grpEditApp.Controls.Add(this.btnUE4);
            this.grpEditApp.Controls.Add(this.lblParams);
            this.grpEditApp.Controls.Add(this.txtParams);
            this.grpEditApp.Controls.Add(this.lblFilePath);
            this.grpEditApp.Controls.Add(this.txtFilePath);
            this.grpEditApp.Controls.Add(this.txtAppName);
            this.grpEditApp.Controls.Add(this.btnBrowseImage);
            this.grpEditApp.Controls.Add(this.picForSelApp);
            this.grpEditApp.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEditApp.Location = new System.Drawing.Point(534, 3);
            this.grpEditApp.Name = "grpEditApp";
            this.grpEditApp.Size = new System.Drawing.Size(408, 673);
            this.grpEditApp.TabIndex = 2;
            this.grpEditApp.TabStop = false;
            this.grpEditApp.Text = "Edit App Selected";
            // 
            // btnEditFilePath
            // 
            this.btnEditFilePath.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditFilePath.Location = new System.Drawing.Point(70, 358);
            this.btnEditFilePath.Name = "btnEditFilePath";
            this.btnEditFilePath.Size = new System.Drawing.Size(58, 22);
            this.btnEditFilePath.TabIndex = 20;
            this.btnEditFilePath.Text = "Browse";
            this.btnEditFilePath.UseVisualStyleBackColor = true;
            this.btnEditFilePath.Click += new System.EventHandler(this.btnEditFilePath_Click);
            // 
            // btnSendKeysForACtrl
            // 
            this.btnSendKeysForACtrl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendKeysForACtrl.Location = new System.Drawing.Point(318, 616);
            this.btnSendKeysForACtrl.Name = "btnSendKeysForACtrl";
            this.btnSendKeysForACtrl.Size = new System.Drawing.Size(45, 22);
            this.btnSendKeysForACtrl.TabIndex = 19;
            this.btnSendKeysForACtrl.Text = "Ctrl";
            this.btnSendKeysForACtrl.UseVisualStyleBackColor = true;
            this.btnSendKeysForACtrl.Click += new System.EventHandler(this.btnSendKeysForACtrl_Click);
            // 
            // btnSendKeysASpace
            // 
            this.btnSendKeysASpace.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendKeysASpace.Location = new System.Drawing.Point(246, 616);
            this.btnSendKeysASpace.Name = "btnSendKeysASpace";
            this.btnSendKeysASpace.Size = new System.Drawing.Size(66, 22);
            this.btnSendKeysASpace.TabIndex = 16;
            this.btnSendKeysASpace.Text = "Spacebar";
            this.btnSendKeysASpace.UseVisualStyleBackColor = true;
            this.btnSendKeysASpace.Click += new System.EventHandler(this.btnSendKeysASpace_Click);
            // 
            // lblSendKeysForA
            // 
            this.lblSendKeysForA.AutoSize = true;
            this.lblSendKeysForA.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendKeysForA.Location = new System.Drawing.Point(6, 616);
            this.lblSendKeysForA.Name = "lblSendKeysForA";
            this.lblSendKeysForA.Size = new System.Drawing.Size(237, 15);
            this.lblSendKeysForA.TabIndex = 18;
            this.lblSendKeysForA.Text = "Send Keys for A button on Xbox controller:";
            this.toolTip1.SetToolTip(this.lblSendKeysForA, "Simulate a keyboard key for pressing the A button on Xbox controller");
            // 
            // txtSendKeysForA
            // 
            this.txtSendKeysForA.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendKeysForA.Location = new System.Drawing.Point(9, 642);
            this.txtSendKeysForA.Name = "txtSendKeysForA";
            this.txtSendKeysForA.Size = new System.Drawing.Size(393, 23);
            this.txtSendKeysForA.TabIndex = 17;
            this.txtSendKeysForA.TextChanged += new System.EventHandler(this.txtAppEditsMade_TextChanged);
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(6, 526);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(41, 15);
            this.lblNotes.TabIndex = 15;
            this.lblNotes.Text = "Notes:";
            this.toolTip1.SetToolTip(this.lblNotes, "Notes to be displayed in the VR app on bottom");
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(9, 544);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(393, 62);
            this.txtNotes.TabIndex = 14;
            this.txtNotes.TextChanged += new System.EventHandler(this.txtAppEditsMade_TextChanged);
            // 
            // btnTabSendKey
            // 
            this.btnTabSendKey.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabSendKey.Location = new System.Drawing.Point(185, 473);
            this.btnTabSendKey.Name = "btnTabSendKey";
            this.btnTabSendKey.Size = new System.Drawing.Size(45, 22);
            this.btnTabSendKey.TabIndex = 13;
            this.btnTabSendKey.Text = "Tab";
            this.btnTabSendKey.UseVisualStyleBackColor = true;
            this.btnTabSendKey.Click += new System.EventHandler(this.btnTabSendKey_Click);
            // 
            // btnEnterSendKey
            // 
            this.btnEnterSendKey.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterSendKey.Location = new System.Drawing.Point(134, 473);
            this.btnEnterSendKey.Name = "btnEnterSendKey";
            this.btnEnterSendKey.Size = new System.Drawing.Size(45, 22);
            this.btnEnterSendKey.TabIndex = 10;
            this.btnEnterSendKey.Text = "Enter";
            this.btnEnterSendKey.UseVisualStyleBackColor = true;
            this.btnEnterSendKey.Click += new System.EventHandler(this.btnEnterSendKey_Click);
            // 
            // lblSendKeysDialog
            // 
            this.lblSendKeysDialog.AutoSize = true;
            this.lblSendKeysDialog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendKeysDialog.Location = new System.Drawing.Point(6, 473);
            this.lblSendKeysDialog.Name = "lblSendKeysDialog";
            this.lblSendKeysDialog.Size = new System.Drawing.Size(122, 15);
            this.lblSendKeysDialog.TabIndex = 12;
            this.lblSendKeysDialog.Text = "Send Keys for Dialog:";
            this.toolTip1.SetToolTip(this.lblSendKeysDialog, "Used to skip dialogs. Simulate key presses such as Enter or Tab");
            // 
            // txtSendKeysForDialog
            // 
            this.txtSendKeysForDialog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendKeysForDialog.Location = new System.Drawing.Point(9, 496);
            this.txtSendKeysForDialog.Name = "txtSendKeysForDialog";
            this.txtSendKeysForDialog.Size = new System.Drawing.Size(393, 23);
            this.txtSendKeysForDialog.TabIndex = 11;
            this.txtSendKeysForDialog.TextChanged += new System.EventHandler(this.txtAppEditsMade_TextChanged);
            // 
            // btnUnity
            // 
            this.btnUnity.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnity.Location = new System.Drawing.Point(136, 417);
            this.btnUnity.Name = "btnUnity";
            this.btnUnity.Size = new System.Drawing.Size(45, 22);
            this.btnUnity.TabIndex = 7;
            this.btnUnity.Text = "Unity";
            this.toolTip1.SetToolTip(this.btnUnity, "Common Unity param for forcing DX11");
            this.btnUnity.UseVisualStyleBackColor = true;
            this.btnUnity.Click += new System.EventHandler(this.btnUnity_Click);
            // 
            // btnUE4
            // 
            this.btnUE4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUE4.Location = new System.Drawing.Point(85, 417);
            this.btnUE4.Name = "btnUE4";
            this.btnUE4.Size = new System.Drawing.Size(45, 22);
            this.btnUE4.TabIndex = 5;
            this.btnUE4.Text = "UE4";
            this.toolTip1.SetToolTip(this.btnUE4, "Quick button to add a common UE4 param");
            this.btnUE4.UseVisualStyleBackColor = true;
            this.btnUE4.Click += new System.EventHandler(this.btnUE4_Click);
            // 
            // lblParams
            // 
            this.lblParams.AutoSize = true;
            this.lblParams.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParams.Location = new System.Drawing.Point(6, 417);
            this.lblParams.Name = "lblParams";
            this.lblParams.Size = new System.Drawing.Size(73, 15);
            this.lblParams.TabIndex = 6;
            this.lblParams.Text = "Parameters:";
            this.toolTip1.SetToolTip(this.lblParams, "Params to send to the exe");
            // 
            // txtParams
            // 
            this.txtParams.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParams.Location = new System.Drawing.Point(9, 440);
            this.txtParams.Name = "txtParams";
            this.txtParams.Size = new System.Drawing.Size(393, 23);
            this.txtParams.TabIndex = 5;
            this.txtParams.TextChanged += new System.EventHandler(this.txtAppEditsMade_TextChanged);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(6, 358);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(58, 15);
            this.lblFilePath.TabIndex = 4;
            this.lblFilePath.Text = "File Path:";
            this.toolTip1.SetToolTip(this.lblFilePath, "Path to file to launch");
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(9, 381);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(393, 23);
            this.txtFilePath.TabIndex = 3;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtAppEditsMade_TextChanged);
            // 
            // txtAppName
            // 
            this.txtAppName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppName.Location = new System.Drawing.Point(9, 30);
            this.txtAppName.Name = "txtAppName";
            this.txtAppName.Size = new System.Drawing.Size(393, 27);
            this.txtAppName.TabIndex = 2;
            this.txtAppName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAppName.TextChanged += new System.EventHandler(this.txtAppEditsMade_TextChanged);
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnBrowseImage.Location = new System.Drawing.Point(144, 327);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(119, 23);
            this.btnBrowseImage.TabIndex = 1;
            this.btnBrowseImage.Text = "change image";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // btnSaveCategories
            // 
            this.btnSaveCategories.Enabled = false;
            this.btnSaveCategories.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCategories.Location = new System.Drawing.Point(83, 170);
            this.btnSaveCategories.Name = "btnSaveCategories";
            this.btnSaveCategories.Size = new System.Drawing.Size(70, 22);
            this.btnSaveCategories.TabIndex = 25;
            this.btnSaveCategories.Text = "Save";
            this.toolTip1.SetToolTip(this.btnSaveCategories, "Save Categor Changes (have to save on left as well)");
            this.btnSaveCategories.UseVisualStyleBackColor = true;
            this.btnSaveCategories.Click += new System.EventHandler(this.btnSaveCategories_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.Location = new System.Drawing.Point(7, 170);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(70, 22);
            this.btnAddCategory.TabIndex = 24;
            this.btnAddCategory.Text = "Add";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToResizeColumns = false;
            this.dgvCategories.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Location = new System.Drawing.Point(7, 18);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(387, 148);
            this.dgvCategories.TabIndex = 23;
            this.dgvCategories.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategories_CellDoubleClick);
            // 
            // btnAddApp
            // 
            this.btnAddApp.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddApp.Location = new System.Drawing.Point(21, 26);
            this.btnAddApp.Name = "btnAddApp";
            this.btnAddApp.Size = new System.Drawing.Size(179, 38);
            this.btnAddApp.TabIndex = 3;
            this.btnAddApp.Text = "+ Add";
            this.btnAddApp.UseVisualStyleBackColor = true;
            this.btnAddApp.Click += new System.EventHandler(this.btnAddApp_Click);
            // 
            // btnSortByNameAsc
            // 
            this.btnSortByNameAsc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortByNameAsc.Location = new System.Drawing.Point(21, 84);
            this.btnSortByNameAsc.Name = "btnSortByNameAsc";
            this.btnSortByNameAsc.Size = new System.Drawing.Size(179, 38);
            this.btnSortByNameAsc.TabIndex = 4;
            this.btnSortByNameAsc.Text = "Sort by Name Ascending";
            this.btnSortByNameAsc.UseVisualStyleBackColor = true;
            this.btnSortByNameAsc.Click += new System.EventHandler(this.btnSortByNameAsc_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // addAppToolStripMenuItem
            // 
            this.addAppToolStripMenuItem.Name = "addAppToolStripMenuItem";
            this.addAppToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // saveChangesToXMLToolStripMenuItem
            // 
            this.saveChangesToXMLToolStripMenuItem.Name = "saveChangesToXMLToolStripMenuItem";
            this.saveChangesToXMLToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // editCategoriesToolStripMenuItem
            // 
            this.editCategoriesToolStripMenuItem.Name = "editCategoriesToolStripMenuItem";
            this.editCategoriesToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(21, 235);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(179, 38);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpAppsActions
            // 
            this.grpAppsActions.BackColor = System.Drawing.Color.Transparent;
            this.grpAppsActions.Controls.Add(this.btnSortDateAsc);
            this.grpAppsActions.Controls.Add(this.btnSortDesc);
            this.grpAppsActions.Controls.Add(this.btnSave);
            this.grpAppsActions.Controls.Add(this.btnSortByNameAsc);
            this.grpAppsActions.Controls.Add(this.btnAddApp);
            this.grpAppsActions.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAppsActions.Location = new System.Drawing.Point(305, 575);
            this.grpAppsActions.Name = "grpAppsActions";
            this.grpAppsActions.Size = new System.Drawing.Size(223, 311);
            this.grpAppsActions.TabIndex = 26;
            this.grpAppsActions.TabStop = false;
            this.grpAppsActions.Text = "Actions on App List";
            // 
            // btnSortDateAsc
            // 
            this.btnSortDateAsc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortDateAsc.Location = new System.Drawing.Point(21, 172);
            this.btnSortDateAsc.Name = "btnSortDateAsc";
            this.btnSortDateAsc.Size = new System.Drawing.Size(179, 38);
            this.btnSortDateAsc.TabIndex = 9;
            this.btnSortDateAsc.Text = "Sort by Date Added";
            this.btnSortDateAsc.UseVisualStyleBackColor = true;
            this.btnSortDateAsc.Click += new System.EventHandler(this.btnSortDateAsc_Click);
            // 
            // btnSortDesc
            // 
            this.btnSortDesc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortDesc.Location = new System.Drawing.Point(21, 128);
            this.btnSortDesc.Name = "btnSortDesc";
            this.btnSortDesc.Size = new System.Drawing.Size(179, 38);
            this.btnSortDesc.TabIndex = 8;
            this.btnSortDesc.Text = "Sort by Name Descending";
            this.btnSortDesc.UseVisualStyleBackColor = true;
            this.btnSortDesc.Click += new System.EventHandler(this.btnSortDesc_Click);
            // 
            // grpFilterCategories
            // 
            this.grpFilterCategories.BackColor = System.Drawing.Color.Transparent;
            this.grpFilterCategories.Controls.Add(this.btnDiscardCatChgs);
            this.grpFilterCategories.Controls.Add(this.btnDeleteCategory);
            this.grpFilterCategories.Controls.Add(this.btnSaveCategories);
            this.grpFilterCategories.Controls.Add(this.dgvCategories);
            this.grpFilterCategories.Controls.Add(this.btnAddCategory);
            this.grpFilterCategories.Location = new System.Drawing.Point(543, 682);
            this.grpFilterCategories.Name = "grpFilterCategories";
            this.grpFilterCategories.Size = new System.Drawing.Size(399, 204);
            this.grpFilterCategories.TabIndex = 27;
            this.grpFilterCategories.TabStop = false;
            this.grpFilterCategories.Text = "Categories for selected app";
            // 
            // btnDiscardCatChgs
            // 
            this.btnDiscardCatChgs.Enabled = false;
            this.btnDiscardCatChgs.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscardCatChgs.Location = new System.Drawing.Point(247, 170);
            this.btnDiscardCatChgs.Name = "btnDiscardCatChgs";
            this.btnDiscardCatChgs.Size = new System.Drawing.Size(70, 22);
            this.btnDiscardCatChgs.TabIndex = 27;
            this.btnDiscardCatChgs.Text = "Discard";
            this.btnDiscardCatChgs.UseVisualStyleBackColor = true;
            this.btnDiscardCatChgs.Click += new System.EventHandler(this.btnDiscardCatChgs_Click);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Enabled = false;
            this.btnDeleteCategory.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCategory.Location = new System.Drawing.Point(323, 170);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(70, 22);
            this.btnDeleteCategory.TabIndex = 26;
            this.btnDeleteCategory.Text = "Delete";
            this.toolTip1.SetToolTip(this.btnDeleteCategory, "Delete Category");
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // bottomLeftPicBox
            // 
            this.bottomLeftPicBox.BackColor = System.Drawing.Color.Transparent;
            this.bottomLeftPicBox.Image = global::VRKioskConfig.Properties.Resources.DragAndDropAppsPic;
            this.bottomLeftPicBox.Location = new System.Drawing.Point(12, 575);
            this.bottomLeftPicBox.Name = "bottomLeftPicBox";
            this.bottomLeftPicBox.Size = new System.Drawing.Size(287, 311);
            this.bottomLeftPicBox.TabIndex = 28;
            this.bottomLeftPicBox.TabStop = false;
            this.toolTip1.SetToolTip(this.bottomLeftPicBox, "Drag and drop images here to change it");
            // 
            // picForSelApp
            // 
            this.picForSelApp.Location = new System.Drawing.Point(75, 65);
            this.picForSelApp.Name = "picForSelApp";
            this.picForSelApp.Size = new System.Drawing.Size(256, 256);
            this.picForSelApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picForSelApp.TabIndex = 0;
            this.picForSelApp.TabStop = false;
            this.toolTip1.SetToolTip(this.picForSelApp, "Drag and drop images here to change it");
            this.picForSelApp.DragDrop += new System.Windows.Forms.DragEventHandler(this.picForSelApp_DragDrop);
            this.picForSelApp.DragEnter += new System.Windows.Forms.DragEventHandler(this.picForSelApp_DragEnter);
            // 
            // VrKioskMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::VRKioskConfig.Properties.Resources.bgImageTile;
            this.ClientSize = new System.Drawing.Size(954, 898);
            this.Controls.Add(this.bottomLeftPicBox);
            this.Controls.Add(this.grpFilterCategories);
            this.Controls.Add(this.grpAppsActions);
            this.Controls.Add(this.grpEditApp);
            this.Controls.Add(this.grpAppsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VrKioskMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VR Kiosk Configuration";
            this.Load += new System.EventHandler(this.VrKioskMain_Load);
            this.grpAppsList.ResumeLayout(false);
            this.grpAppsList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppList)).EndInit();
            this.grpEditApp.ResumeLayout(false);
            this.grpEditApp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.grpAppsActions.ResumeLayout(false);
            this.grpFilterCategories.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeftPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForSelApp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpAppsList;
        private System.Windows.Forms.GroupBox grpEditApp;
        private System.Windows.Forms.PictureBox picForSelApp;
        private System.Windows.Forms.Button btnAddApp;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Button btnSortByNameAsc;
        private System.Windows.Forms.DataGridView dgvAppList;
        private System.Windows.Forms.TextBox txtAppName;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblParams;
        private System.Windows.Forms.TextBox txtParams;
        private System.Windows.Forms.Button btnUnity;
        private System.Windows.Forms.Button btnUE4;
        private System.Windows.Forms.Button btnTabSendKey;
        private System.Windows.Forms.Button btnEnterSendKey;
        private System.Windows.Forms.Label lblSendKeysDialog;
        private System.Windows.Forms.TextBox txtSendKeysForDialog;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveChangesToXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCategoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnSendKeysForACtrl;
        private System.Windows.Forms.Button btnSendKeysASpace;
        private System.Windows.Forms.Label lblSendKeysForA;
        private System.Windows.Forms.TextBox txtSendKeysForA;
        private System.Windows.Forms.Button btnEditFilePath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox grpAppsActions;
        private System.Windows.Forms.Button btnSortDesc;
        private System.Windows.Forms.Button btnSortDateAsc;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnSaveCategories;
        private System.Windows.Forms.GroupBox grpFilterCategories;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDiscardCatChgs;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.PictureBox bottomLeftPicBox;
    }
}