using UnityEngine;

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
        Pet.Instance.PetAIController.SetDestination(roamingDestination);
    }  

    #endregion CUSTOM_FUNCTIONS
}
