using UnityEngine;

public class CameraEngine : Singelton<CameraEngine>
{
    #region PROPERTIES

    public Camera MainCamera { get; private set; }

    #endregion PROPERTIES

    private void Awake()
    {
        MainCamera = GetComponent<Camera>();
    }
}
