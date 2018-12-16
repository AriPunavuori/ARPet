using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushToy : MonoBehaviour, IDraggable {

    IDragger dragger;
    Vector3 oldPos;
    Vector3 newPos;
    Vector3 fakeUp;
    Vector3 fakeForward;
    public bool isMoving;
    public bool draggable = true;
    float movingThreshold = 0.1f;
    Collider scareCol;
    Quaternion dragStartRot;
    Quaternion draggerStartRot;
    public float maxRotSpeed;
    Rigidbody rb;
    MoveBot mb;

    private void Start() {
        mb = FindObjectOfType<MoveBot>();
    }

    public void OnDragStart(IDragger dragger, Quaternion draggerRotation) {
        var rotCheck = new Vector3[] { transform.right, -transform.right, transform.up, -transform.up, transform.forward, -transform.forward };
        for(int i = 0 ; i < rotCheck.Length ; i++) {
            if(rotCheck[i].y > fakeUp.y) {
                fakeUp = rotCheck[i];
            }
            if(rotCheck[i].z > fakeForward.z) {
                fakeForward = rotCheck[i];
            }
        }
        this.dragger = dragger;
        dragStartRot = transform.rotation;
        draggerStartRot = draggerRotation;
        rb.isKinematic = true;
        mb.SetBotState(BotState.Waiting, Vector3.zero, mb.Waiting, "Waiting");
    }

    public void OnDragEnd() {
        if(draggable) {
            dragger = null;
            rb.isKinematic = false;
            fakeUp = Vector3.zero;
            mb.SetBotState(BotState.Roaming, transform.position, mb.Searching, "Searching");
            print("Searhing started");
        }
    }

    public void OnDragContinue(Vector3 target) {
        OnDragContinue(target, transform.rotation);
    }

    public void OnDragContinue(Vector3 target, Quaternion draggerRotation) {
        if(draggable) {
            rb.velocity = Vector3.zero;
            var targetRot = draggerRotation * Quaternion.Inverse(draggerStartRot) * dragStartRot;
            targetRot = Quaternion.LookRotation(Vector3.ProjectOnPlane(targetRot * Vector3.forward, Vector3.up), Vector3.up);
            rb.MovePosition(target);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, maxRotSpeed * Time.deltaTime));
        }
    }

    public bool IsCurrentlyDraggable() {
        if(draggable) {
            return true;
        } else {
            return false;
        }
    }

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

}
