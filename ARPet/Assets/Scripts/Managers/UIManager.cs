using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singelton<UIManager>
{
    #region VARIABLES

    private Text stateText;
    private Text mainTaskText;
    private Text secondaryTaskText;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform DebugBox
    {
        get;
        private set;
    }

    public string StateText
    {
        set
        {
            stateText.text = "State: " + value;
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
        stateText = DebugBox.Find("StateText").GetComponent<Text>();
        mainTaskText = DebugBox.Find("MainTaskText").GetComponent<Text>();
        secondaryTaskText = DebugBox.Find("SecondaryTaskText").GetComponent<Text>();
    }

    #endregion CUSTOM_FUNCTIONS
}
