using HuaweiARUnitySDK;
using UnityEngine;

public class Huabot : Singelton<Huabot>
{
    #region PROPERTIES

    public HuabotAIController HuabotAIController { get; private set; }
    public HuabotGraphicsController HuabotGraphicsController { get; private set; }

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
        HuabotAIController = GetComponentInChildren<HuabotAIController>();
        HuabotGraphicsController = GetComponentInChildren<HuabotGraphicsController>();
    }

    #endregion CUSTOM_FUNCTIONS
}
