using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singelton<InputManager>
{
    #region VARIABLES

    private Vector3 targetPlacementArea = new Vector3(1f, 1, 1f);

    [SerializeField]
    private LayerMask hitLayerMask;

    private Touch currentTouch;
    private LineRenderer lineRenderer;
    private HitIndicator hitIndicator;

    private RaycastHit hitInfo;
    private Vector2 screenCenterPoint;
    private readonly float rayMaxDistance = 10f;

    private GameObject currentHitTarget;

    #endregion VARIABLES

    #region PROPERTIES

    public string CurrentTrackableName
    {
        get
        {
            return currentHitTarget == null ? "NULL" : currentHitTarget.name;
        }
    }
    public float CurrentHitDistance
    {
        get
        {
            return hitInfo.distance > 0 ? hitInfo.distance : 0;
        }
    }
    public Vector3 CurrentHitPoint
    {
        get
        {
            return hitInfo.point != null ? hitInfo.point : Vector3.zero;
        }
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
        SetLinePositions(Vector3.zero, Vector3.zero, false);
    }

    private void Start()
    {
        screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        UnityShootRayFromScreenPoint(screenCenterPoint);      
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        hitIndicator = Instantiate(ResourceManager.Instance.HitIndicatorPrefab, GameMaster.Instance.ModelContainer).GetComponent<HitIndicator>();
        hitIndicator.ChangeState(false);
    }

    private void UnityShootRayFromScreenPoint(Vector2 screenPoint)
    {
        var ray = CameraEngine.Instance.MainCamera./*ScreenPointToRay(screenCenterPoint)*/ViewportPointToRay(new Vector2( 0.5f, 0.5f));
        if(Physics.Raycast(ray, out hitInfo, rayMaxDistance, hitLayerMask))
        {
            hitIndicator.ChangeState(true);

            currentHitTarget = hitInfo.collider.gameObject;
            var bounds = hitInfo.collider.bounds;
            var normal = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            hitIndicator.transform.SetPositionAndRotation(CurrentHitPoint, normal);
            SetLinePositions(/*screenCenterPoint*/ray.origin, CurrentHitPoint, true);

            if(WorldManager.Instance.IsWorldCreated == false)
            {
                CanWeBuildWorld(bounds, normal);
            }
        }
        else
        {
            currentHitTarget = null;
            hitIndicator.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            hitIndicator.ChangeState(false);
            SetLinePositions(Vector3.zero, Vector3.zero, false);
        }
    }

    private void CanWeBuildWorld(Bounds bounds, Quaternion hitNormal)
    {
        if (bounds.size.x >= targetPlacementArea.x && bounds.size.z >= targetPlacementArea.z)
        {
            UIManager.Instance.SwitchDeviceImage(true, 2);

            hitIndicator.transform.SetPositionAndRotation(bounds.center, hitNormal);
            hitIndicator.ChangeColor(Color.green);

            if (Input.touchCount > 0)
            {
                currentTouch = Input.GetTouch(0);

                switch (currentTouch.phase)
                {
                    case TouchPhase.Began:

                        break;

                    case TouchPhase.Moved:

                        break;

                    case TouchPhase.Stationary:

                        break;

                    case TouchPhase.Ended:

                        WorldManager.Instance.CreateWorld(new Pose(hitIndicator.transform.position, hitNormal));

                        UIManager.Instance.SwitchDeviceImage(false);

                        break;

                    case TouchPhase.Canceled:

                        break;

                    default:

                        break;
                }
            }
        }
        else
        {
            UIManager.Instance.SwitchDeviceImage(true, 1);

            hitIndicator.ChangeColor(Color.red);
        }
    }

    public void CanWeCreateBox()
    {
        if(currentHitTarget != null)
        {
            hitIndicator.transform.SetPositionAndRotation(CurrentHitPoint, Quaternion.identity);
            hitIndicator.ChangeColor(Color.green);

            if (Input.touchCount > 0)
            {
                currentTouch = Input.GetTouch(0);

                var newBlock = Instantiate(ResourceManager.Instance.BlockPrefab, CurrentHitPoint, Quaternion.identity).GetComponent<Block>();

                switch (currentTouch.phase)
                {
                    case TouchPhase.Began:

                        if (currentHitTarget != null)
                        {
                            newBlock.StartPlacing();
                        }                    

                        break;
                    case TouchPhase.Moved:

                        if(currentHitTarget != null)
                        {
                            newBlock.SetNewPosition(CurrentHitPoint);
                        }

                        break;
                    case TouchPhase.Stationary:

                        break;

                    case TouchPhase.Ended:

                        if (currentHitTarget != null)
                        {
                            newBlock.NewPlacement(CurrentHitPoint);
                        }

                        break;

                    case TouchPhase.Canceled:

                        if (currentHitTarget != null)
                        {
                            newBlock.CancelPlacement();
                        }

                        break;

                    default:

                        break;
                }
            }
        }
        else
        {
            hitIndicator.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            hitIndicator.ChangeState(false);
            SetLinePositions(Vector3.zero, Vector3.zero, false);
        }       
    }

    private void SetLinePositions(Vector3 startPosition, Vector3 endPosition, bool isEnabled)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
        lineRenderer.enabled = isEnabled;
    }

    #endregion CUSTOM_FUNCTIONS
}
