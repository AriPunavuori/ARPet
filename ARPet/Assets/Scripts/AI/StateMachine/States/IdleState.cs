using UnityEngine;

public class IdleState : IState
{
    private Vector2 randomMovementTimer;

    private float moveTimer;

    public IdleState(Vector2 randomMovementTimer)
    {
        this.randomMovementTimer = randomMovementTimer;
    }

    public void Enter()
    {
        moveTimer = Random.Range(randomMovementTimer.x, randomMovementTimer.y);
    }

    public void Execute()
    {   
        moveTimer -= Time.deltaTime;

        if(moveTimer <= 0f)
        {
            moveTimer = Random.Range(randomMovementTimer.x, randomMovementTimer.y);
            Debug.Log("!!!");
        }
    }

    public void Exit()
    {
        
    }
}
