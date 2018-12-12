using HuaweiARUnitySDK;
using UnityEngine;

public class Huabot : Singelton<Huabot>
{
    #region PROPERTIES

    public HuabotAIController PetAIController { get; private set; }
    public HuaGraphicsController HuabotGraphicsController { get; private set; }
    public ARAnchor PetAnchor { get; set; }
    public Pose PetPose { get; private set; }

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
        PetAIController = GetComponentInChildren<HuabotAIController>();
        HuabotGraphicsController = GetComponentInChildren<HuaGraphicsController>();
    }

    #endregion CUSTOM_FUNCTIONS
}
