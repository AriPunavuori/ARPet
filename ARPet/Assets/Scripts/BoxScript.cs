using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour,IDraggable {



    IDragger dragger;
    Vector3 oldPos;
    Vector3 newPos;
    public bool isMoving;
    float movingThreshold = 0.1f;
    Collider scareCol;
    Quaternion dragStartRot;
    Rigidbody rb;

    public void OnDragStart(IDragger dragger) {
        this.dragger = dragger;
        dragStartRot = transform.rotation;
    }

    public void OnDragEnd() {
        dragger = null;
    }

    public void OnDragContinue(Vector3 target) {
        OnDragContinue(target, transform.rotation);
    }

    public void OnDragContinue(Vector3 target, Quaternion rotation) {
        rb.velocity = Vector3.zero;
        var targetRot = dragStartRot * rotation;
        rb.MovePosition(target);
        rb.MoveRotation(rotation);
    }

    public bool IsCurrentlyDraggable() {
        return true;
    }

    void Awake () {
        scareCol = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		newPos = transform.position;
        if(Vector3.Distance(newPos, oldPos) > movingThreshold) {
            isMoving = true;
            scareCol.enabled = true;
        } else {
            isMoving = false;
            scareCol.enabled = false;
        }
        oldPos = transform.position;
	}

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "ARBot") {

            var escapeVector = other.transform.position - transform.position;
            //escapeVector.y = 0;
            escapeVector = Vector3.ProjectOnPlane(escapeVector,Vector3.up);
            other.attachedRigidbody.AddForce(escapeVector.normalized * 100, ForceMode.Acceleration);
        }
    }

    

}
