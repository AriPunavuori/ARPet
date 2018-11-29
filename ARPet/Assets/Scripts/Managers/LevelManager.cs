using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singelton<LevelManager>
{
    #region VARIABLES

    private NavMeshSurface[] navMeshSurfaces;
    private GameObject floorPrefab, petPrefab, foodPrefab;
    private Transform modelContainer;

    #endregion VARIABLES

    #region PROPERTIES

    public Vector3 FloorSize
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

    private void Start()
    {
        CreateLevel();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        modelContainer = transform.Find("ModelContainer");

        floorPrefab = Resources.Load<GameObject>("Prefabs/Floor");
        petPrefab = Resources.Load<GameObject>("Prefabs/Pet");
        foodPrefab = Resources.Load<GameObject>("Prefabs/Food");

        FloorSize = new Vector3(floorPrefab.transform.localScale.x, 2, floorPrefab.transform.localScale.x) / 2;
    }

    private void CreateLevel()
    {
        CreatePrefabInstance(floorPrefab);

        BuildNavMesh();

        CreatePrefabInstance(petPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);

        CreatePrefabInstanceRandomPosition(foodPrefab, -4, 4, 5);
    }

    private void CreatePrefabInstance(GameObject prefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {     
        var newInstance = Instantiate(prefab, position, rotation);
        newInstance.transform.SetParent(modelContainer);
        newInstance.name = prefab.name;       
    }

    private void CreatePrefabInstanceRandomPosition(GameObject prefab, float min, float max, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CreatePrefabInstance(prefab, new Vector3(Random.Range(min, max), 0.5f, Random.Range(min, max)));
        }
    }

    private void BuildNavMesh()
    {
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
