using UnityEngine;
using UnityEngine.AI;

public class WorldTest : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    private NavMeshTestController NavMeshTestController;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void Init(NavMeshTestController navMeshTestController)
    {
        NavMeshTestController = navMeshTestController;
    }

    private void Start()
    {
        navMeshSurface.BuildNavMesh();

        NavMeshTestController.CreatePlayer();
    }
}
