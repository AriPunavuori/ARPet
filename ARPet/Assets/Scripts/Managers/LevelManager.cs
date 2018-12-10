using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singelton<LevelManager>
{
    #region VARIABLES

    private Pet pet;
    private NavMeshSurface[] navMeshSurfaces;

    #endregion VARIABLES

    #region PROPERTIES

    #endregion PROPERTIES

    public Transform ModelContainer
    {
        get;
        private set;
    }

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        ModelContainer = transform.Find("ModelContainer");
    }

    public void CreateLevel(Vector3 petStartPosition)
    {
        BuildNavMesh();

        CreatePet(petStartPosition);
    }

    private void BuildNavMesh()
    {
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    public void CreatePet(Vector3 petStartPosition)
    {
        pet = Instantiate(ResourceManager.Instance.PetPrefab, petStartPosition, Quaternion.identity).GetComponent<Pet>();
        pet.PetAnchor = GameMaster.Instance.World.WorldAnchor;
    }

    #endregion CUSTOM_FUNCTIONS
}
