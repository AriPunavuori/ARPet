using System.Collections.Generic;
using UnityEngine;

public class WorldManager : Singelton<WorldManager>
{
    #region VARIABLES

    #endregion VARIABLES

    #region PROPERTIES

    public bool IsWorldCreated
    {
        get;
        private set;
    }
    public World World
    {
        get;
        private set;
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Start()
    {
        Initialize();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        //CreateWorld(new Pose(Vector3.zero, Quaternion.identity));
    }

    public void CreateWorld(Pose newPose)
    {
        if (IsWorldCreated)
        {
            return;
        }

        World = Instantiate(ResourceManager.Instance.WorldObjectPrefab, newPose.position, newPose.rotation).GetComponent<World>();
        World.Initialize(SessionManager.Instance.CreateAnchor(newPose));
        IsWorldCreated = true;

        //Instantiate(ResourceManager.Instance.BlockPrefab, newPose.position + Vector3.up, Quaternion.identity);

        //SessionManager.Instance.ClearAndRemoveHorizontalPlanes();
        SessionManager.Instance.SetPlaneFindingMode(0);

        //UIManager.Instance.SwitchDeviceImage(false);
    }

   

  

    #endregion CUSTOM_FUNCTIONS
}
