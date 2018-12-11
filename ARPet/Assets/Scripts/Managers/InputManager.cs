using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
        SetLinePositions(Vector3.zero, Vector3.zero, false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            currentTouch = Input.GetTouch(0);
            ShootRayFromTouch(currentTouch);
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

    private void ShootRayFromTouch(Touch touch)
    {
        List<ARHitResult> hitResults = ARFrame.HitTest(touch);

        if(hitResults.Count > 0)
        {
            currentHitResult = hitResults[0];
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
                }
                
                break;
                
            case TouchPhase.Moved:

                if (currentTrackable != null)
                {
                    SetLinePositions(touch.position, currentHitResult.HitPose.position, true);
                    currentTouchHitPointObject.SetActive(true);
                    currentTouchHitPointObject.transform.SetPositionAndRotation(currentHitResult.HitPose.position, currentHitResult.HitPose.rotation);

                    if(currentTrackable is ARPlane)
                    {
                        LevelManager.Instance.World.MoveWorld(CurrentHitPose);
                    }
                }

                break;

            case TouchPhase.Stationary:


                break;

            case TouchPhase.Ended:

                if(currentTrackable != null)
                {
     
                }

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

    private void ShootRayFromScreenPosition()
    {
        List<ARHitResult> hitResults = ARFrame.HitTest(Screen.width / 2, Screen.height / 2);

        if(hitResults.Count > 0)
        {
            currentHitResult = hitResults[0];

            currentTrackable = currentHitResult.GetTrackable();

            SetLinePositions(new Vector3(Screen.width / 2, Screen.height / 2, 0), currentHitResult.HitPose.position, true);
            currentTouchHitPointObject.SetActive(true);
            currentTouchHitPointObject.transform.position = currentHitResult.HitPose.position;

        }
        else
        {
            currentHitResult = null;
            currentTrackable = null;
            SetLinePositions(Vector3.zero, Vector3.zero, false);
            currentTouchHitPointObject.SetActive(false);
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
