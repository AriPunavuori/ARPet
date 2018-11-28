using UnityEngine;
using UnityEngine.AI;

public class PetAI : MonoBehaviour
{
    #region VARIABLES

    private Transform interactionTarget;
    private NavMeshAgent navMeshAgent;
    private MeshRenderer meshRenderer;

    private StateMachine stateMachine;
    private LayerMask searchLayer;

    private Color defaultColor;

    #endregion VARIABLES

    #region PROPERTIES

    #endregion PROPERTIES

    public float Happiness
    {
        get;
        private set;
    }
    public float Sleepiness
    {
        get;
        private set;
    }
    public float Energines
    {
        get;
        private set;
    }

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        SetStats();

        stateMachine.ChangeState(new SearchState(transform.position, searchLayer, 20f, "Food", OnSearchCompleted));
    }

    private void Update()
    {
        UpdateStats();

        UpdateUI();

        stateMachine.ExecuteStateUpdate();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void SetStats()
    {
        Happiness = PlayerPrefs.HasKey("Happiness") ? PlayerPrefs.GetFloat("Happiness") : 100f;
        Sleepiness = PlayerPrefs.HasKey("Sleepiness") ? PlayerPrefs.GetFloat("Sleepiness") : 100f;
        Energines = PlayerPrefs.HasKey("Energines") ? PlayerPrefs.GetFloat("Energines") : 100f;
    }

    private void SaveStats()
    {
        PlayerPrefs.SetFloat("Happiness", Happiness);
        PlayerPrefs.SetFloat("Sleepiness", Sleepiness);
        PlayerPrefs.SetFloat("Energines", Energines);
    }

    private void UpdateStats()
    {
        var currentDeltaTime = Time.deltaTime;
        Happiness -= currentDeltaTime;
        Sleepiness -= currentDeltaTime;
        Energines -= currentDeltaTime;
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateUI(
            Happiness,
            Sleepiness,
            Energines,
            stateMachine.CurrentState != null ? stateMachine.CurrentState.ToString() : "NULL",
            stateMachine.PreviousState != null ? stateMachine.PreviousState.ToString() : "NULL");
    }

    private void Initialize()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        stateMachine = new StateMachine();
        searchLayer = LayerMask.GetMask("Interactable");

        defaultColor = meshRenderer.material.color;
    }

    private void OnSearchCompleted(SearchResult searchResult)
    {
        interactionTarget = GetClosestObject(searchResult.AllHitCollidersInRadius);

        if(interactionTarget == null)
        {
            stateMachine.ChangeState(new IdleState(new Vector2(1, 4)));
            return;
        }

        navMeshAgent.SetDestination(interactionTarget.position);
        stateMachine.ChangeState(new MoveState(transform, interactionTarget.position, OnDestinationReached));
    }

    private void OnDestinationReached()
    {
        stateMachine.ChangeState(new InteractState(4f, OnInteractionStart, OnInteractionEnd));
    }

    private void OnInteractionStart()
    {
        meshRenderer.material.color = Color.red;
    }

    private void OnInteractionEnd()
    {
        meshRenderer.material.color = defaultColor;

        interactionTarget.gameObject.SetActive(false);
        interactionTarget = null;

        stateMachine.ChangeState(new SearchState(transform.position, searchLayer, 20f, "Food", OnSearchCompleted));
    }

    private void OnEatStateEnd()
    {
        Destroy(interactionTarget.gameObject);

        stateMachine.ChangeState(new IdleState(new Vector2(1, 4)));
    }

    private void RandomMovement(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    private Transform GetClosestObject(Collider[] hitColliders)
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

            return hitColliders[closestIndex].transform;
        }

        return null;
    }

    #endregion CUSTOM_FUNCTIONS
}

