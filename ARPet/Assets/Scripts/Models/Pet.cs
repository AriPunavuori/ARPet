using System.Collections;
using UnityEngine;

public enum PetStates
{
    Idle,
    Move,
    Sleep
}

public class Pet : MonoBehaviour
{
    private Queue mainTask = new Queue();
    private Queue secondaryTask = new Queue();

    private PetStates currentState;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Initialize()
    {
        
    }

    private void ChangePetState(PetStates newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case PetStates.Idle:

                break;
            case PetStates.Move:

                break;
            case PetStates.Sleep:

                break;
            default:

                break;
        }
    }

    private void ExecuteMainTask()
    {

    }

    private IEnumerator IExecuteMainTask()
    {
        yield return null;
    }
}
