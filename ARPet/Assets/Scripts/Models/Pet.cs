using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    #region VARIABLES

    private NavMeshAgent navMeshAgent;

    private StateMachine stateMachine;
    private LayerMask searchLayer;
    private float searchRadius;
    private string searchTag;

    #endregion VARIABLES

    #region PROPERTIES

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        stateMachine.ChangeState(new SearchState(transform.position, searchLayer, searchRadius, searchTag, OnSearchCompleted));
    }

    private void Update()
    {
        UpdateUI();

        stateMachine.ExecuteStateUpdate();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void UpdateUI()
    {
        UIManager.Instance.StateText = stateMachine.CurrentState.ToString();

        //if(currentMainTask != null)
        //{
        //    UIManager.Instance.MainTaskText = currentMainTask.TaskName;
        //}

        //if (currentSecondaryTask != null)
        //{
        //    UIManager.Instance.SecondaryTaskText = currentSecondaryTask.TaskName;
        //}
    }

    private void Initialize()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine();
        searchLayer = LayerMask.GetMask("Searchable");
        searchRadius = 20f;
        searchTag = "Food"; 
    }

    private void OnSearchCompleted(SearchResult searchResult)
    {
        var closestObject = GetClosestObject(searchResult.AllHitCollidersInRadius);

        navMeshAgent.SetDestination(closestObject);
    }

    private Vector3 GetClosestObject(Collider[] hitColliders)
    {
        if (hitColliders.Length > 0)
        {
            var closestIndex = 0;
            var nearestDistance = Vector3.SqrMagnitude(transform.position - hitColliders[0].transform.position);

            for (int i = 1; i < hitColliders.Length; i++)
            {
                var distance = Vector3.SqrMagnitude(transform.position - hitColliders[i].transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    closestIndex = i;
                }
            }

            return hitColliders[closestIndex].transform.position;
        }

        Debug.LogError("!!!");
        return Vector3.zero;
    }

    #endregion CUSTOM_FUNCTIONS
}

