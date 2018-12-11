using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singelton<LevelManager>
{
    #region VARIABLES

    private Pet pet;

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

    private void Start()
    {
        CreateWorld();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
       
    }

    public void CreateWorld()
    {
        World = Instantiate(ResourceManager.Instance.WorldObjectPrefab, GameMaster.Instance.ModelContainer).GetComponent<World>();
        //World.Initialize();
        IsWorldCreated = true;
    }

    public void BuildNavMesh(NavMeshSurface[] navMeshSurfaces)
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    public void CreatePet(Vector3 petStartPosition)
    {
        //pet = Instantiate(ResourceManager.Instance.PetPrefab, petStartPosition, Quaternion.identity).GetComponent<Pet>();
        //pet.PetAnchor = GameMaster.Instance.World.WorldAnchor;
    }

    #endregion CUSTOM_FUNCTIONS
}
