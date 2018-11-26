using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    #region VARIABLES

    private NavMeshSurface[] navMeshSurfaces;
    private GameObject floorPrefab, petPrefab, foodPrefab;
    private Transform modelContainer;

    #endregion VARIABLES

    #region PROPERTIES

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
    }

    private void CreateLevel()
    {
        CreatePrefabInstance(floorPrefab);

        BuildNavMesh();

        CreatePrefabInstance(petPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        CreatePrefabInstance(foodPrefab, new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-2, 4)));
    }

    private void CreatePrefabInstance(GameObject prefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            var newInstance = Instantiate(prefab, position, rotation);
            newInstance.transform.SetParent(modelContainer);
            newInstance.name = prefab.name;
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
