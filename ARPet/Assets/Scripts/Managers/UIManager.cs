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
    private Image energinessBar;

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

    public string CurrentStateText
    {
        set
        {
            currentStateText.text = "Current state: " + value;
        }
    }
    public string PreviousStateText
    {
        set
        {
            previousStateText.text = "Previous state: " + value;
        }
    }
    public string MainTaskText
    {
        set
        {
            mainTaskText.text = "MainTask: " + value;
        }
    }
    public string SecondaryTaskText
    {
        set
        {
            secondaryTaskText.text = "SecondaryTask: " + value;
        }
    }

    public float Happiness
    {
        get
        {
            return happinessBar.rectTransform.position.x;
        }
    }
    public float Sleepiess
    {
        get
        {
            return sleepinessBar.rectTransform.position.x;
        }
    }
    public float Energiness
    {
        get
        {
            return energinessBar.rectTransform.position.x;
        }
    }

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
        energinessBar = PetStats.Find("Energiness").GetComponentInChildren<Image>();
    }

    #endregion CUSTOM_FUNCTIONS
}
