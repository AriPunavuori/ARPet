using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region PROPERTIES

    public Transform HUDCanvas { get; private set; }
    public Transform Managers { get; private set; }
    public Transform Others { get; private set; }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        HUDCanvas = transform.Find("HUDCanvas");
        Managers = transform.Find("Managers");
        Others = transform.Find("Others");
    }

    #endregion CUSTOM_FUNCTIONS
}
