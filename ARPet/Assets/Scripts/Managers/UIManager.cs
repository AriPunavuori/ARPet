using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

    private Text currentStateText;
    private Text previousStateText;
    private Text mainTaskText;
    private Text secondaryTaskText;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform DebugBox
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
        currentStateText = DebugBox.Find("CurrentStateText").GetComponent<Text>();
        previousStateText = DebugBox.Find("PreviousStateText").GetComponent<Text>();
        mainTaskText = DebugBox.Find("MainTaskText").GetComponent<Text>();
        secondaryTaskText = DebugBox.Find("SecondaryTaskText").GetComponent<Text>();
    }

    #endregion CUSTOM_FUNCTIONS
}
