<<<<<<< HEAD:Assets/Scripts/AI/StateMachine/States/RoamingState.cs
﻿using UnityEngine;

public class RoamingState : IState
{
    #region VARIABLES

    private Transform objectToMove;
    private Vector3 roamingDestination;
    private Vector3 roamingArea;

    private float randomIdleTime;
    private readonly float randomMinIdleTime = 0.5f;
    private readonly float randomMaxIdleTime = 4f;

    #endregion VARIABLES

    #region PROPERTIES

    public bool IsTimeToIdle
    {
        get
        {
            return randomIdleTime > 0f;
        }
    }

    #endregion PROPERTIES

    #region CONSTRUCTORS

    public RoamingState(Transform objectToMove ,Vector3 roamingArea)
    {
        this.objectToMove = objectToMove;
        this.roamingArea = roamingArea;
    }

    #endregion CONSTRUCTORS

    #region CUSTOM_FUNCTIONS

    private Vector3 RandomDestinationFromArea(Vector3 areaSize)
    {
        return new Vector3(
            Random.Range(-areaSize.x, areaSize.x),
            1,
            Random.Range(-areaSize.z, areaSize.z));
    }

    public void Enter()
    {
        SetNewRandomDestination();
    }

    public void Execute()
    {
        if(IsTimeToIdle)
        {
            randomIdleTime -= Time.deltaTime;
            return;
        }

        if (objectToMove.transform.position.x.Equals(roamingDestination.x) 
            && 
            objectToMove.transform.position.z.Equals(roamingDestination.z))
        {
            SetNewRandomDestination();
        }
    }

    public void Exit()
    {
        
    }

    private void SetNewRandomDestination()
    {
        randomIdleTime = Random.Range(randomMinIdleTime, randomMaxIdleTime);

        roamingDestination = RandomDestinationFromArea(roamingArea);
        Huabot.Instance.HuabotAIController.SetDestination(roamingDestination);
    }  

    #endregion CUSTOM_FUNCTIONS
}
=======
﻿using UnityEngine;

public class RoamingState : IState
{
    #region VARIABLES

    private Transform objectToMove;
    private Vector3 roamingDestination;
    private Vector3 roamingArea;

    private float randomIdleTime = 4;
    private readonly float randomMinIdleTime = 0.5f;
    private readonly float randomMaxIdleTime = 2f;

    #endregion VARIABLES

    #region PROPERTIES

    public bool IsTimeToIdle
    {
        get
        {
            return randomIdleTime > 0f;
        }
    }

    #endregion PROPERTIES

    #region CONSTRUCTORS

    public RoamingState(Transform objectToMove ,Vector3 roamingArea)
    {
        this.objectToMove = objectToMove;
        this.roamingArea = roamingArea;
    }

    #endregion CONSTRUCTORS

    #region CUSTOM_FUNCTIONS

    private Vector3 RandomDestinationFromArea(Vector3 areaSize)
    {
        return new Vector3(
            Random.Range(-areaSize.x, areaSize.x),
            0.1f,
            Random.Range(-areaSize.z, areaSize.z));
    }

    private void CheckDistance()
    {
        var distance = Vector3.Distance(objectToMove.transform.position, roamingDestination);

        if (distance <= .5f)
        {
            SetNewRandomDestination();
        }
    }

    public void Enter()
    {
        SetNewRandomDestination();


        CheckDistance();
    }

    public void Execute()
    {
        if(IsTimeToIdle)
        {
            randomIdleTime -= Time.deltaTime;
            return;
        }

        CheckDistance();
    }

    public void Exit()
    {
        
    }

    private void SetNewRandomDestination()
    {
        randomIdleTime = Random.Range(randomMinIdleTime, randomMaxIdleTime);

        roamingDestination = RandomDestinationFromArea(roamingArea);
        Huabot.Instance.HuabotAIController.SetDestination(roamingDestination);
        //Debug.Log(roamingDestination);
    }  

    #endregion CUSTOM_FUNCTIONS
}
>>>>>>> c5991e646145c1830fee939d0e66a147eea824cc:ARPet/Assets/Scripts/AI/StateMachine/States/RoamingState.cs
