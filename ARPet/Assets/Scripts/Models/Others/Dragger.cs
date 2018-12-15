using UnityEngine;

public class Dragger : MonoBehaviour, IDragger {
    public LayerMask HitLayerMask;

    private readonly float maxHitDist = 100f;
    private GameObject boxPrefab;
    IDraggable currentDrag;
    float dragDistance;

    public void BreakDrag() {
        currentDrag = null;
    }

    private void Awake()
    {
        boxPrefab = ResourceManager.Instance.BlockPrefab;
    }

    void Update () {

        if(Input.GetKeyDown(KeyCode.B)) {
            SpawnBox();
        }

        if(currentDrag != null) {
            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.B)) {
                currentDrag.OnDragEnd();
                currentDrag = null;
            } else {
                currentDrag.OnDragContinue(transform.position + transform.forward * dragDistance, transform.rotation);
            }
        } else {
            var ray = /*Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));*/ InputManager.Instance.Ray;
            // !!!
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
        var newBox = Instantiate(boxPrefab);
        newBox.transform.position = /*transform.forward * 0.3f;*/ InputManager.Instance.Ray.origin;
        currentDrag = newBox.GetComponent<IDraggable>();
        if(currentDrag != null) {
            currentDrag.OnDragStart(this, transform.rotation);
        }
    }
}