using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum BotState { Idle, Roaming, Interested, Searching, Spooked, Happy, Bored, Hungry, Hello }
public class MoveBot : MonoBehaviour {

    Animator[] Animators;
    NavMeshAgent nma;
    public Vector3 target;
    public BotState currentBotState;

    public float idleInterval;
    public float boredInterval;
    public float hungryIntreval;

    public float idleTimer;
    public float boredTimer;
    public float hungryTimer;

    float targetDist = 0.4f;
    bool targetFound;
    public bool targetSet;
    bool stateSet;

    private void Awake() {
        nma = GetComponent<NavMeshAgent>();
        idleTimer = idleInterval;
        boredTimer = boredInterval;
        hungryTimer = hungryIntreval;
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

 
        idleTimer -= Time.deltaTime;
        boredTimer -= Time.deltaTime;
        hungryTimer -= Time.deltaTime;
    

        if(idleTimer > 0 && currentBotState != BotState.Hungry) {
            currentBotState = BotState.Idle;
        } else if(currentBotState != BotState.Interested && currentBotState != BotState.Hungry) {

            currentBotState = BotState.Roaming;

            if(boredTimer < 0 && currentBotState != BotState.Hungry && !stateSet) {
                currentBotState = BotState.Bored;
            }

            if(hungryTimer < 0 && !stateSet) {
                currentBotState = BotState.Hungry;
                PlayAnimations("batteryStart");
                stateSet = true;
            }
        }

        if(currentBotState == BotState.Interested || currentBotState == BotState.Roaming|| currentBotState == BotState.Searching) {
            // Botti saa uuden randomi targetin

            if(Vector3.Distance(transform.position, target) < targetDist ) {
                nma.destination = transform.position;
                targetSet = false;
                idleTimer = Random.Range(1f, 5f);
            }

            if(currentBotState == BotState.Roaming && !targetSet) {
                target = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            }

            // Botti liikkuu
            if(!targetFound && !targetSet) {
                nma.destination = target;
                targetSet = true;
            }
        }

        if(nma.velocity.sqrMagnitude > Mathf.Epsilon) {
            transform.rotation = Quaternion.LookRotation(nma.velocity.normalized);
        }
    }

    public void BotIsSearching() {
        currentBotState = BotState.Searching;
    }

    public void BotRoam() {
        currentBotState = BotState.Roaming;
        //target = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        //targetSet = false;
    }

    public void BotIsInterested(Vector3 newTarget) {
        currentBotState = BotState.Interested;
        target = newTarget;
        targetSet = false;
    }

    public void BotIsHappy() {
        boredTimer = boredInterval;
        currentBotState = BotState.Roaming;
    }

    public void GotSomeJuice() {
        hungryTimer = hungryIntreval;
        boredTimer = boredInterval;
        currentBotState = BotState.Roaming;
        PlayAnimations("charged");
        idleTimer = 2.5f;
    }
}
