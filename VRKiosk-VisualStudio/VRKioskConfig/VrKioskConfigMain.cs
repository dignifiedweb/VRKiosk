using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TsudaKageyu;
using System.Xml.Linq;
using System.Linq;
using System.Resources;
using System.Data;
using System.Diagnostics;
using Shell32;

namespace VRKioskConfig
{
    public partial class VrKioskMain : Form
    {
        private Bitmap _currentBitmapDisplayed;
        private String _currentDir;
        private String _gameImagesDir;
        private String _mainXmlPath;

        private List<AppListItem> _appListItems;

        private XDocument _xdoc;
        private IEnumerable<XElement> _rootAppsNodeXml;
        private IEnumerable<XElement> _appsListFromXml;
        private IEnumerable<XElement> _appFiltersFromXml;
        private int _dataErrorCount;
        private List<AppListItemCategory> _newCategoriesToSave;
        private List<AppListItemCategory> _origSelectedCategory;
        private List<AppListItemCategory> _deletedCategories;
        private bool _editCategoryMode;

        public const int ICON_WIDTH = 64;

        public VrKioskMain()
        {
            InitializeComponent();

            _currentDir = Directory.GetCurrentDirectory();
            _gameImagesDir = _currentDir + "\\VRKioskGUI\\gameImages";
            _mainXmlPath = _currentDir + "\\VRKioskGUI\\VRKiosk_Config.xml";
            _dataErrorCount = 0;
            _editCategoryMode = false;

            if (!Directory.Exists(_gameImagesDir))
            {
                try
                {
                    Directory.CreateDirectory(_gameImagesDir);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, could not create: " + _gameImagesDir + "\n\nAfter pressing ok, the application will close. \n\n\n\n" + ex.Message, "ERROR - Game images folder exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(1);
                }
            }
        }

        private void VrKioskMain_Load(object sender, EventArgs e)
        {
            ((Control)picForSelApp).AllowDrop = true;

            _appListItems = new List<AppListItem>();
            _newCategoriesToSave = new List<AppListItemCategory>();
            _origSelectedCategory = new List<AppListItemCategory>();
            _deletedCategories = new List<AppListItemCategory>();

            // Hide the header columns on the datagridview
            dgvAppList.ColumnHeadersVisible = false;

            // Only allow full row to be selected in datagridview
            // Todo: add datagridview generic method for settings
            // JCarewick - TODO - either generic method for settings or move to designer code
            dgvAppList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppList.AllowUserToResizeColumns = false;
            dgvAppList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvAppList.MultiSelect = false;
            dgvAppList.AllowUserToResizeRows = false;

            // Add a key down listener so that shortcuts can be created for the app
            this.KeyPreview = true;
            this.KeyDown += VrKioskMain_KeyDown;

            try
            {
                LoadAppsFromXML();

                if (_appListItems.Count > 0)
                {
                    RefreshDataGridViewMain();

                    dgvAppList.Columns.Add(new DeleteColumn());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading info from XML.\n\n\n\n" + ex.Message, "ERROR - VrKioskMain_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Monitor key presses for keyboard shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VrKioskMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                DialogResult diagResult = MessageBox.Show("Do you want to save?", "Do you want to save?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (diagResult.Equals(DialogResult.Yes))
                {
                    SaveChanges();
                }

                e.SuppressKeyPress = true;  // Stops bing! Also sets handled which stop event bubbling
            }
        }

        private void CreateXmlFileIfNotFound()
        {
            // If the XML file does not exist, create a basic one that's empty to initialize everything
            if (!File.Exists(_mainXmlPath))
            {
                XElement xmlRoot = new XElement("VRKiosk_Config");
                XElement xmlFilterCategories = new XElement("filterCategories");
                XElement xmlFilterCategoryDefault = new XElement("filterCategory");
                xmlFilterCategoryDefault.SetAttributeValue("name", "Demo");
                xmlFilterCategoryDefault.SetAttributeValue("id", "0");
                xmlFilterCategories.Add(xmlFilterCategoryDefault);
                xmlRoot.Add(xmlFilterCategories);

                XDocument xRootDocTmp = new XDocument(xmlRoot);

                using (var fs = new System.IO.FileStream(_mainXmlPath, System.IO.FileMode.Create))
                {
                    xRootDocTmp.Save(fs);
                }
            }
        }

        private void LoadAppsFromXML()
        {
            try
            {
                CreateXmlFileIfNotFound();

                // Load main XML document and keep a reference to the root node to add to later
                _xdoc = XDocument.Load(_mainXmlPath);
                _rootAppsNodeXml =      from apps in _xdoc.Descendants("apps")
                                        select apps;

                _appsListFromXml =      from app in _xdoc.Descendants("app")
                                        select app;

                _appFiltersFromXml =    from appFilters in _xdoc.Descendants("filterCategory")
                                        select appFilters;

                // List all apps from XML
                foreach (XElement appElement in _appsListFromXml)
                {
                    AppListItem newItem = new AppListItem();

                    string tmpImagePathToLoad = _currentDir + "\\VRKioskGUI\\" + appElement.Attribute("imgFilename").Value;
                    if (File.Exists(tmpImagePathToLoad))
                    {
                        // Open the image as a stream, so it doens't keep a file handle lock on the file
                        using (var fs = new System.IO.FileStream(tmpImagePathToLoad, System.IO.FileMode.Open))
                        {
                            Bitmap bmp = new Bitmap(fs);
                            newItem.Image = (Bitmap)bmp.Clone();
                            newItem.Icon = new Bitmap(newItem.Image, new Size(ICON_WIDTH, ICON_WIDTH));
                            newItem.ImageFilePath = appElement.Attribute("imgFilename").Value;
                        }
                    }
                    else
                    {
                        Bitmap imageNotFoundBmp = new Bitmap(VRKioskConfig.Properties.Resources.imageNotFound);
                        newItem.Image = imageNotFoundBmp;
                        newItem.Icon = new Bitmap(imageNotFoundBmp, new Size(ICON_WIDTH, ICON_WIDTH));
                        newItem.ImageFilePath = "";
                    }

                    // Set app list items we can with the attribute values:
                    newItem.FilePath = appElement.Attribute("fullFilePath").Value;
                    newItem.Name = appElement.Attribute("name").Value;
                    newItem.Parameters = appElement.Attribute("paramsCustom").Value;
                    newItem.SendKeysForDialog = appElement.Attribute("sendKeysForDialog").Value;
                    newItem.SendKeysForAButton = appElement.Attribute("sendKeysForAButton").Value;
                    newItem.AppDateTimeAdded = DateTime.Parse(appElement.Attribute("appDateTimeAdded").Value);

                    // Get the notes for this app
                    IEnumerable<XElement> notesForApp = from notes in appElement.Descendants("notes")
                                                        select notes;

                    newItem.Notes = notesForApp.First().Value;

                    // List all filters that were saved and display which apply to each app
                    foreach (XElement appFilter in _appFiltersFromXml)
                    {
                        AppListItemCategory category = new AppListItemCategory();

                        // Check if this app has this filter (count > 0)
                        bool appHasFilter = (from filter in appElement.Descendants("filter")
                                                where filter.Value == appFilter.Attribute("id").Value
                                                select filter).Count() > 0;

                        category.CategorySelected = appHasFilter;
                        category.CategoryName = appFilter.Attribute("name").Value;
                        category.CategoryNum = int.Parse(appFilter.Attribute("id").Value);

                        newItem.Categories.Add(category);
                    }

                    // Add the new app to our list
                    _appListItems.Add(newItem);
                }

                if (_appListItems.Count > 0)
                {
                    SetGUIItemsToSelectedApp(_appListItems[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading XML file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RefreshDataGridViewMain()
        {
            // Clear the data grid view and load data
            dgvAppList.DataSource = _appListItems;
            dgvAppList.Refresh();

            // Format the DataGridView
            // Hide some columns
            dgvAppList.Columns["FilePath"].Visible = false;
            dgvAppList.Columns["Image"].Visible = false;
            dgvAppList.Columns["Parameters"].Visible = false;
            dgvAppList.Columns["SendKeysForDialog"].Visible = false;
            dgvAppList.Columns["SendKeysForAButton"].Visible = false; 
            dgvAppList.Columns["Notes"].Visible = false;
            dgvAppList.Columns["PhotoChanged"].Visible = false;
            dgvAppList.Columns["ImageFilePath"].Visible = false;
            dgvAppList.Columns["AppDateTimeAdded"].Visible = false;

            // Format the width of the columns
            dgvAppList.Columns["Name"].Width = (380-ICON_WIDTH); // 380
            dgvAppList.Columns["Icon"].Width = ICON_WIDTH;

            // Auto-resize the rows to fit the full icon
            for (int i = 0; i < dgvAppList.Rows.Count; i++)
            {
                dgvAppList.AutoResizeRow(i);
            }
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "Executables (*.exe)|*.exe";
            DialogResult result = diag.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                AddNewApp(diag.FileName);
            }
        }

        private void AddNewApp(string filename)
        {
            AppListItem app = new AppListItem();
            app.Name = GetNameOfExeOnly(filename);
            app.Image = GetBitmapFromExecutable(filename);
            app.Icon = new Bitmap(app.Image, new Size(ICON_WIDTH, ICON_WIDTH));
            app.ImageFilePath = "";
            app.FilePath = filename;
            app.Parameters = "";
            app.SendKeysForDialog = "";
            app.SendKeysForAButton = "";
            app.Notes = "";
            app.PhotoChanged = true;
            app.AppDateTimeAdded = DateTime.Now;

            // List all filters that were saved and display which apply to each app
            int countFilters = 1;
            foreach (XElement appFilter in _appFiltersFromXml)
            {
                AppListItemCategory category = new AppListItemCategory();
                if (countFilters == 1)
                {
                    category.CategorySelected = true;
                    countFilters++;
                }
                else
                {
                    category.CategorySelected = false;
                }

                category.CategoryName = appFilter.Attribute("name").Value;
                category.CategoryNum = int.Parse(appFilter.Attribute("id").Value);

                app.Categories.Add(category);
            }

            // Add new app to list (displayable in datagridview)
            _appListItems.Add(app);
            dgvAppList.DataSource = null;
            if (dgvAppList.Columns["Delete"] != null)
            {
                dgvAppList.Columns.Remove("Delete");
            }
            RefreshDataGridViewMain();
            dgvAppList.Columns.Add(new DeleteColumn());

            // Jump to bottom of the datagridview and display the app just added
            dgvAppList.Rows[dgvAppList.Rows.Count - 1].Selected = true;
            dgvAppList.Rows[dgvAppList.Rows.Count - 1].Cells[0].Selected = true;
            SelectAppInDataGrid();
        }

        /// <summary>
        /// Extract a bitmap object from an executable using the Library by Tsuda Kageyu
        /// http://www.codeproject.com/Articles/26824/Extract-icons-from-EXE-or-DLL-files
        /// Grab the largest available graphic
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Bitmap grahic object, convertable to other formats</returns>
        private Bitmap GetBitmapFromExecutable(String filename)
        {
            Bitmap tmpBitmap = null;
            Icon icon = null;
            Icon []
            splitIcons = null;

            try
            {
                var extractor = new IconExtractor(filename);
                icon = extractor.GetIcon(0);
                splitIcons = IconUtil.Split(icon);
                int lastIconWidth = 0;

                foreach (var i in splitIcons)
                {
                    var size = i.Size;
                    var bits = IconUtil.GetBitCount(i);
                    if (size.Width > lastIconWidth)
                    {
                        tmpBitmap = IconUtil.ToBitmap(i);
                    }
                    i.Dispose();
                }

                return tmpBitmap;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error with extracting icon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return tmpBitmap;
            }
        }

        /// <summary>
        /// Get name of executable only, no exe extension
        /// </summary>
        /// <param name="pathToExe"></param>
        /// <returns></returns>
        private String GetNameOfExeOnly(String pathToExe) 
        {
            FileInfo info = new FileInfo(pathToExe);
            string exeName = info.Name;
            int locOfExeStr = exeName.ToLower().IndexOf(".exe");
            return exeName.Substring(0, locOfExeStr);
        }

        /// <summary>
        /// Determine if user clicked the arrow next to row (still part of the row)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAppList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectAppInDataGrid();
        }

        /// <summary>
        /// Determine if user clicked the row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAppList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv == null)
                    return;
                if (dgv.CurrentRow.Selected)
                {
                    SelectAppInDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clicking on datagridview" + ex.Message);
            }
        }

        /// <summary>
        /// DataGridView select row with arrows up/down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAppList_KeyUpDown(object sender, KeyEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            if (dgv.CurrentRow.Selected)
            {
                SelectAppInDataGrid();
            }
        }

        /// <summary>
        /// Action to take when App is selected in DataGridView
        /// </summary>
        private void SelectAppInDataGrid()
        {
            if (dgvAppList.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAppList.SelectedRows[0];
                AppListItem selectedApp = (AppListItem)selectedRow.DataBoundItem;
                SetGUIItemsToSelectedApp(selectedApp);
            }
            else
            {
                ClearEditAppSelectedArea();
            }
        }

        /// <summary>
        /// Clear all textboxes, etc in the "Edit selected app" area
        /// Called when no apps can be selected
        /// </summary>
        private void ClearEditAppSelectedArea()
        {
            txtAppName.Text = "";
            _currentBitmapDisplayed = null;
            picForSelApp.Image = _currentBitmapDisplayed;
            txtFilePath.Text = "";
            txtParams.Text = "";
            txtSendKeysForDialog.Text = "";
            txtNotes.Text = "";
            txtSendKeysForA.Text = "";
            dgvCategories.DataSource = null;
        }

        private void SetGUIItemsToSelectedApp(AppListItem selectedApp)
        {
            // Set the name
            txtAppName.Text = selectedApp.Name;

            // Set the image
            _currentBitmapDisplayed = selectedApp.Image;
            picForSelApp.Image = _currentBitmapDisplayed;

            txtFilePath.Text = selectedApp.FilePath;
            txtParams.Text = selectedApp.Parameters;
            txtSendKeysForDialog.Text = selectedApp.SendKeysForDialog;
            txtNotes.Text = selectedApp.Notes;
            txtSendKeysForA.Text = selectedApp.SendKeysForAButton;

            // Set the categories datagridview
            CategoriesDSProperties(selectedApp.Categories, true);
        }

        private void CategoriesDSProperties(List<AppListItemCategory> categories, bool readOnly)
        {
            dgvCategories.DataSource = categories;
            dgvCategories.Columns["CategoryNum"].Visible = false;
            dgvCategories.ColumnHeadersVisible = false;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.Columns["CategorySelected"].Width = 40;
            dgvCategories.Columns["CategoryName"].Width = 327;
            dgvCategories.Columns["CategoryName"].ReadOnly = readOnly;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "Image Files (*.png, *.jpg, *.bmp, *.exe)|*.png;*.jpg;*.bmp;*.exe";
            DialogResult result = diag.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                ChangeSelectedImage(diag.FileName);
            }
        }

        private void ChangeSelectedImage(string filename)
        {
            if (filename.Contains(".exe"))
            {
                _currentBitmapDisplayed = GetBitmapFromExecutable(filename);
            }
            else
            {
                _currentBitmapDisplayed = new Bitmap(filename);
            }

            // Update the displayed photo with selected image
            picForSelApp.Image = _currentBitmapDisplayed;

            DataGridViewRow selectedRow = dgvAppList.SelectedRows[0];
            AppListItem selectedApp = (AppListItem)selectedRow.DataBoundItem;
            selectedApp.Image = _currentBitmapDisplayed;
            selectedApp.Icon = new Bitmap(_currentBitmapDisplayed, new Size(ICON_WIDTH, ICON_WIDTH));
            RefreshDataGridViewMain();

            selectedApp.PhotoChanged = true;
        }

        /// <summary>
        /// btnSortByNameAsc - action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSortByNameAsc_Click(object sender, EventArgs e)
        {
            // Working tests
            // Works to get the directory of the executable
            //String currentDir = Directory.GetCurrentDirectory();

            // Works for getting full path to the exe
            //String currentExePath = System.Reflection.Assembly.GetEntryAssembly().Location;

            // Try converting exe name to just the name of file (no exe)
            //String testNameOfFileOnly = GetNameOfExeOnly(currentExePath);

            //MessageBox.Show(currentDir + " " + testNameOfFileOnly);

            //LoadAppsFromXML();
            //RefreshDataGridViewMain();

            // Sorting methods:
            // Working sort by name:
            _appListItems.Sort();

            // Sorting works with Linq too:
            // Sort Ascending
            //_appListItems = _appListItems.OrderBy(i => i.Name).ToList();

            // Sort Descending
            //_appListItems = _appListItems.OrderByDescending(i => i.Name).ToList();

            // Sort Ascending
            //_appListItems = _appListItems.OrderBy(i => i.Name).ToList();

            // Sort by XML Order
            //_appListItems = _appListItems.OrderByDescending(i => i.XmlAppOrderNum).ToList();


            // Refresh datagrid and refresh app selected
            RefreshDataGridViewMain();
            SelectAppInDataGrid();
        }

        private void btnSortDesc_Click(object sender, EventArgs e)
        {
            _appListItems = _appListItems.OrderByDescending(i => i.Name).ToList();

            // Refresh datagrid and refresh app selected
            RefreshDataGridViewMain();
            SelectAppInDataGrid();
        }

        private void btnSortDateAsc_Click(object sender, EventArgs e)
        {
            _appListItems = _appListItems.OrderBy(i => i.AppDateTimeAdded).ToList();

            // Refresh datagrid and refresh app selected
            RefreshDataGridViewMain();
            SelectAppInDataGrid();
        }

        /// <summary>
        /// TextChanged event handler for all textboxes to keep track of edits/changes in text per app object selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAppEditsMade_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if (dgvAppList.DataSource != null)
                {
                    if (dgvAppList.Rows.Count > 0)
                    {
                        TextBox textBoxChanged = (TextBox)sender;
                        DataGridViewRow selectedRow = dgvAppList.SelectedRows[0];
                        AppListItem selectedApp = (AppListItem)selectedRow.DataBoundItem;

                        switch (textBoxChanged.Name)
                        {
                            case "txtAppName":
                                selectedApp.Name = textBoxChanged.Text;
                                break;

                            case "txtFilePath":
                                selectedApp.FilePath = textBoxChanged.Text;
                                break;

                            case "txtParams":
                                selectedApp.Parameters = txtParams.Text;
                                break;

                            case "txtSendKeysForDialog":
                                selectedApp.SendKeysForDialog = txtSendKeysForDialog.Text;
                                break;

                            case "txtNotes":
                                selectedApp.Notes = txtNotes.Text;
                                break;

                            case "txtSendKeysForA":
                                selectedApp.SendKeysForAButton = txtSendKeysForA.Text;
                                break;
                        }

                        RefreshDataGridViewMain();
                    }
                }
            }
        }

        private void btnEditFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "Executables (*.exe)|*.exe";
            DialogResult result = diag.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                txtFilePath.Text = diag.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            XElement xAppsElement = new XElement("apps");
            String imgFileFullpathTmp = "";

            foreach (AppListItem app in _appListItems)
            {
                // Save the photo to a file if it's changed or new
                if (app.PhotoChanged)
                {
                    if (app.ImageFilePath.Length == 0)
                    {
                        app.ImageFilePath = "gameImages\\" + app.Name + ".png";
                    }

                    imgFileFullpathTmp = _currentDir + "\\VRKioskGUI\\" + app.ImageFilePath;
                    app.Image.Save(imgFileFullpathTmp, ImageFormat.Png);
                }

                // Setup the apps section of the XML document
                XElement xmlApp = new XElement("app");
                xmlApp.SetAttributeValue("name", app.Name);
                xmlApp.SetAttributeValue("imgFilename", app.ImageFilePath);
                xmlApp.SetAttributeValue("fullFilePath", app.FilePath);
                xmlApp.SetAttributeValue("paramsCustom", app.Parameters);
                xmlApp.SetAttributeValue("sendKeysForDialog", app.SendKeysForDialog);
                xmlApp.SetAttributeValue("sendKeysForAButton", app.SendKeysForAButton);
                xmlApp.SetAttributeValue("appDateTimeAdded", app.AppDateTimeAdded);

                XElement notesElement = new XElement("notes", app.Notes);
                xmlApp.Add(notesElement);

                XElement xmlfiltersElement = new XElement("filters");
                IEnumerable<AppListItemCategory> selectedFilters = from filters in app.Categories
                                                                   where filters.CategorySelected == true
                                                                   select filters;

                foreach (AppListItemCategory filter in selectedFilters)
                {
                    XElement xmlFilter = new XElement("filter", filter.CategoryNum);
                    xmlfiltersElement.Add(xmlFilter);
                }

                xmlApp.Add(xmlfiltersElement);


                // Add the app element to the apps node in the XML doc
                xAppsElement.Add(xmlApp);
            }

            // Filter Categories
            XElement xmlFilters = new XElement("filterCategories");
            foreach (XElement appFilter in _appFiltersFromXml)
            {
                xmlFilters.Add(appFilter);
            }

            // Document Root
            XElement xmlRootElement = new XElement("VRKiosk_Config", xAppsElement);
            xmlRootElement.Add(xmlFilters);
            XDocument testRootXDoc = new XDocument(xmlRootElement);

            // Backup original if exists
            if (File.Exists(_mainXmlPath))
            {
                String backupXmlName = _mainXmlPath.Replace(".xml", ".xml.bak");

                if (File.Exists(backupXmlName))
                {
                    File.Delete(backupXmlName);
                }

                File.Move(_mainXmlPath, backupXmlName);
            }

            // Save the XML document
            using (var fs = new System.IO.FileStream(_mainXmlPath, System.IO.FileMode.Create))
            {
                testRootXDoc.Save(fs);
            }

            MessageBox.Show("Save Complete", "XML & images saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Search as you type using datagridview only
        /// This retains the list items being used for editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvAppList.CurrentCell = null;
            foreach (DataGridViewRow row in dgvAppList.Rows)
            {
                if (row.Cells["Name"].Value.ToString().ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }



        #region "Hard-coded quick buttons to help fill textboxes"

        private void btnUE4_Click(object sender, EventArgs e)
        {
            txtParams.AppendText("-fullscreen");
        }

        private void btnUnity_Click(object sender, EventArgs e)
        {
            txtParams.AppendText("-force-d3d11");
        }

        private void btnEnterSendKey_Click(object sender, EventArgs e)
        {
            txtSendKeysForDialog.AppendText("{ENTER}");
        }

        private void btnTabSendKey_Click(object sender, EventArgs e)
        {
            txtSendKeysForDialog.AppendText("{TAB}");
        }

        private void btnSendKeysASpace_Click(object sender, EventArgs e)
        {
            txtSendKeysForA.AppendText(" ");
            txtSendKeysForA.Focus();
            txtSendKeysForA.SelectAll();
        }

        private void btnSendKeysForACtrl_Click(object sender, EventArgs e)
        {
            txtSendKeysForA.AppendText("^");
        }



        #endregion

        private void dgvAppList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  Handle the delete row button
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                Console.WriteLine("Button clicked delete");

                DataGridViewRow selectedRow = dgvAppList.SelectedRows[0];
                AppListItem selectedApp = (AppListItem)selectedRow.DataBoundItem;

                DialogResult result = MessageBox.Show("Are you sure you want to remove " + selectedApp.Name + "?", "Delete Selected App?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    _appListItems.Remove(selectedApp);
                    dgvAppList.DataSource = null;
                    dgvAppList.Columns.Remove("Delete");
                    RefreshDataGridViewMain();
                    dgvAppList.Columns.Add(new DeleteColumn());
                    SelectAppInDataGrid();
                }
            }
        }

        /// <summary>
        /// dgvAppList_DataError - catch datagridview data errors
        /// Hopefully they never happen! It would be annoying to get more than 1 popup, this tends to crash the app
        /// Put count in just for this. Generally if this happens, the application will crash, this just can't be caught by an exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAppList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (_dataErrorCount < 1)
            {
                MessageBox.Show("VRKiosk DataGrid Error: \n\n" + e.Exception.Message, "ERROR - VRKiosk DataGrid Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _dataErrorCount++;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (_editCategoryMode == false)
            {
                SetEditMode(true);
            }

            if (dgvCategories.DataSource is List<AppListItemCategory>)
            {
                List<AppListItemCategory> currentCategories = (List<AppListItemCategory>)dgvCategories.DataSource;
                int maxCategoryNum = currentCategories.Max(x => x.CategoryNum);

                AppListItemCategory newCategory = new AppListItemCategory();
                newCategory.CategoryName = "";
                newCategory.CategoryNum = maxCategoryNum + 1;
                newCategory.CategorySelected = false;

                // Refresh the datagridview
                currentCategories.Add(newCategory);
                dgvCategories.DataSource = null;
                CategoriesDSProperties(currentCategories, false);
                dgvCategories.Refresh();

                // Add to categories to save
                _newCategoriesToSave.Add(newCategory);
            }

            // Todo:
            // (x) - Get max value of categories currently in XML & increment for add new
            // (x) - Create global list to keep track of new categories added to 1 app
            // (x) - Disable the rest of the U.I. until categories saved, this is more clear
            // (x) - Save categories bubbles changes (added) to rest of categories
            // (x) - Save also potentially edits the XML file, may be neeeded at that step
            // (x) - Ensure rest of apps get new category and on new load the category is set
            // (x) - double-click edit goes into edit mode - monitor this as an event - add discard changes button - maybe add "edit" categories button instead
            // (x) - if added button, search for readonly property and reset it to that
            // () - 
        }

        private void SetEditMode(bool editModeSet)
        {
            _editCategoryMode = editModeSet;
            btnSaveCategories.Enabled = editModeSet;
            btnDiscardCatChgs.Enabled = editModeSet;
            btnDeleteCategory.Enabled = editModeSet;

            if (editModeSet)
            {
                grpFilterCategories.BackColor = Color.LightGreen;
                grpFilterCategories.Text = "*Editing Categories*";

                if (dgvCategories.DataSource is List<AppListItemCategory> && _origSelectedCategory.Count == 0)
                {
                    List<AppListItemCategory> currentCategories = (List<AppListItemCategory>)dgvCategories.DataSource;

                    foreach (AppListItemCategory appCat in currentCategories)
                    {
                        AppListItemCategory appCatNew = new AppListItemCategory();
                        appCatNew.CategoryName = appCat.CategoryName;
                        appCatNew.CategoryNum = appCat.CategoryNum;
                        appCatNew.CategorySelected = appCat.CategorySelected;
                        _origSelectedCategory.Add(appCatNew);
                    }
                }
            }
            else
            {
                grpFilterCategories.BackColor = Color.Transparent;
                grpFilterCategories.Text = "Categories for selected app";
            }

            // If edit mode is set, disable controls (!true)
            foreach (Control ctrl in grpAppsList.Controls)
            {
                ctrl.Enabled = !editModeSet;
            }
            foreach (Control ctrl in grpAppsActions.Controls)
            {
                ctrl.Enabled = !editModeSet;
            }
            foreach (Control ctrl in grpEditApp.Controls)
            {
                ctrl.Enabled = !editModeSet;
            }
        }

        private void btnSaveCategories_Click(object sender, EventArgs e)
        {
            if (dgvCategories.DataSource is List<AppListItemCategory>)
            {
                List<AppListItemCategory> allCategoryEdits = ((List<AppListItemCategory>)dgvCategories.DataSource);
                List<AppListItemCategory> categoryEditsOnly = (from allEdits in allCategoryEdits
                                                               where !(from newItems in _newCategoriesToSave
                                                                        select newItems.CategoryNum)
                                                                        .Contains(allEdits.CategoryNum)
                                                                select allEdits).ToList();

                foreach (AppListItem app in _appListItems)
                {               
                    // Save the new categories
                    foreach (AppListItemCategory newCategory in _newCategoriesToSave)
                    {
                        if (!app.Categories.Contains(newCategory))
                        {
                            AppListItemCategory tmp = new AppListItemCategory();
                            tmp.CategoryName = newCategory.CategoryName;
                            tmp.CategoryNum = newCategory.CategoryNum;
                            tmp.CategorySelected = false;

                            // Add copy of the object
                            app.Categories.Add(tmp);
                        }
                    }

                    // Save edits to the current ones
                    foreach (AppListItemCategory editedCategory in categoryEditsOnly)
                    {
                        foreach (AppListItemCategory oldCategory in app.Categories)
                        {
                            if (oldCategory.CategoryNum == editedCategory.CategoryNum)
                            {
                                oldCategory.CategoryName = editedCategory.CategoryName;
                            }
                        }
                    }

                    // Save deletes
                    foreach (AppListItemCategory deletedCatgory in _deletedCategories)
                    {
                        //foreach (AppListItemCategory appCategory in app.Categories)
                        for (int i=0; i < app.Categories.Count; i++)
                        {
                            AppListItemCategory appCategory = app.Categories[i];
                            if (appCategory.CategoryNum == deletedCatgory.CategoryNum)
                            {
                                // Remove the category
                                app.Categories.RemoveAt(i);
                            }

                            // Check if there are any selected catogires
                            int countSelectedCatgories = (from countSelected in app.Categories
                                                          where countSelected.CategorySelected
                                                          select countSelected).ToList().Count;

                            // Select the top one of there are no more selected
                            if (countSelectedCatgories == 0)
                            {
                                app.Categories[0].CategorySelected = true;
                            }
                        }
                    }
                }


                // Update the XML for the categories (no save to XML file till user says save to whole program)
                // Example XML:
                // <filterCategories>
                //     <filterCategory name = "Demo" id = "0" />
                //     <filterCategory name = "Experience" id = "1" />
                // </ filterCategories>
                List<XElement> editCategoriesXlements = new List<XElement>();
                foreach (AppListItemCategory categoryEdit in allCategoryEdits)
                {
                    XElement xmlFilter = new XElement("filterCategory");
                    xmlFilter.SetAttributeValue("name", categoryEdit.CategoryName);
                    xmlFilter.SetAttributeValue("id", categoryEdit.CategoryNum.ToString());
                    editCategoriesXlements.Add(xmlFilter);
                }
                //_appFiltersFromXml = _appFiltersFromXml.Concat(newCategoriesXlements);
                _appFiltersFromXml = editCategoriesXlements;

                // Refresh the main datagridview and turn edit mode off
                RefreshDataGridViewMain();
                _newCategoriesToSave.Clear();
                _origSelectedCategory.Clear();
                _deletedCategories.Clear();
                SetEditMode(false);
                dgvCategories.Columns["CategoryName"].ReadOnly = true;
            }
        }

        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_editCategoryMode == false)
            {
                SetEditMode(true);

                if (dgvCategories.DataSource is List<AppListItemCategory>)
                {
                    List<AppListItemCategory> currentCategories = (List<AppListItemCategory>)dgvCategories.DataSource;
                    CategoriesDSProperties(currentCategories, false);
                }
            }
        }

        private void btnDiscardCatChgs_Click(object sender, EventArgs e)
        {
            // Refresh the main datagridview and turn edit mode off
            RefreshDataGridViewMain();
            _newCategoriesToSave.Clear();
            SetEditMode(false);

            DataGridViewRow selectedRow = dgvAppList.SelectedRows[0];
            AppListItem selectedApp = (AppListItem)selectedRow.DataBoundItem;

            dgvCategories.DataSource = null;
            selectedApp.Categories.Clear();

            foreach (AppListItemCategory cat in _origSelectedCategory)
            {
                selectedApp.Categories.Add(cat);
            }

            CategoriesDSProperties(selectedApp.Categories, true);
            _origSelectedCategory.Clear();

        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            //dgvCategories
            // if (dgvCategories.CurrentRow.Selected)
            //if (dgvCategories.Rows.Count > 0)
            //DataGridViewRow selectedRow = dgvAppList.SelectedRows[0];
            //AppListItemCategory selectedCategory = (AppListItemCategory)selectedRow.DataBoundItem;
            // look at SelectAppInDataGrid() if need more

            if (dgvCategories.CurrentRow.Selected)
            {
                if (dgvCategories.Rows.Count > 1)
                {
                    DataGridViewRow selectedRow = dgvCategories.SelectedRows[0];
                    AppListItemCategory selectedCategory = (AppListItemCategory)selectedRow.DataBoundItem;
                    _deletedCategories.Add(selectedCategory);

                    if (dgvCategories.DataSource is List<AppListItemCategory>)
                    {
                        List<AppListItemCategory> currentCategories = (List<AppListItemCategory>)dgvCategories.DataSource;
                        currentCategories.Remove(selectedCategory);
                        dgvCategories.DataSource = null;
                        CategoriesDSProperties(currentCategories, false);
                        dgvCategories.Refresh();
                    }

                }
                else
                {
                    MessageBox.Show("At least 1 category is required.\nRename the top category rather than deleting", "Edit Categories - Delete blocked", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }



            //string strTest = "Current row selected: " + dgvCategories.CurrentRow.Selected.ToString();
            //strTest += "\nCount of categories: " + dgvCategories.Rows.Count.ToString();
            //strTest += "\nSelected row type: " + selectedRow.DataBoundItem.GetType().ToString();
            //MessageBox.Show("Testing: " + strTest);
        }

        public string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = System.IO.Path.GetFileName(shortcutFilename);
            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            if (folder == null)
            {
            }
            else
            {
                FolderItem folderItem = folder.ParseName(filenameOnly);
                if (folderItem != null)
                {
                    Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                    return link.Path;
                }
            }
            return string.Empty;
        }

        private void picForSelApp_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void picForSelApp_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (FileList.Length > 0)
            {
                string fileName = FileList[0];

                if (fileName.Contains(".png") || fileName.Contains(".jpg") || fileName.Contains(".bmp") || fileName.Contains(".exe"))
                {
                    ChangeSelectedImage(fileName);
                }
            }
        }

        private void dgvAppList_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (FileList.Length > 0)
            {
                foreach (string fileName in FileList)
                {
                    string fullFileName = fileName;
                    if (fileName.Contains(".lnk"))
                    {
                        fullFileName = GetShortcutTargetFile(fileName);
                    }

                    if (fullFileName.Contains(".exe"))
                    {
                        AddNewApp(fullFileName);
                    }
                }
            }
        }

        private void dgvAppList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
