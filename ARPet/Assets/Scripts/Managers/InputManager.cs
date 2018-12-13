using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singelton<InputManager>
{
    #region VARIABLES

    private Vector3 targetPlacementArea = new Vector3(.5f, 1, .5f);

    [SerializeField]
    private LayerMask horizontalPlaneMask;
    [SerializeField]
    private LayerMask hitLayer;

    private ARHitResult currentHitResult;
    private ARTrackable currentTrackable;

    private Touch currentTouch;
    private LineRenderer lineRenderer;
    private HitIndicator hitIndicator;

    private RaycastHit hitInfo;
    private Vector2 screenCenterPoint;
    private readonly float maxRayDistance = 10f;

    #endregion VARIABLES

    #region PROPERTIES

    public string CurrentTrackableName
    {
        get
        {
            return currentTrackable == null ? "NULL" : currentTrackable.GetType().Name;
        }
    }
    public float CurrentHitDistance
    {
        get
        {
            return currentHitResult != null ? currentHitResult.Distance : 0;
        }
    }
    public Pose CurrentHitPose
    {
        get
        {
            return currentHitResult != null ? currentHitResult.HitPose : new Pose(Vector3.zero, Quaternion.identity);
        }
    }

    public GameObject CurrentlySelectedPrefab { get; set; }

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

        if (Input.touchCount > 0)
        {
            currentTouch = Input.GetTouch(0);
        }
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
        var ray = CameraEngine.Instance.MainCamera.ScreenPointToRay(screenCenterPoint);

        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, horizontalPlaneMask))
        {
            hitIndicator.transform.SetPositionAndRotation(hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
            SetLinePositions(screenCenterPoint, hitInfo.point, true);

            if(hitInfo.collider.bounds.size.sqrMagnitude >= targetPlacementArea.sqrMagnitude)
            {
                hitIndicator.ChangeColor(Color.green);
            }
            else
            {
                hitIndicator.ChangeColor(Color.red);
            }


        }
        else
        {
            hitIndicator.transform.SetPositionAndRotation(hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
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
