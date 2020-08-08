<<<<<<< HEAD:Assets/Scripts/Managers/WorldManager.cs
﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : Singelton<WorldManager>
{
    #region VARIABLES

    private List<GameObject> createdObjects = new List<GameObject>();

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

        SessionManager.Instance.ClearAndRemoveHorizontalPlanes();

        UIManager.Instance.SwitchDeviceImage(false);
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
=======
﻿using System.Collections.Generic;
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
>>>>>>> c5991e646145c1830fee939d0e66a147eea824cc:ARPet/Assets/Scripts/Managers/WorldManager.cs
