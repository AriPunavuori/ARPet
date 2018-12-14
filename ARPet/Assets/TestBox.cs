using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestBox : MonoBehaviour
{
    public Vector3 TargetDestination = Vector3.zero;

    public NavMeshSurface NavMeshSurface;

    [SerializeField]
    private NavMeshAgent NavMeshAgent;

    private void Awake()
    {
        NavMeshSurface.BuildNavMesh();
        //NavMeshSurface.
    }

    private void Update()
    {
        NavMeshAgent.SetDestination(TargetDestination);
    }
}
