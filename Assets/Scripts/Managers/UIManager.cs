<<<<<<< HEAD:Assets/Scripts/Managers/UIManager.cs
﻿using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

    [SerializeField]
    private Sprite devicePanSprite;
    [SerializeField]
    private Sprite deviceTapSprite;

    private Image deviceImage;

    private Text currentStateText;
    private Text previousStateText;
    private Text mainTaskText;
    private Text secondaryTaskText;

    private Image happinessBar;
    private Image sleepinessBar;
    private Image energinesBar;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform GameDebugBox
    {
        get;
        private set;
    }
    public Transform AudioDebugBox
    {
        get;
        private set;
    }
    public Transform ARSessionDebugBox
    {
        get;
        private set;
    }
    public Transform HuabotStates
    {
        get;
        private set;
    }
    public Transform HuabotStats
    {
        get;
        private set;
    }

    public Text ARSessionStatusText
    {
        get;
        private set;
    }
    public Text DevicePoseText
    {
        get;
        private set;
    }
    public Text ARErrorMessageText
    {
        get;
        private set;
    }
    public Text HitTargetText
    {
        get;
        private set;
    }
    public Text HitPositionText
    {
        get;
        private set;
    }
    public Text HitDistanceText
    {
        get;
        private set;
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateARSessionUI();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        deviceImage = GameMaster.Instance.HUDCanvas.Find("DeviceImage").GetComponent<Image>();

        InitializeGameDebugBox();
        InitializeARSessionDebugBox();
        InitializeAudioDebugBox();      
    }

    private void InitializeGameDebugBox()
    {
        GameDebugBox = GameMaster.Instance.HUDCanvas.Find("GameDebugBox");

        HuabotStates = GameDebugBox.Find("HuabotStates");
        HuabotStats = GameDebugBox.Find("HuabotStats");

        currentStateText = HuabotStates.Find("CurrentStateText").GetComponent<Text>();
        previousStateText = HuabotStates.Find("PreviousStateText").GetComponent<Text>();
        mainTaskText = HuabotStates.Find("MainTaskText").GetComponent<Text>();
        secondaryTaskText = HuabotStates.Find("SecondaryTaskText").GetComponent<Text>();

        happinessBar = HuabotStats.Find("Happiness").GetComponentInChildren<Image>();
        sleepinessBar = HuabotStats.Find("Sleepiness").GetComponentInChildren<Image>();
        energinesBar = HuabotStats.Find("Energines").GetComponentInChildren<Image>();
    }

    private void InitializeARSessionDebugBox()
    {
        ARSessionDebugBox = GameMaster.Instance.HUDCanvas.Find("ARSessionDebugBox");
        ARSessionStatusText = ARSessionDebugBox.Find("ARSessionStatus").GetComponent<Text>();
        ARErrorMessageText = ARSessionDebugBox.Find("ARErrorMessage").GetComponent<Text>();
        DevicePoseText = ARSessionDebugBox.Find("DevicePose").GetComponent<Text>();
        HitTargetText = ARSessionDebugBox.Find("HitTarget").GetComponent<Text>();
        HitPositionText = ARSessionDebugBox.Find("HitPosition").GetComponent<Text>();
        HitDistanceText = ARSessionDebugBox.Find("HitDistance").GetComponent<Text>();
    }

    private void InitializeAudioDebugBox()
    {
        AudioDebugBox = GameMaster.Instance.HUDCanvas.Find("AudioDebugBox");
    }

    private void OnQuitPressed()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdateUI(float currentHappiness, float currentSleepiness, float currentEnergines, string currentState, string previousState)
    {
        happinessBar.rectTransform.localScale = new Vector2(currentHappiness / 100f, 1);
        sleepinessBar.rectTransform.localScale = new Vector2(currentSleepiness / 100f, 1);
        energinesBar.rectTransform.localScale = new Vector2(currentEnergines / 100f, 1);

        currentStateText.text = "Current state: " + "<color=yellow>" + currentState + "</color>";
        previousStateText.text = previousStateText.text = "Previous state: " + "<color=yellow>" + previousState + "</color>";
    }

    public void UpdateARSessionUI()
    {
        ARSessionStatusText.text = "Session status: " + SessionManager.Instance.CurrentARSessionStatus;
        ARErrorMessageText.text = "Error: " + SessionManager.Instance.ErrorMessage;
        DevicePoseText.text = "Device pose: " + CameraEngine.Instance.CameraPose.ToString();
        HitTargetText.text = "Hit Target: " + InputManager.Instance.CurrentTrackableName;
        HitPositionText.text = "Hit position: " + InputManager.Instance.CurrentHitPoint;
        HitDistanceText.text = "Hit distance: " + InputManager.Instance.CurrentHitDistance;
    }

    public void QuitButton(float quitDelay)
    {
        Invoke("OnQuitPressed", quitDelay);
    }

    public void SpawnBlockButton()
    {
        InputManager.Instance.CanWeCreateBox();
    }

    /// <summary>
    ///  is ImageEnable = Set device image on/off, 
    ///  1 = Set device "pan" sprite,
    ///  2 = Set device "tap" sprite
    /// </summary>
    /// <param name="spriteIndex"></param>
    public void SwitchDeviceImage(bool isImageEnable, int spriteIndex = 0)
    {
        deviceImage.enabled = isImageEnable;

        switch (spriteIndex)
        {
            case 0:

                // Do nothing...

                break;

            case 1:

                deviceImage.sprite = devicePanSprite;

                break;

            case 2:

                deviceImage.sprite = deviceTapSprite;

                break;
       
            default:

                break;
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
=======
﻿using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

    [SerializeField]
    private Sprite devicePanSprite;
    [SerializeField]
    private Sprite deviceTapSprite;

    private Image deviceImage;

    private Text currentStateText;
    private Text previousStateText;
    private Text mainTaskText;
    private Text secondaryTaskText;

    private Image happinessBar;
    private Image sleepinessBar;
    private Image energinesBar;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform GameDebugBox
    {
        get;
        private set;
    }
    public Transform AudioDebugBox
    {
        get;
        private set;
    }
    public Transform ARSessionDebugBox
    {
        get;
        private set;
    }
    public Transform HuabotStates
    {
        get;
        private set;
    }
    public Transform HuabotStats
    {
        get;
        private set;
    }

    public Text ARSessionStatusText
    {
        get;
        private set;
    }
    public Text DevicePoseText
    {
        get;
        private set;
    }
    public Text ARErrorMessageText
    {
        get;
        private set;
    }
    public Text HitTargetText
    {
        get;
        private set;
    }
    public Text HitPositionText
    {
        get;
        private set;
    }
    public Text HitDistanceText
    {
        get;
        private set;
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateARSessionUI();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        deviceImage = GameMaster.Instance.HUDCanvas.Find("DeviceImage").GetComponent<Image>();

        InitializeGameDebugBox();
        InitializeARSessionDebugBox();
        InitializeAudioDebugBox();
    }

    private void InitializeGameDebugBox()
    {
        GameDebugBox = GameMaster.Instance.HUDCanvas.Find("GameDebugBox");

        HuabotStates = GameDebugBox.Find("HuabotStates");
        HuabotStats = GameDebugBox.Find("HuabotStats");

        currentStateText = HuabotStates.Find("CurrentStateText").GetComponent<Text>();
        previousStateText = HuabotStates.Find("PreviousStateText").GetComponent<Text>();
        mainTaskText = HuabotStates.Find("MainTaskText").GetComponent<Text>();
        secondaryTaskText = HuabotStates.Find("SecondaryTaskText").GetComponent<Text>();

        happinessBar = HuabotStats.Find("Happiness").GetComponentInChildren<Image>();
        sleepinessBar = HuabotStats.Find("Sleepiness").GetComponentInChildren<Image>();
        energinesBar = HuabotStats.Find("Energines").GetComponentInChildren<Image>();
    }

    private void InitializeARSessionDebugBox()
    {
        ARSessionDebugBox = GameMaster.Instance.HUDCanvas.Find("ARSessionDebugBox");
        ARSessionStatusText = ARSessionDebugBox.Find("ARSessionStatus").GetComponent<Text>();
        ARErrorMessageText = ARSessionDebugBox.Find("ARErrorMessage").GetComponent<Text>();
        DevicePoseText = ARSessionDebugBox.Find("DevicePose").GetComponent<Text>();
        HitTargetText = ARSessionDebugBox.Find("HitTarget").GetComponent<Text>();
        HitPositionText = ARSessionDebugBox.Find("HitPosition").GetComponent<Text>();
        HitDistanceText = ARSessionDebugBox.Find("HitDistance").GetComponent<Text>();
    }

    private void InitializeAudioDebugBox()
    {
        AudioDebugBox = GameMaster.Instance.HUDCanvas.Find("AudioDebugBox");
    }

    private void OnQuitPressed()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdateUI(float currentHappiness, float currentSleepiness, float currentEnergines, string currentState, string previousState)
    {
        happinessBar.rectTransform.localScale = new Vector2(currentHappiness / 100f, 1);
        sleepinessBar.rectTransform.localScale = new Vector2(currentSleepiness / 100f, 1);
        energinesBar.rectTransform.localScale = new Vector2(currentEnergines / 100f, 1);

        currentStateText.text = "Current state: " + "<color=yellow>" + currentState + "</color>";
        previousStateText.text = previousStateText.text = "Previous state: " + "<color=yellow>" + previousState + "</color>";
    }

    public void UpdateARSessionUI()
    {
        ARSessionStatusText.text = "Session status: " + SessionManager.Instance.CurrentARSessionStatus;
        ARErrorMessageText.text = "Error: " + SessionManager.Instance.ErrorMessage;
        DevicePoseText.text = "Device pose: " + CameraEngine.Instance.CameraPose.ToString();
        HitTargetText.text = "Hit Target: " + InputManager.Instance.CurrentTrackableName;
        HitPositionText.text = "Hit position: " + InputManager.Instance.CurrentHitPoint;
        HitDistanceText.text = "Hit distance: " + InputManager.Instance.CurrentHitDistance;
    }

    public void QuitButton(float quitDelay)
    {
        Invoke("OnQuitPressed", quitDelay);
    }

    //public void SpawnBlockButton()
    //{
    //    InputManager.Instance.CanWeCreateBox();
    //}

    /// <summary>
    ///  is ImageEnable = Set device image on/off, 
    ///  1 = Set device "pan" sprite,
    ///  2 = Set device "tap" sprite
    /// </summary>
    /// <param name="spriteIndex"></param>
    public void SwitchDeviceImage(bool isImageEnable, int spriteIndex = 0)
    {
        deviceImage.enabled = isImageEnable;

        switch (spriteIndex)
        {
            case 0:

                // Do nothing...

                break;

            case 1:

                deviceImage.sprite = devicePanSprite;

                break;

            case 2:

                deviceImage.sprite = deviceTapSprite;

                break;
       
            default:

                break;
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
>>>>>>> c5991e646145c1830fee939d0e66a147eea824cc:ARPet/Assets/Scripts/Managers/UIManager.cs
