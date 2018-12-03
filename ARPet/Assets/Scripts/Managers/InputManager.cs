using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region VARIABLES

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
       
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {

    }

    #endregion CUSTOM_FUNCTIONS
}
