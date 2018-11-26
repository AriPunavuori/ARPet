using UnityEngine;

public class Task : MonoBehaviour
{
    public string TaskName { get; private set; }
    public bool IsCompleted { get; private set; }
    public Vector3 TaskPosition { get; private set; }

    public Task(string taskName, Vector3 taskPosition)
    {
        IsCompleted = false;

        TaskName = taskName;
        TaskPosition = taskPosition;
    }

    public void OnTaskStart()
    {

    }

    private void Execute()
    {

    }

    private void OnTaskEnd()
    {

    }
}
