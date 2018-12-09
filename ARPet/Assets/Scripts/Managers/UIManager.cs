using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

    [SerializeField]
    private Text poseText;
    [SerializeField]
    private Text planesText;
    [SerializeField]
    private Text deviceText;
    [SerializeField]
    private Text hitObjectText;
    [SerializeField]
    private Text hitDistanceText;
    [SerializeField]
    private Text hitPoseText;

    [SerializeField]
    private Text touchPhaseText;
    [SerializeField]
    private Text planeAnchorsText;
    [SerializeField]
    private Text isWorldCreatedText;

    private Text currentStateText;
    private Text previousStateText;
    private Text mainTaskText;
    private Text secondaryTaskText;

    private Image happinessBar;
    private Image sleepinessBar;
    private Image energinesBar;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform DebugBox
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

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Start()
    {
        Initialize();
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.normal.background = null;
        guiStyle.normal.textColor = new Color(1, 0, 0);
        guiStyle.fontSize = 45;

        GUI.Label(new Rect(0, Screen.height - 100, 200, 200), SessionManager.Instance.ErrorMessage, guiStyle);
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        DebugBox = GameMaster.Instance.HUDCanvas.Find("DebugBox");
        PetStates = DebugBox.Find("PetStates");
        PetStats = DebugBox.Find("PetStats");

        currentStateText = PetStates.Find("CurrentStateText").GetComponent<Text>();
        previousStateText = PetStates.Find("PreviousStateText").GetComponent<Text>();
        mainTaskText = PetStates.Find("MainTaskText").GetComponent<Text>();
        secondaryTaskText = PetStates.Find("SecondaryTaskText").GetComponent<Text>();

        happinessBar = PetStats.Find("Happiness").GetComponentInChildren<Image>();
        sleepinessBar = PetStats.Find("Sleepiness").GetComponentInChildren<Image>();
        energinesBar = PetStats.Find("Energines").GetComponentInChildren<Image>();
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

    public void QuitButton(float delay)
    {
        Invoke("OnQuitPressed", delay);
    }

    public void UpdateDebugTexts(
        string poseText, 
        string planesText,
        string deviceText,
        string hitObjectText,
        string hitDistanceText, 
        string hitPoseText, 
        string touchPhaseText, 
        string planeAnchorsText)
    {        
        this.poseText.text = "Pose: " + poseText;
        this.planesText.text = "Planes: " + planesText;
        this.deviceText.text = "Device: " + deviceText;
        this.hitObjectText.text = "HitObject: " + hitObjectText;
        this.hitDistanceText.text = "Hit distance: " + hitDistanceText;
        this.hitPoseText.text = "Hit pose: " + hitPoseText;
        this.touchPhaseText.text = "Touch phase: " + touchPhaseText;
        this.planeAnchorsText.text = "Plane anchors: " + planeAnchorsText;
        isWorldCreatedText.text = "Is world created: " + GameMaster.Instance.IsWorldCreated.ToString();
}

    #endregion CUSTOM_FUNCTIONS
}
