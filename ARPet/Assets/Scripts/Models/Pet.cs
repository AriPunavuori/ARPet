using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    #region VARIABLES

    private NavMeshAgent navMeshAgent;
    private MeshRenderer meshRenderer;

    private StateMachine stateMachine;
    private LayerMask searchLayer;
    private float searchRadius;
    private string searchTag;

    private Color defaultColor, destinationReachedColor;

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
        UIManager.Instance.CurrentStateText = stateMachine.CurrentState != null ? stateMachine.CurrentState.ToString() : "NULL";
        UIManager.Instance.PreviousStateText = stateMachine.PreviousState != null ? stateMachine.PreviousState.ToString() : "NULL";

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
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        stateMachine = new StateMachine();
        searchLayer = LayerMask.GetMask("Searchable");
        searchRadius = 20f;
        searchTag = "Food";

        defaultColor = meshRenderer.material.color;
        destinationReachedColor = Color.red;
    }

    private void OnSearchCompleted(SearchResult searchResult)
    {
        var closestObject = GetClosestObject(searchResult.AllHitCollidersInRadius);

        navMeshAgent.SetDestination(closestObject);

        stateMachine.ChangeState(new MoveState(transform, closestObject, meshRenderer.material, OnDestinationReached));
    }

    private void OnDestinationReached()
    {
        // Eat state???
        // stateMachine.ChangeState(new EatState(4f, meshRenderer.material, defaultColor, OnEatStateEnd));

        stateMachine.ChangeState(new SearchState(transform.position, searchLayer, searchRadius, searchTag, OnSearchCompleted));
    }

    private void OnEatStateEnd()
    {
        stateMachine.ChangeState(new IdleState(new Vector2(1, 4)));
    }

    private void RandomMovement(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
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

