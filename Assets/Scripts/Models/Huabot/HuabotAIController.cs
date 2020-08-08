using UnityEngine;
using UnityEngine.AI;

public class HuabotAIController : MonoBehaviour
{
    #region VARIABLES

    private const float MAX_HAPPINESS = 100f;
    private const float MAX_SLEEPINESS = 100f;
    private const float MAX_ENERGINES = 100f;

    private Transform interactionTarget;
    private NavMeshAgent navMeshAgent;

    private StateMachine stateMachine;
    private LayerMask searchLayer;

    private readonly float statLoseRate = 0;

    #endregion VARIABLES

    #region PROPERTIES

    public Vector3 CurrentDestination
    {
        get
        {
            return navMeshAgent.destination;
        }
    }

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

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        //SetStats();
        //stateMachine.ChangeState(new RoamingState(
        //    transform,
        //        new Vector3(
        //        LevelManager.Instance.FloorSize.x - 1,
        //        1, 
        //        LevelManager.Instance.FloorSize.z - 1)));
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
        Happiness = PlayerPrefs.HasKey("Happiness") ? PlayerPrefs.GetFloat("Happiness") : MAX_HAPPINESS;
        Sleepiness = PlayerPrefs.HasKey("Sleepiness") ? PlayerPrefs.GetFloat("Sleepiness") : MAX_SLEEPINESS;
        Energines = PlayerPrefs.HasKey("Energines") ? PlayerPrefs.GetFloat("Energines") : MAX_ENERGINES;
    }

    private void SaveStats()
    {
        PlayerPrefs.SetFloat("Happiness", Happiness);
        PlayerPrefs.SetFloat("Sleepiness", Sleepiness);
        PlayerPrefs.SetFloat("Energines", Energines);
    }

    private void UpdateStats()
    {
        var currentDeltaTime = Time.deltaTime + statLoseRate;

        Happiness = Happiness > 0 ? Happiness -= currentDeltaTime : 0;
        Sleepiness = Sleepiness > 0 ? Happiness -= currentDeltaTime : 0;
        Energines = Energines > 0 ? Happiness -= currentDeltaTime : 0;
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateUI(
            Happiness,
            Sleepiness,
            Energines,
            stateMachine.CurrentState != null ? stateMachine.CurrentState.ToString() : "NONE",
            stateMachine.PreviousState != null ? stateMachine.PreviousState.ToString() : "NONE");
    }

    private void Initialize()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();

        stateMachine = new StateMachine();
        searchLayer = LayerMask.GetMask("Interactable");
    }

    private void OnSearchCompleted(SearchResult searchResult)
    {
        interactionTarget = GetClosestObject(searchResult.AllHitCollidersInRadius);

        if(interactionTarget == null)
        {
            stateMachine.ChangeState(new IdleState(new Vector2(1, 4)));
            return;
        }

        SetDestination(interactionTarget.position);
        //stateMachine.ChangeState(new MoveState(transform, interactionTarget.position, OnDestinationReached));
    }

    private void OnDestinationReached()
    {
        stateMachine.ChangeState(new InteractState(4f, OnInteractionStart, OnInteractionEnd));
    }

    private void OnInteractionStart()
    {
        Huabot.Instance.HuabotGraphicsController.ChangeMaterialColor(Color.red);
    }

    private void OnInteractionEnd()
    {
        Huabot.Instance.HuabotGraphicsController.ChangeMaterialColor(Color.white);

        interactionTarget.gameObject.SetActive(false);
        interactionTarget = null;

        stateMachine.ChangeState(new SearchState(transform.position, searchLayer, 20f, "Food", OnSearchCompleted));
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

    public void SetDestination(Vector3 newDestination)
    {
        navMeshAgent.SetDestination(newDestination);
    }

    #endregion CUSTOM_FUNCTIONS
}