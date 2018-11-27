using System;
using UnityEngine;

public class InteractState : IState
{
    private float interactionTime;
    private readonly Action OnInteractionStart, OnInteractFinished;

    public InteractState(
        float interactionTime,
        Color defaultColor,
        Action OnInteractionStart,
        Action OnInteractFinished)
    {
        this.interactionTime = interactionTime;
        this.OnInteractionStart = OnInteractionStart;
        this.OnInteractFinished = OnInteractFinished;
    }

    public void Enter()
    {
        OnInteractionStart();
    }

    public void Execute()
    {
        interactionTime -= Time.deltaTime;

        if(interactionTime <= 0f)
        {
            OnInteractFinished();
        }
    }

    public void Exit()
    {

    }
}
