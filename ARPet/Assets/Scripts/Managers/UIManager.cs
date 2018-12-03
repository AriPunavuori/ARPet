using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

    public Text CanBuildText;
    public Text PlaneSizeText;

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

    //public string MainTaskText
    //{
    //    set
    //    {
    //        mainTaskText.text = "MainTask: " + "<color=yellow>" + value + "</color>";
    //    }
    //}
    //public string SecondaryTaskText
    //{
    //    set
    //    {
    //        secondaryTaskText.text = "SecondaryTask: " + "<color=yellow>" + value + "</color>";
    //    }
    //}

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

        GUI.Label(new Rect(0, Screen.height - 100, 200, 200), GameMaster.Instance.ErrorMessage, guiStyle);
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

    #endregion CUSTOM_FUNCTIONS
}
