using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region VARIABLES

    public GameObject Cube, Sphere;

    private List<ARAnchor> addedAnchors = new List<ARAnchor>(); 

    private const int anchorLimit = 16;
    private const float MAX_RAY_DISTANCE = 100f;

    public LayerMask TouchInteractLayer;

    #endregion VARIABLES

    #region PROPERTIES

    public bool IsPointerOverGameObject
    {
        get
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        CheckTouch();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {

    }

    private void CheckTouch()
    {
        var touch = new Touch();

        if (ARFrame.GetTrackingState() != ARTrackable.TrackingState.TRACKING
                || Input.touchCount < 1 
                || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        DrawObject(touch);   
    }

    private void DrawObject(Touch touch)
    {
        List<ARHitResult> hitResults = ARFrame.HitTest(touch);
        foreach (ARHitResult singleHit in hitResults)
        {
            ARTrackable trackable = singleHit.GetTrackable();
            if ((trackable is ARPlane && ((ARPlane)trackable).IsPoseInPolygon(singleHit.HitPose)) ||
                (trackable is ARPoint))
            {
                var prefab = trackable is ARPlane ? Cube : Sphere;

                if (addedAnchors.Count > anchorLimit)
                {
                    ARAnchor toRemove = addedAnchors[0];
                    toRemove.Detach();
                    addedAnchors.RemoveAt(0);
                }

                ARAnchor anchor = singleHit.CreateAnchor();
                var newObject = Instantiate(prefab, anchor.GetPose().position, anchor.GetPose().rotation);
                newObject.GetComponent<ObjectVisualizer>().Initialize(anchor);
                addedAnchors.Add(anchor);
                break;
            }
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
