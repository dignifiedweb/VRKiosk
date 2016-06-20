using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; 

public class VRKioskGUIMain : MonoBehaviour
{
    // game objects expected to be populated through inspector
    public Image _gameImage;
    public Text _nameOfGame;
    public Text _gameCountDisplay;
    public Button _leftButton;
    public Button _rightButton;
    public Button _playButton;
    public Text _notesText;
    public Canvas _fadeOutAndExitCanvas;
    public GameObject _categoriesPanel;
    public GameObject _categoriesToggleButtonPrefab;
    public OVRManager _ovrManager;
    public OVRCameraRig _ovrCameraRig;
    public Sprite _loadingImage;

    // xml config document
    private XmlNode _rootElementOfXmlConfig;
    private XmlNodeList _appsXMLNodeList;

    private List<VRKioskAppListItem> _vrKioskAppListItems;

    private List<string> _filtersAppliedList;
    private List<GameObject> _categoryToggleGameObjList;
    private List<string> _categoryToggleIdList;

    // Current game to launch and referece to lauch CLI class
    private int _currentGame;
    private VRKioskLaunchApp _launchGame;
    private string _workingDirOfLauncher;

    private bool _btnsEnabledAfterHSW;

    private bool _xinputPlayerIndexSet = false;
    private PlayerIndex _xinputPlayerIndex;
    private GamePadState _gamepadState;
    private GamePadState _prevGamepadState;
    private bool _gamepadButtonPressed = false;


    // Use this for initialization
    void Start ()
    {
        _workingDirOfLauncher = Path.GetFullPath(".");

        XmlDocument xmlVRKioskConfig = new XmlDocument();
        xmlVRKioskConfig.Load(_workingDirOfLauncher + "\\VRKiosk_Config.xml");

        // Query XML document with xpath to get apps
        _rootElementOfXmlConfig = xmlVRKioskConfig.DocumentElement;
        _appsXMLNodeList = _rootElementOfXmlConfig.SelectNodes("apps/app");

        // Intitialize the list of apps
        _vrKioskAppListItems = new List<VRKioskAppListItem>();
        _filtersAppliedList = new List<string>();

        // Lists for categories
        _categoryToggleGameObjList = new List<GameObject>();
        _categoryToggleIdList = new List<string>();

        // Filters to be applied to adding games (all games will require at least 1 category)
        loadFilterCategories();

        // Apply default filter and load all games lists
        applyFilterToAllLists();

        // Set first image and name
        _currentGame = 0;
        if (_vrKioskAppListItems.Count > 0)
        {
            _gameImage.sprite = _vrKioskAppListItems[_currentGame].Image;
            _nameOfGame.text = _vrKioskAppListItems[_currentGame].Name;
            _notesText.text = _vrKioskAppListItems[_currentGame].Notes;
        }
        UpdateGameCountText();

        // Instantiate LaunchGame object reference:
        _launchGame = new VRKioskLaunchApp();

        // Play button is disabled until HSW (Health and Safety Warning) is gone
        _btnsEnabledAfterHSW = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Enable buttons after health and safety warning is gone
        if (OVRManager.isHSWDisplayed == false && _btnsEnabledAfterHSW == false)
        {
            _playButton.interactable = true;
            _btnsEnabledAfterHSW = true;
        }

        // Game running, show loading screen, disable play button
        if (_launchGame.ProcessRunning != null)
        {
            if (_launchGame.ProcessRunning.HasExited == false && _launchGame.LaunchedGame == true)
            {
                _gameImage.sprite = _loadingImage;
                _playButton.interactable = false;
            }
            else if (_launchGame.ProcessRunning.HasExited)
            {
                if (_launchGame.LaunchedGame == true)
                {
                    applyFilterBasedOnCheckedButtons();
                    _launchGame.LaunchedGame = false;
                    _playButton.interactable = true;
                }
            }
        }

        // XInput dll
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!_xinputPlayerIndexSet || !_prevGamepadState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex xinputPlayerIndex = (PlayerIndex)i;
                GamePadState gamepadState = GamePad.GetState(xinputPlayerIndex);
                if (gamepadState.IsConnected)
                {
                    //Debug.Log(string.Format("XInput GamePad found {0}", xinputPlayerIndex));
                    this._xinputPlayerIndex = xinputPlayerIndex;
                    _xinputPlayerIndexSet = true;
                }
            }
        }

        if (_xinputPlayerIndexSet)
        {
            _prevGamepadState = _gamepadState;
            _gamepadState = GamePad.GetState(_xinputPlayerIndex);

            if (!_prevGamepadState.Equals(_gamepadState))
            {
                // back button - re-center display for oculus
                // Detect if a button was released this frame
                if (_prevGamepadState.Buttons.Back == ButtonState.Pressed && _gamepadState.Buttons.Back == ButtonState.Released)
                {
                    if (_gamepadButtonPressed)
                    {
                        Debug.Log("XInput - back Button released");
                        OVRManager.display.RecenterPose();
                    }
                    else
                    {
                        _gamepadButtonPressed = true;
                    }
                }

                // DPad Left button
                if (_prevGamepadState.DPad.Left == ButtonState.Released && _gamepadState.DPad.Left == ButtonState.Pressed)
                {
                    Debug.Log("XInput - dpad left pressed");
                    DpadLeftButtonPressed();
                }

                // DPad Right button
                if (_prevGamepadState.DPad.Right == ButtonState.Released && _gamepadState.DPad.Right == ButtonState.Pressed)
                {
                    Debug.Log("XInput - dpad right pressed");
                    DpadRightButtonPressed();
                }
            }
        }


        // Exit VRKiosk GUI by pressing Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    /// <summary>
    /// Loads the filter categories.
    /// </summary>
    private void loadFilterCategories()
    {
        XmlNodeList filterCategoriesNodes = _rootElementOfXmlConfig.SelectNodes("filterCategories/filterCategory");

        for (int i = 0; i < filterCategoriesNodes.Count; i++)
        {
            XmlNode filter = filterCategoriesNodes[i];
            string filterId = filter.Attributes["id"].Value.ToString();
            string filterName = filter.Attributes["name"].Value.ToString();

            // Load the Filter Categories list w/ all categories (default checked):
            _filtersAppliedList.Add(filterId);

            // Keep track of the ids as they relate to the toggle buttons:
            _categoryToggleIdList.Add(filterId);

            // Add Toggle buttons to scene and keep reference to the buttons in a list
            _categoryToggleGameObjList.Add(createCategoryToggleButton(filterName));
        }
    }

    /// <summary>
    /// Applies the filter to all lists.
    /// </summary>
    private void applyFilterToAllLists()
    {
        // Go through all apps listed
        for (int i = 0; i < _appsXMLNodeList.Count; i++)
        {
            XmlNode app = _appsXMLNodeList[i];
            XmlNodeList filtersForApp = app.SelectNodes("filters/filter");
            XmlNode notesForApp = app.SelectSingleNode("notes");

            // Check if this app has filters that are currently selected
            int foundCount = 0;
            for (int y = 0; y < filtersForApp.Count; y++)
            {
                XmlNode appFilter = filtersForApp[y];
                string filterValue = appFilter.InnerText;

                if (_filtersAppliedList.Contains(filterValue))
                {
                    foundCount++;
                }
            }

            // Only show app if filters are applicable
            if(foundCount > 0)
            {
                VRKioskAppListItem vrApp = new VRKioskAppListItem();

                // Set the sprite array value
                Sprite sprite = GetSpriteFromFilename(app.Attributes["imgFilename"].Value);
                vrApp.Image = sprite;

                // Set the name of the game array value
                vrApp.Name = app.Attributes["name"].Value.ToString();

                // Set the filepath list (names of executables to launch)
                vrApp.FilePath = app.Attributes["fullFilePath"].Value.ToString();

                // Set the params list
                vrApp.Parameters = app.Attributes["paramsCustom"].Value.ToString();

                // Set sendkeys for dialog windows
                vrApp.SendKeysForDialog = app.Attributes["sendKeysForDialog"].Value.ToString();

                // Set sendkeys for pressing 360 controller "A" button
                vrApp.SendKeysForAButton = app.Attributes["sendKeysForAButton"].Value.ToString();

                // Set notes for games
                vrApp.Notes = notesForApp.InnerText;

                // Add one to found count if it's been found, don't add it again
                foundCount++;

                // Add vr app to list
                _vrKioskAppListItems.Add(vrApp);
            }

        }
    }

    private void UpdateGameCountText()
    {
        // Setup the game count text
        _gameCountDisplay.text = "(" + (_currentGame + 1) + "/" + _vrKioskAppListItems.Count.ToString() + ")";
    }

    /// <summary>
    /// Creates the category toggle button.
    /// </summary>
    /// <param name="categoryName">Category name.</param>
    private GameObject createCategoryToggleButton(string categoryName)
    {
        // Working adding a Toggle programatically using the prefab
        GameObject toggleButton = Instantiate(_categoriesToggleButtonPrefab) as GameObject;
        toggleButton.transform.SetParent(_categoriesPanel.transform, false);

        Text textForButton = toggleButton.GetComponentInChildren<Text>();
        textForButton.text = categoryName;

        Toggle toggleButtonEvent = toggleButton.GetComponentInChildren<Toggle>();
        toggleButtonEvent.onValueChanged.AddListener(toggleButton_onValueChanged);

        // Activate the toggle button (put it in the scene)
        toggleButton.SetActive(true);

        return toggleButton;
    }

    /// <summary>
    /// Event listener for toggle button checked/unchecked
    /// </summary>
    /// <param name="value">Value.</param>
    public void toggleButton_onValueChanged(bool flag)
    {
        applyFilterBasedOnCheckedButtons();
    }

    private void applyFilterBasedOnCheckedButtons()
    {
        _filtersAppliedList.Clear();
        _vrKioskAppListItems.Clear();

        // Get the filters which are toggled on:
        getCheckedFilters();

        if (_filtersAppliedList.Count > 0)
        {
            // re-load all of the link lists with all games under that category
            applyFilterToAllLists();

            // Set first image and name
            _gameImage.sprite = _vrKioskAppListItems[0].Image;
            _nameOfGame.text = _vrKioskAppListItems[0].Name;
            _notesText.text = _vrKioskAppListItems[0].Notes;
            _currentGame = 0;
        }
        else
        {
            Sprite catErrorSprite = (Sprite)Resources.Load("images/categoryEmptyImg", typeof(Sprite));

            // Set the 
            _gameImage.sprite = catErrorSprite;
            _nameOfGame.text = "please select a category from left hand side";
            _notesText.text = "You have unchecked all categories or there may be a problem with the XML config. All games need at least 1 category.";
            _currentGame = -1;
        }

        UpdateGameCountText();
    }

    /// <summary>
    /// Checks to see which filters to apply based on toggle buttons
    /// </summary>
    private void getCheckedFilters()
    {
        // Go through every toggle and check if they are checked
        for (int i = 0; i < _categoryToggleGameObjList.Count; i++)
        {
            GameObject obj = _categoryToggleGameObjList[i];
            Toggle toggleButton = obj.GetComponentInChildren<Toggle>();

            // If the toggle button is checked, add the filter to the list again
            if (toggleButton.isOn)
            {
                _filtersAppliedList.Add(_categoryToggleIdList[i]);
            }
        }
    }

    private Sprite GetSpriteFromFilename(string file)
    {
        if (file.Substring(0, 10).Equals("gameImages"))
        {
            file = _workingDirOfLauncher + "\\" + file;
        }

        Texture2D loadedTexture = LoadImageFromFile(file);
        Rect rectOfLoadedTex = new Rect(0, 0, loadedTexture.width, loadedTexture.height);
        Sprite spriteLoaded = Sprite.Create(loadedTexture, rectOfLoadedTex, new Vector2(0.5f, 0.5f));
        return spriteLoaded;
    }

    // LoadImageFromFile - method taken from the following URL:
    // http://answers.unity3d.com/questions/432655/loading-texture-file-from-pngjpg-file-on-disk.html
    // loads an image file (PNG, jpg, etc) and returns a Texture2D object for unity to use
    private Texture2D LoadImageFromFile(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    public void NextGame()
    {
        if (_currentGame < (_vrKioskAppListItems.Count - 1))
        {
            _currentGame++;
        }
        else
        {
            _currentGame = 0;
        }
        _gameImage.sprite = _vrKioskAppListItems[_currentGame].Image;
        _nameOfGame.text = _vrKioskAppListItems[_currentGame].Name;
        _notesText.text = _vrKioskAppListItems[_currentGame].Notes;
        UpdateGameCountText();
    }

    public void PreviousGame()
    {
        if (_currentGame > 0)
        {
            _currentGame--;
        }
        else
        {
            _currentGame = (_vrKioskAppListItems.Count - 1);
        }
        _gameImage.sprite = _vrKioskAppListItems[_currentGame].Image;
        _nameOfGame.text = _vrKioskAppListItems[_currentGame].Name;
        _notesText.text = _vrKioskAppListItems[_currentGame].Notes;
        UpdateGameCountText();
    }

    /// <summary>
    /// Launches the current game.
    /// </summary>
    public void LaunchCurrentGame()
    {
        // Launch the current game selected
        _launchGame.LaunchSelectedGame(_vrKioskAppListItems[_currentGame].FilePath,
                                       _vrKioskAppListItems[_currentGame].Parameters,
                                       _vrKioskAppListItems[_currentGame].SendKeysForDialog,
                                       _vrKioskAppListItems[_currentGame].SendKeysForAButton);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        // Go to black facing exit
        _fadeOutAndExitCanvas.enabled = true;

        // TODO - clean this up

        // Exit application after 100 ms delay
        //Thread.Sleep(100); // TODO - Remove this - JCarewick - DEBUG
        Application.Quit();
    }

    public void DpadLeftButtonPressed()
    {
        PreviousGame();
        _leftButton.Select();
    }

    public void DpadRightButtonPressed()
    {
        NextGame();
        _rightButton.Select();
    }

}
