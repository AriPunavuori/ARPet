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

    private void Awake()
    {
        Initialize();
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

    public void UpdateUI(
        float currentHappiness, 
        float currentSleepiness, 
        float currentEnergines, 
        string currentState, 
        string previousState)
    {
        happinessBar.rectTransform.localScale = new Vector2(currentHappiness / 100f, 1);
        sleepinessBar.rectTransform.localScale = new Vector2(currentSleepiness / 100f, 1);
        energinesBar.rectTransform.localScale = new Vector2(currentEnergines / 100f, 1);

        currentStateText.text = "Current state: " + "<color=yellow>" + currentState + "</color>";
        previousStateText.text = previousStateText.text = "Previous state: " + "<color=yellow>" + previousState + "</color>";
    }

    #endregion CUSTOM_FUNCTIONS
}
