using HuaweiARUnitySDK;
using UnityEngine;

public class Pet : Singelton<Pet>
{
    #region PROPERTIES

    public PetAIController PetAIController { get; private set; }
    public PetGraphicsController PetGraphicsController { get; private set; }
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
        PetAIController = GetComponentInChildren<PetAIController>();
        PetGraphicsController = GetComponentInChildren<PetGraphicsController>();
    }

    #endregion CUSTOM_FUNCTIONS
}
