using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region VARIABLES

    private GameObject floorPrefab, petPrefab;

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
        floorPrefab = Resources.Load<GameObject>("Prefabs/Floor");
        petPrefab = Resources.Load<GameObject>("Prefabs/Pet");
    }

    private void CreateLevel()
    {
        Instantiate(floorPrefab);
        Instantiate(petPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
    }

    #endregion CUSTOM_FUNCTIONS
}
