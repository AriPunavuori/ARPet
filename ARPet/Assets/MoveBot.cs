using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum BotState { Bored, Charged, Happy, Hello, Hungry, Idle, Interested, Roaming, Searching, Spooked, Waiting }
public class MoveBot : MonoBehaviour {

    Animator[] Animators;
    NavMeshAgent nma;
    public Vector3 target;
    public BotState currentBotState;

    public float idleInterval;
    public float boredInterval;
    public float hungryIntreval;
    public float stateInterval;

    public float idleTimer;
    public float stateTimer;
    public float boredTimer;
    public float hungryTimer;

    float targetDist = 0.4f;
    bool targetFound;
    public bool targetSet;
    bool stateSet;

    private void Awake() {
        nma = GetComponent<NavMeshAgent>();
        //idleTimer = idleInterval;
        //boredTimer = boredInterval;
        //hungryTimer = hungryIntreval;
    }

    private void PlayAnimations(string animationName) {

        foreach(var animator in Animators) {
            animator.Play(animationName);
        }
    }
    private void Start() {
        Animators = GetComponentsInChildren<Animator>();
    }

    private void Update() {
    
        if(currentBotState != BotState.Hungry) {
            idleTimer -= Time.deltaTime;
            boredTimer -= Time.deltaTime;
            hungryTimer -= Time.deltaTime;
            stateTimer -= Time.deltaTime;
        }

        if(boredTimer < 0) {
            currentBotState = BotState.Bored;
        }
        if(hungryTimer < 0) {
            currentBotState = BotState.Hungry;
        }

        if(currentBotState == BotState.Bored && currentBotState != BotState.Hungry) {
            // triggeröidään ajastimella ja täältä pois lelun nostolla waittiin tai jos tulee nälkä

        }

        if(currentBotState == BotState.Charged) {
            // triggeröidään patterilla ja täältä roaming
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget());
            }
        }

        if(currentBotState == BotState.Happy) {
            // Triggeröidään löytämällä lelu ja täältä roaming tilaan
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget());
            }
        }

        if(currentBotState == BotState.Hello) {
            // Triggeröidään tulemalla lähelle
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget());
            }
        }

        if(currentBotState == BotState.Hungry) {
            // triggeröidään ajastimella ja täältä charged tilaan vain patterilla
        }

        if(currentBotState == BotState.Idle) {
            // Triggeröidään ainakin roamingista ja palataan roamingiin
            if(idleTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget());
            }
        }

        if(currentBotState == BotState.Interested) {
            // Triggeröidään tavaran nostamisella
        }

        if(currentBotState == BotState.Roaming) {

            if(Vector3.Distance(transform.position, target) < targetDist) {
                SetBotState(BotState.Idle, Vector3.zero);
                idleTimer = Random.Range(0.2f, 2f);
            }

            if(!targetSet) {
                GenerateRandomTarget();
                nma.destination = target;
                targetSet = true;
            }

            if(nma.velocity.sqrMagnitude > Mathf.Epsilon) {
                transform.rotation = Quaternion.LookRotation(nma.velocity.normalized);
            }
        }

        if(currentBotState == BotState.Searching) {
            // Wait tilastä tänne ja kun lelu löytyy, niin happy tilaan
        }

        if(currentBotState == BotState.Spooked) {
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget());
            }
        }

        if(currentBotState == BotState.Waiting) {
            // Tila vaihdetaan lelun laskemisella searching tilaan
        }
    }

    public void SetBotState(BotState newState, Vector3 newTarget) {
        currentBotState = newState;

        //PlayAnimations("newState");

        //Play audio botState
        //Play anim botState
        stateTimer = stateInterval;
    }

    public void BotIsInterested(Vector3 newTarget) {
        currentBotState = BotState.Interested;
        target = newTarget;
        targetSet = false;
    }

    public void GotSomeJuice() {
        hungryTimer = hungryIntreval;
        boredTimer = boredInterval;
        SetBotState(BotState.Charged, Vector3.zero);
        stateTimer = stateInterval;
    }

    public Vector3 GenerateRandomTarget() {
        targetSet = false;
        return target = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));

    }
}
