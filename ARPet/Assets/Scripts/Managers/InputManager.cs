using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region VARIABLES

    private ARHitResult currentHitResult;
    private ARTrackable currentTrackable;
    private List<ARAnchor> planeAnchors = new List<ARAnchor>();

    private Touch currentTouch;
    private LineRenderer lineRenderer;
    private GameObject currentTouchHitPoint;

    #endregion VARIABLES

    #region PROPERTIES

    public string CurrentTrackable
    {
        get
        {
            return currentTrackable != null ? currentTrackable.GetType().Name : "NULL";
        }
    }
    public string CurrentHitDistance
    {
        get
        {
            return currentHitResult != null ? currentHitResult.Distance.ToString() : "NULL";
        }
    }
    public string CurrentHitPose
    {
        get
        {
            return currentHitResult != null ? currentHitResult.HitPose.ToString() : "NULL";
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
        ShootRayFromScreenPosition();

        if (Input.touchCount > 0)
        {
            currentTouch = Input.GetTouch(0);
            ShootRayFromTouch(currentTouch);        
        }

        UIManager.Instance.UpdateDebugTexts(
             CameraEngine.Instance.CameraPose.ToString(),
             "NULL",
             AudioManager.Instance.Device,
             CurrentTrackable,
             CurrentHitDistance,
             CurrentHitPose,
             currentTouch.phase.ToString(),
             planeAnchors.Count.ToString()
            );
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        currentTouchHitPoint = Instantiate(ResourceManager.Instance.TouchHitPointPrefab);
    }

    private void ShootRayFromTouch(Touch touch)
    {
        List<ARHitResult> hitResults = ARFrame.HitTest(touch);

        if (hitResults.Count > 0)
            currentHitResult = hitResults[0];

        switch (touch.phase)
        {
            case TouchPhase.Began:

                    foreach (var hitResult in hitResults) 
                    {
                        currentTrackable = hitResult.GetTrackable();

                        if (currentTrackable != null)
                        {              
                            SetLinePositions(touch.position, hitResult.HitPose.position, true);
                            currentTouchHitPoint.SetActive(true);
                            currentTouchHitPoint.transform.position = hitResult.HitPose.position;

                            LevelManager.Instance.CreateLevel(hitResult.HitPose.position);
                        }
                    }

                break;

            case TouchPhase.Moved:

                    SetLinePositions(touch.position, currentHitResult.HitPose.position, true);
                    currentTouchHitPoint.SetActive(true);
                    currentTouchHitPoint.transform.position = currentHitResult.HitPose.position;

                break;

            case TouchPhase.Stationary:

                //SetLinePositions(touch.position, hitResult.HitPose.position, true);

                break;

            case TouchPhase.Ended:

                    SetLinePositions(Vector3.zero, Vector3.zero, false);
                    currentTouchHitPoint.SetActive(false);

                    break;

            case TouchPhase.Canceled:

                    SetLinePositions(Vector3.zero, Vector3.zero, false);
                    currentTouchHitPoint.SetActive(false);

                    break;

            default:

                break;
        }
    }

    private void ShootRayFromScreenPosition()
    {
        var foo = CameraEngine.Instance.CameraPose.position;
        List<ARHitResult> hitResults = ARFrame.HitTest(foo.x, foo.y);

        if (hitResults.Count > 0)
        {
            foreach (var hitResult in hitResults)
            {
                currentTrackable = hitResult.GetTrackable();
             
                SetLinePositions(new Vector3(foo.x, foo.y, 0), hitResult.HitPose.position, true);
                currentTouchHitPoint.SetActive(true);
                currentTouchHitPoint.transform.position = hitResult.HitPose.position;

                //LevelManager.Instance.CreateLevel(hitResult.HitPose.position);
                
            }
        }
        else
        {
            currentTouchHitPoint.SetActive(false);
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
