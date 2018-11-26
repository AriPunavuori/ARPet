using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singelton<TaskManager>
{
    #region VARIABLES

    private Task currentMainTask, currentSecondaryTask;
    private List<Task> mainTask = new List<Task>();
    private List<Task> secondaryTask = new List<Task>();

    #endregion VARIABLES

    #region PROPERTIES

    public bool HasMainTask
    {
        get
        {
            return mainTask.Count > 0 ? true : false;
        }
    }
    public bool HasSecondaryTask
    {
        get
        {
            return secondaryTask.Count > 0 ? true : false;
        }
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        currentMainTask = null;
        currentSecondaryTask = null;
    }

    private void StartMainTask()
    {
        StartCoroutine(IExecuteMainTask());
    }

    private void StartSecondaryTask()
    {
        StartCoroutine(IExecuteSecondaryTask());
    }

    public void AddMainTask(Task newTask)
    {
        mainTask.Add(newTask);
    }

    public void AddSecondaryTask(Task newTask)
    {
        secondaryTask.Add(newTask);
    }

    #region COROUTINES

    private IEnumerator IExecuteMainTask()
    {
        while (HasMainTask)
        {
            if (HasSecondaryTask)
            {
                StartSecondaryTask();

                yield return new WaitWhile(() => HasSecondaryTask);
            }
        }

        yield return null;
    }

    private IEnumerator IExecuteSecondaryTask()
    {
        yield return null;
    }

    #endregion COROUTINES

    #endregion CUSTOM_FUNCTIONS
}

