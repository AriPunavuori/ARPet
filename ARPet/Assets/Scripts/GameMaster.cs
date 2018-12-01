using Common;
using HuaweiARInternal;
using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region VARIABLES

    public ARConfigBase Config;

    private List<ARPlane> newPlanes = new List<ARPlane>();
    private GameObject planePrefab;

    private bool isFirstConnect = true; //this is used to avoid multiple permission request when it was rejected
    private bool isSessionCreated = false;
    private bool isErrorHappendWhenInit = false;
    private bool installRequested = false;

    #endregion VARIABLES

    #region PROPERTIES

    public Transform HUDCanvas { get; private set; }
    public Transform Managers { get; private set; }
    public Transform Others { get; private set; }

    public string ErrorMessage { get; private set; }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void OnApplicationQuit()
    {
        ARSession.Stop();
        isFirstConnect = true;
        isSessionCreated = false;
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            ARSession.Pause();
        }
        else
        {
            if (!isSessionCreated)
            {
                InitializeAR();
            }
            if (isErrorHappendWhenInit)
            {
                return;
            }
            try
            {
                ARSession.Resume();
            }
            catch (ARCameraPermissionDeniedException /*e*/)
            {
                ARDebug.LogError("camera permission is denied");
                ErrorMessage = "This app require camera permission";
                Invoke("_DoQuit", 0.5f);
            }
        }
    }

    private void Update()
    {
        AsyncTask.Update();
        ARSession.Update();

        DrawPlane();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        planePrefab = Resources.Load<GameObject>("Prefabs/Plane");

        HUDCanvas = transform.Find("HUDCanvas");
        Managers = transform.Find("Managers");
        Others = transform.Find("Others");
    }

    private void InitializeAR()
    {
        //If you do not want to switch engines, AREnginesSelector is useless.
        // You just need to use AREnginesApk.Instance.requestInstall() and the default engine
        // is Huawei AR Engine.
        AREnginesAvaliblity ability = AREnginesSelector.Instance.CheckDeviceExecuteAbility();
        if ((AREnginesAvaliblity.HUAWEI_AR_ENGINE & ability) != 0)
        {
            AREnginesSelector.Instance.SetAREngine(AREnginesType.HUAWEI_AR_ENGINE);
        }
        /*else if((AREnginesAvaliblity.GOOGLE_AR_CORE&ability) != 0)
        {
            AREnginesSelector.Instance.SetAREngine(AREnginesType.GOOGLE_AR_CORE);
        }*/
        else
        {
            ErrorMessage = "This device does not support AR Engine. Exit.";
            UIManager.Instance.QuitButton();
            return;
        }

        try
        {
            switch (AREnginesApk.Instance.RequestInstall(!installRequested))
            {
                case ARInstallStatus.INSTALL_REQUESTED:
                    installRequested = true;
                    return;
                case ARInstallStatus.INSTALLED:
                    break;
            }

        }
        catch (ARUnavailableConnectServerTimeOutException /*e*/)
        {
            ErrorMessage = "Network is not available, retry later!";
            Invoke("_DoQuit", 0.5f);
            return;
        }
        catch (ARUnavailableDeviceNotCompatibleException /*e*/)
        {
            ErrorMessage = "This Device does not support AR!";
            Invoke("_DoQuit", 0.5f);
            return;
        }
        catch (ARUnavailableEmuiNotCompatibleException /*e*/)
        {
            ErrorMessage = "This EMUI does not support AR!";
            Invoke("_DoQuit", 0.5f);
            return;
        }
        catch (ARUnavailableUserDeclinedInstallationException /*e*/)
        {
            ErrorMessage = "User decline installation right now, quit";
            Invoke("_DoQuit", 0.5f);
            return;
        }
        if (isFirstConnect)
        {
            Connect();
            isFirstConnect = false;
        }
    }

    private void Connect()
    {
        ARDebug.LogInfo("_connect begin");
        const string ANDROID_CAMERA_PERMISSION_NAME = "android.permission.CAMERA";
        if (AndroidPermissionsRequest.IsPermissionGranted(ANDROID_CAMERA_PERMISSION_NAME))
        {
            ConnectToService();
            return;
        }
        var permissionsArray = new string[] { ANDROID_CAMERA_PERMISSION_NAME };
        AndroidPermissionsRequest.RequestPermission(permissionsArray).ThenAction((requestResult) =>
        {
            if (requestResult.IsAllGranted)
            {
                ConnectToService();
            }
            else
            {
                ARDebug.LogError("connection failed because a needed permission was rejected.");
                ErrorMessage = "This app require camera permission";
                Invoke("_DoQuit", 0.5f);
                return;
            }
        });
    }

    private void ConnectToService()
    {
        try
        {
            ARSession.CreateSession();
            isSessionCreated = true;
            ARSession.Config(Config);
            ARSession.Resume();
            ARSession.SetCameraTextureNameAuto();
            ARSession.SetDisplayGeometry(Screen.width, Screen.height);
        }
        catch (ARCameraPermissionDeniedException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ARDebug.LogError("camera permission is denied");
            ErrorMessage = "This app require camera permission";
            Invoke("_DoQuit", 0.5f);
        }
        catch (ARUnavailableDeviceNotCompatibleException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This device does not support AR";
            Invoke("_DoQuit", 0.5f);
        }
        catch (ARUnavailableServiceApkTooOldException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This AR Engine is too old, please update";
            Invoke("_DoQuit", 0.5f);
        }
        catch (ARUnavailableServiceNotInstalledException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This app depend on AREngine.apk, please install it";
            Invoke("_DoQuit", 0.5f);
        }
        catch (ARUnSupportedConfigurationException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This config is not supported on this device, exit now.";
            UIManager.Instance.QuitButton();
        }
    }

    private void DrawPlane()
    {
        newPlanes.Clear();
        ARFrame.GetTrackables(newPlanes, ARTrackableQueryFilter.NEW);
        for (int i = 0; i < newPlanes.Count; i++)
        {
            var planeObject = Instantiate(planePrefab, Vector3.zero, Quaternion.identity, transform);
            planeObject.GetComponent<TrackedPlaneVisualizer>().Initialize(newPlanes[i]);
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
