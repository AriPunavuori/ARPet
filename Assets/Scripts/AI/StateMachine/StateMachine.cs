public class StateMachine
{
    #region PROPERTIES

    public IState CurrentState
    {
        get;
        private set;
    }
    public IState PreviousState
    {
        get;
        private set;
    }

    #endregion PROPERTIES

    #region CUSTOM_FUNCTIONS

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

    #endregion CUSTOM_FUNCTIONS
}
