using UnityEngine;
using TMPro;
public class Dragger : MonoBehaviour, IDragger {
    public LayerMask HitLayerMask;

    private readonly float maxHitDist = 100f;
    private GameObject boxPrefab;
    IDraggable currentDrag;
    Transform target;
    float dragDistance;
    float textTimer;
    float resetText = Mathf.Infinity;
    bool mctiShown;
    bool ttiShown;
    MoveBot mb;

    public float distToGrab = 0.5f;
    public TextMeshProUGUI UIText;
    public void BreakDrag() {
        currentDrag = null;
    }

    private void Awake() {
        boxPrefab = ResourceManager.Instance.BlockPrefab;
    }

    private void Start() {
        mb = FindObjectOfType<MoveBot>();
    }

    void Update () {

        textTimer -= Time.deltaTime;
        resetText -= Time.deltaTime;
        if(resetText < 0){
            mctiShown = false;
            ttiShown = false;
        }

        if(Input.GetKeyDown(KeyCode.B)) {
            SpawnBox();
        }

        if(currentDrag != null) {
            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.B)) {
                mb.BotRoam();
                BotLookScript.target = null;
                currentDrag.OnDragEnd();
                currentDrag = null;
            } else {
                currentDrag.OnDragContinue(transform.position + transform.forward * dragDistance, transform.rotation);
               if(mb.currentBotState != BotState.Hungry) {
                    mb.BotIsInterested(target.position);
                }
            }

        } else {
            var ray = /*CameraEngine.Instance.MainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));*/Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));// InputManager.Instance.Ray;
            // !!!
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, maxHitDist, HitLayerMask)) {
                //Debug.Log(hitInfo.collider.name);
                var draggable = hitInfo.collider.GetComponent<IDraggable>();
                target = hitInfo.collider.GetComponent<Transform>();
                if(draggable != null) {
                    if(hitInfo.distance < distToGrab) {
                        if(!ttiShown) {
                            UIText.text = "Tap and hold interact!";
                            ttiShown = true;
                            textTimer = 5f;
                            resetText = 60f;
                        }
                    } else {
                        if(!mctiShown) {
                            UIText.text = "Move closer to interact.";
                            mctiShown = true;
                            textTimer = 5f;
                        }
                    }

                    if(Input.GetMouseButtonDown(0)) {
                        dragDistance = hitInfo.distance;
                        currentDrag = draggable;
                        currentDrag.OnDragStart(this, transform.rotation);

                        if(mb.currentBotState != BotState.Hungry) {
                            mb.boredTimer = mb.boredInterval;
                            BotLookScript.target = target;
                        }
                    } 
                }

            } else {
                if(textTimer < 0) {
                    UIText.text = "";
                }
            }
        }
    }

    public void SpawnBox() {
        //var newBox = Instantiate(boxPrefab, transform.forward * 0.3f, Quaternion.identity);  

        var newBox = Instantiate(boxPrefab, CameraEngine.Instance.CameraPose.position + Vector3.forward, CameraEngine.Instance.CameraPose.rotation);  

        if (currentDrag != null) {
            currentDrag.OnDragStart(this, transform.rotation);
        }
    }
}