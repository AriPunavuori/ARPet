using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    public GameObject Prefab;
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        navMeshSurface.BuildNavMesh();

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Instantiate(Prefab, Vector3.zero + Vector3.up, Quaternion.identity);
    }
}
