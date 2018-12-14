using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour, IDragger {
    public LayerMask HitLayerMask;

    private readonly float maxHitDist = 100f;
    public GameObject box;
    IDraggable currentDrag;
    float dragDistance;

    public void BreakDrag() {
        currentDrag = null;
    }

	void Update () {

        if(Input.GetKeyDown(KeyCode.B)) {
            SpawnBox();
        }

        if(currentDrag != null) {
            if(Input.GetMouseButtonUp(0)) {
                currentDrag.OnDragEnd();
                currentDrag = null;
            } else {
                currentDrag.OnDragContinue(transform.position + transform.forward * dragDistance, transform.rotation);
            }
        } else {
            var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, maxHitDist, HitLayerMask)) {
                //Debug.Log(hitInfo.collider.name);
                var draggable = hitInfo.collider.GetComponent<IDraggable>();
                if(draggable != null) {
                    if(Input.GetMouseButtonDown(0)) {
                        dragDistance = hitInfo.distance;
                        currentDrag = draggable;
                        currentDrag.OnDragStart(this, transform.rotation);
                    }
                }
            }
        }
    }

    public void SpawnBox() {
        Instantiate(box);
        box.transform.position = new Vector3(0,10,0);
        currentDrag = box.GetComponent<IDraggable>();
        if(currentDrag != null) {
            currentDrag.OnDragStart(this, transform.rotation);
        }
    }
}