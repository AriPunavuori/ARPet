using UnityEngine;
using UnityEngine.AI;

public class TestPlayer : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 newDestination)
    {
        navMeshAgent.SetDestination(newDestination);
    }
}
