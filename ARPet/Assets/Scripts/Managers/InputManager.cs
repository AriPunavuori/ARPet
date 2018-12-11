using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singelton<InputManager>
{
    #region VARIABLES

    private ARHitResult currentHitResult;
    private ARTrackable currentTrackable;

    private Touch currentTouch;
    private LineRenderer lineRenderer;
    private GameObject currentTouchHitPointObject;

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
            return currentHitResult != null ? currentHitResult.HitPose : new Pose(Vector3.zero, Quaternion.Euler(Vector3.zero));
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

    }

    public void ChangeCurrentlySelectedObject()
    {

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            currentTouch = Input.GetTouch(0);
            //ARShootRayFromTouch(currentTouch);
            ShootRayUnity(currentTouch);
        }
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        currentTouchHitPointObject = Instantiate(ResourceManager.Instance.TouchHitPointPrefab, GameMaster.Instance.ModelContainer);
        currentTouchHitPointObject.SetActive(false);
    }

    private void ARShootRayFromTouch(Touch touch)
    {
        List<ARHitResult> arHitResults = ARFrame.HitTest(touch);

        if(arHitResults.Count > 0)
        {
            currentHitResult = arHitResults[0];

            currentTrackable = currentHitResult.GetTrackable();
        }   

        switch (touch.phase)
        {
            case TouchPhase.Began:
                                       
                if (currentTrackable != null)
                {
                    SetLinePositions(touch.position, currentHitResult.HitPose.position, true);
                    currentTouchHitPointObject.SetActive(true);
                    currentTouchHitPointObject.transform.SetPositionAndRotation(currentHitResult.HitPose.position, currentHitResult.HitPose.rotation);

                    if(currentTrackable is ARPlane)
                    WorldManager.Instance.TryPlaceObject(CurrentlySelectedPrefab, CurrentHitPose);
                    
                }
                
                break;
                
            case TouchPhase.Moved:

                if (currentTrackable != null)
                {
                    SetLinePositions(touch.position, currentHitResult.HitPose.position, true);
                    currentTouchHitPointObject.SetActive(true);
                    currentTouchHitPointObject.transform.SetPositionAndRotation(currentHitResult.HitPose.position, currentHitResult.HitPose.rotation);
                }

                break;

            case TouchPhase.Stationary:


                break;

            case TouchPhase.Ended:
                
                CurrentlySelectedPrefab = null;

                currentTrackable = null;
                currentHitResult = null;
                SetLinePositions(Vector3.zero, Vector3.zero, false);
                currentTouchHitPointObject.SetActive(false);

                    break;

            case TouchPhase.Canceled:

                currentTrackable = null;
                currentHitResult = null;
                SetLinePositions(Vector3.zero, Vector3.zero, false);
                currentTouchHitPointObject.SetActive(false);

                    break;

            default:

                break;
        }
    }

    private void ShootRayUnity(Touch touch)
    {
        var ray = CameraEngine.Instance.MainCamera.ScreenPointToRay(touch.position);
        RaycastHit hit; 

        switch (touch.phase)
        {
            case TouchPhase.Began:

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    CurrentlySelectedPrefab = null;
                    return;
                }

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    SetLinePositions(ray.origin, hit.point, false);
                    currentTouchHitPointObject.SetActive(true);
                    currentTouchHitPointObject.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                    if (CurrentlySelectedPrefab != null)
                    {
                        var newObject = Instantiate(CurrentlySelectedPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    }
                }

                break;

            case TouchPhase.Moved:

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    CurrentlySelectedPrefab = null;
                    return;
                }

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    SetLinePositions(ray.origin, hit.point, false);
                    currentTouchHitPointObject.SetActive(true);
                    currentTouchHitPointObject.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                    if (CurrentlySelectedPrefab != null)
                    {

                    }
                }

                break;

            case TouchPhase.Stationary:


                break;

            case TouchPhase.Ended:


                SetLinePositions(Vector3.zero, Vector3.zero, false);
                currentTouchHitPointObject.SetActive(false);
                currentTouchHitPointObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));

                break;

            case TouchPhase.Canceled:

                SetLinePositions(Vector3.zero, Vector3.zero, false);
                currentTouchHitPointObject.SetActive(false);
                currentTouchHitPointObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));

                break;

            default:

                break;
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
