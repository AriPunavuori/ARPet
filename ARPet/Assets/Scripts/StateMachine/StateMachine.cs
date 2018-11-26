public class StateMachine
{
    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }

    public void ChangeState(IState newState)
    {
        if(CurrentState != null)
        {
            PreviousState = CurrentState;

            CurrentState.Exit();
        }

        CurrentState = newState;

        CurrentState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }

    public void ChangePreviousState()
    {
        if (PreviousState != null)
        {
            ChangeState(PreviousState);
        }
    }
}
