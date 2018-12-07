using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region VARIABLES

    private Vector2 currentPlaneSize;
    private Vector2 buildArea = new Vector2(1f, 1f);
    private GameObject arPalm;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform HUDCanvas { get; private set; }
    public Transform Managers { get; private set; }
    public Transform Others { get; private set; }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    protected override void Awake()
    {
        base.Awake();

        arPalm = ResourceManager.Instance.GetFromResources<GameObject>("Models", "ArPalm");

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

    public void CheckCanWeBuild(Mesh mesh, Pose pose)
    {
        currentPlaneSize = new Vector2(mesh.bounds.size.x, mesh.bounds.size.z);

        if (currentPlaneSize.x >= buildArea.x || currentPlaneSize.y >= buildArea.y)
        {

        }
    }

    private void CreateWorld(Vector3 centerPosition, Quaternion rotation)
    {

    }

    #endregion CUSTOM_FUNCTIONS
}