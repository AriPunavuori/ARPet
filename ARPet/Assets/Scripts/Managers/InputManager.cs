using HuaweiARUnitySDK;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region VARIABLES

    private const float MAX_RAY_DISTANCE = 100f;

    public LayerMask TouchInteractLayer;

    #endregion VARIABLES

    #region PROPERTIES

    public bool IsWorldCreated { get; private set; }
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
        CanWeTouch();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {

    }

    private void CanWeTouch()
    {
        Touch touch;

        if (IsPointerOverGameObject)
            return;

        if (ARFrame.GetTrackingState() != ARTrackable.TrackingState.TRACKING
            || Input.touchCount < 1
            || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }
        else
        {
            GameMaster.Instance.DoTouch(touch);
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
