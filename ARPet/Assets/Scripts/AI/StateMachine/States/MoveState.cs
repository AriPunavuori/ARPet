using System;
using UnityEngine;

public class MoveState : IState
{
    private Transform objectToMove;
    private Vector3 targetPosition;

    private readonly Action OnDestinationReached;

    private bool isDestinationReached;

    public MoveState(Transform objectToMove, Vector3 targetPosition, Action OnDestinationReached)
    {
        this.objectToMove = objectToMove;
        this.targetPosition = targetPosition;

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

        if(objectToMove.position.x == targetPosition.x)
        {
            isDestinationReached = true;
            OnDestinationReached();
        }
    }

    public void Exit()
    {
   
    }
}
