using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region VARIABLES

    #endregion VARIABLES

    #region PROPERTIES

    public Transform HUDCanvas { get; private set; }
    public Transform Managers { get; private set; }
    public Transform Others { get; private set; }

    public bool IsWorldCreated { get; private set; }
    public World World { get; private set; }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void Update()
    {
       
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        HUDCanvas = transform.Find("HUDCanvas");
        Managers = transform.Find("Managers");
        Others = transform.Find("Others");
    }

    private void CreateWorld(Vector3 centerPosition, Quaternion rotation)
    {

    }

    private void DrawNewARHorizontalPlane()
    {
       
    }

    #endregion CUSTOM_FUNCTIONS
}