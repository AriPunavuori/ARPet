using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : Singelton<WorldManager>
{
    #region VARIABLES

    private List<GameObject> createdObjects = new List<GameObject>();

    #endregion VARIABLES

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

    #region PROPERTIES

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
       
    }

    public void CreateWorld(Pose newPose)
    {
        if (IsWorldCreated)
        {
            return;
        }

        World = Instantiate(ResourceManager.Instance.WorldObjectPrefab, newPose.position, newPose.rotation).GetComponent<World>();
        //World.Initialize(SessionManager.Instance.CreateAnchor(newPose));
        IsWorldCreated = true;

        Instantiate(ResourceManager.Instance.HuabotPrefab, newPose.position + Vector3.up, Quaternion.identity);

        SessionManager.Instance.ClearHorizontalPlanes();

        UIManager.Instance.SwitchDeviceImage();
    }

    public void BuildNavMesh(NavMeshSurface[] navMeshSurfaces)
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    public void TryPlaceObject(GameObject selectedPrefab, Pose currentHitPose)
    {
        var newInstance = Instantiate(selectedPrefab, currentHitPose.position, currentHitPose.rotation);

        createdObjects.Add(newInstance);
    }

    #endregion CUSTOM_FUNCTIONS
}
