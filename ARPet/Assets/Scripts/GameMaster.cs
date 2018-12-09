using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region VARIABLES

    private Vector2 currentPlaneSize;
    private Vector2 buildArea = new Vector2(1f, 1f);
    
    private HorizontalPlane arHorizontalPlane;
    
    private List<ARPlane> newPlanes = new List<ARPlane>();
    public ARPlane currentPlane;

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
        DrawNewARHorizontalPlane();

        if(arHorizontalPlane != null)
        World.MoveWorld(arHorizontalPlane.CenterPose.position);
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

    //public void CheckCanWeBuild(Mesh mesh, Pose pose)
    //{
    //    currentPlaneSize = new Vector2(mesh.bounds.size.x, mesh.bounds.size.z);

    //    if (currentPlaneSize.x >= buildArea.x || currentPlaneSize.y >= buildArea.y)
    //    {

    //    }
    //}

    private void CreateWorld(Vector3 centerPosition, Quaternion rotation)
    {
        World = Instantiate(ResourceManager.Instance.WorldObjectPrefab, centerPosition, rotation).GetComponent<World>();
        World.Initialize(ARSession.AddAnchor(arHorizontalPlane.CenterPose));
        Instantiate(ResourceManager.Instance.AnchorPrefab, arHorizontalPlane.CenterPose.position, arHorizontalPlane.CenterPose.rotation);

        IsWorldCreated = true;

        //arHorizontalPlane.Creat

        //world.gameObject.SetActive(false);
    }

    private void DrawNewARHorizontalPlane()
    {
        newPlanes.Clear();
        ARFrame.GetTrackables(newPlanes, ARTrackableQueryFilter.NEW);

        if(newPlanes.Count > 0)
        currentPlane = newPlanes[0];

        for (int i = 0; i < newPlanes.Count; i++)
        {
            arHorizontalPlane = Instantiate(ResourceManager.Instance.HorizontalPlanePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<HorizontalPlane>();
            arHorizontalPlane.Initialize(newPlanes[i]);

            // !!!
            if(IsWorldCreated == false)
            {
                CreateWorld(arHorizontalPlane.CenterPose.position, arHorizontalPlane.CenterPose.rotation);
            }
        }
    }

    private void CheckBuildArea(Bounds areaBounds)
    {
        //if(arHorizontalPlane.PlaneBounds.)
    }

    #endregion CUSTOM_FUNCTIONS
}