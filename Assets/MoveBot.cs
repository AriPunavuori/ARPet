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

    public AudioClip Bored;
    public AudioClip Charged;
    public AudioClip Happy;
    public AudioClip Hello;
    public AudioClip Hungry;
    public AudioClip Idle;
    public AudioClip Interested;
    public AudioClip Roaming;
    public AudioClip Searching;
    public AudioClip Spooked;
    public AudioClip Waiting;
    AudioSource audioSource;

    public float idleInterval;
    public float boredInterval = 300;
    public float hungryInterval = 120;
    public float stateInterval = 2;

    public float idleTimer;
    public float stateTimer;
    public float boredTimer;
    public float hungryTimer = 20;

    float targetDist = 0.1f;
    bool targetFound;
    public bool targetSet;
    bool stateSet;

    bool isHungry;
    bool isBored;

    private void Awake() {
        nma = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
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

        if(boredTimer < 0 && !isBored) {
            SetBotState(BotState.Bored, Vector3.zero, Bored, "Bored");
            isBored = true;
        }

        if(hungryTimer < 0 && !isHungry) {
            SetBotState(BotState.Hungry, Vector3.zero, Hungry, "Hungry");
            isHungry =true;
        }

        if(currentBotState == BotState.Bored && currentBotState != BotState.Hungry) {
            // triggeröidään ajastimella ja täältä pois lelun nostolla waittiin tai jos tulee nälkä

        }

        if(currentBotState == BotState.Charged) {
            // triggeröidään patterilla ja täältä roaming
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget(), Roaming, "Roaming");
            }
        }

        if(currentBotState == BotState.Happy) {
            // Triggeröidään löytämällä lelu ja täältä roaming tilaan
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget(), Roaming, "Roaming");
            }
        }

        if(currentBotState == BotState.Hello) {
            // Triggeröidään tulemalla lähelle
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget(), Roaming, "Roaming");
            }
        }

        if(currentBotState == BotState.Hungry) {
            // triggeröidään ajastimella ja täältä charged tilaan vain patterilla
        }

        if(currentBotState == BotState.Idle) {
            // Triggeröidään ainakin roamingista ja palataan roamingiin
            if(idleTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget(), Roaming, "Roaming");
            }
        }

        if(currentBotState == BotState.Interested) {
            // Triggeröidään tavaran nostamisella
        }

        if(currentBotState == BotState.Roaming) {

            if(Vector3.Distance(transform.position, target) < targetDist) {
                SetBotState(BotState.Idle, Vector3.zero, Idle, "Idle");
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
            print("aloitetaan etsintä looppi");
            // Wait tilastä tänne ja kun lelu löytyy, niin happy tilaan
            
            if(!targetSet) {
                nma.destination = target;
                targetSet = true;
            }

            if(Vector3.Distance(transform.position, target) < targetDist) {
                SetBotState(BotState.Happy, Vector3.zero, Happy, "Happy");
                print("Löyty");
            }

            if(nma.velocity.sqrMagnitude > Mathf.Epsilon) {
                transform.rotation = Quaternion.LookRotation(nma.velocity.normalized);
            }
        }

        if(currentBotState == BotState.Spooked) {
            if(stateTimer < 0) {
                SetBotState(BotState.Roaming, GenerateRandomTarget(), Roaming, "Roaming");
            }
        }

        if(currentBotState == BotState.Waiting) {
            isBored = false;
            // Tila vaihdetaan lelun laskemisella searching tilaan
        }
    }

    public void SetBotState(BotState newState, Vector3 newTarget, AudioClip clip, string animState) {
        currentBotState = newState;
        audioSource.clip = clip;
        PlayAnimations("" + animState);
        audioSource.Play();
        //Play audio state
        //Play anim state
        targetSet = false;
        target = newTarget;
        stateTimer = stateInterval;
    }

    public void GotSomeJuice() {
        hungryTimer = hungryInterval;
        boredTimer = boredInterval;
        SetBotState(BotState.Charged, Vector3.zero, Charged, "Charged");
        stateTimer = stateInterval;
        isHungry = false;
    }

    public Vector3 GenerateRandomTarget() {
        targetSet = false;
        return target = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
    }
}
