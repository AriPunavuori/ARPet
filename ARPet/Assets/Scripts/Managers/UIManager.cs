using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

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
    public Transform PetStates
    {
        get;
        private set;
    }
    public Transform PetStats
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
    public Text TrackableText
    {
        get;
        private set;
    }
    public Text HitPoseText
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
        InitializeGameDebugBox();
        InitializeARSessionDebugBox();
        InitializeAudioDebugBox();      
    }

    private void InitializeGameDebugBox()
    {
        GameDebugBox = GameMaster.Instance.HUDCanvas.Find("GameDebugBox");
        PetStates = GameDebugBox.Find("PetStates");
        PetStats = GameDebugBox.Find("PetStats");

        currentStateText = PetStates.Find("CurrentStateText").GetComponent<Text>();
        previousStateText = PetStates.Find("PreviousStateText").GetComponent<Text>();
        mainTaskText = PetStates.Find("MainTaskText").GetComponent<Text>();
        secondaryTaskText = PetStates.Find("SecondaryTaskText").GetComponent<Text>();

        happinessBar = PetStats.Find("Happiness").GetComponentInChildren<Image>();
        sleepinessBar = PetStats.Find("Sleepiness").GetComponentInChildren<Image>();
        energinesBar = PetStats.Find("Energines").GetComponentInChildren<Image>();
    }

    private void InitializeARSessionDebugBox()
    {
        ARSessionDebugBox = GameMaster.Instance.HUDCanvas.Find("ARSessionDebugBox");
        ARSessionStatusText = ARSessionDebugBox.Find("ARSessionStatus").GetComponent<Text>();
        ARErrorMessageText = ARSessionDebugBox.Find("ARErrorMessage").GetComponent<Text>();
        DevicePoseText = ARSessionDebugBox.Find("DevicePose").GetComponent<Text>();
        TrackableText = ARSessionDebugBox.Find("Trackable").GetComponent<Text>();
        HitPoseText = ARSessionDebugBox.Find("HitPose").GetComponent<Text>();
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
        TrackableText.text = "Trackable: " + InputManager.Instance.CurrentTrackable;
        HitPoseText.text = "Hit pose: " + InputManager.Instance.CurrentHitPose;
        HitDistanceText.text = "Hit distance: " + InputManager.Instance.CurrentHitDistance;
    }

    public void QuitButton(float quitDelay)
    {
        Invoke("OnQuitPressed", quitDelay);
    }

    #endregion CUSTOM_FUNCTIONS
}
