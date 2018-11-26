using System;
using UnityEngine;

public class MoveState : IState
{
    private Transform objectToMove;
    private Vector3 endPosition;
    private Material petMaterial;

    private readonly Action OnDestinationReached;

    private bool isDestinationReached;

    public MoveState(Transform objectToMove, Vector3 endPosition, Material petMaterial, Action OnDestinationReached)
    {
        this.objectToMove = objectToMove;
        this.endPosition = endPosition;
        this.petMaterial = petMaterial;

        this.OnDestinationReached = OnDestinationReached;
    }

    public void Enter()
    {
        isDestinationReached = false;
    }

    public void Execute()
    {
        if (isDestinationReached)
        {
            return;
        }

        if(objectToMove.position.x == endPosition.x)
        {
            isDestinationReached = true;
            OnDestinationReached();
        }
    }

    public void Exit()
    {
        Debug.Log("MovesState:: Exit()");
    }
}
