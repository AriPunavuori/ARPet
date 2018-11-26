using System;
using UnityEngine;

public class EatState : IState
{
    private float eatTime;
    private Material material;
    private Color defaultColor;
    private Action OnEatStateEnd;

    public EatState(float eatTime, Material material, Color defaultColor, Action OnEatStateEnd)
    {
        this.eatTime = eatTime;
        this.material = material;
        this.defaultColor = defaultColor;
        this.OnEatStateEnd = OnEatStateEnd;
    }

    public void Enter()
    {
        material.color = Color.red;
    }

    public void Execute()
    {
        eatTime -= Time.deltaTime;

        if(eatTime <= 0f)
        {
            OnEatStateEnd();
        }
    }

    public void Exit()
    {
        material.color = defaultColor;
    }
}
