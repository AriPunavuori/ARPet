using HuaweiARInternal;
using HuaweiARUnitySDK;
using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region VARIABLES

    public ARConfigBase Config;

    private const float QUIT_DELAY = 0.5f;

    private bool isFirstConnect = true; //this is used to avoid multiple permission request when it was rejected
    private bool isSessionCreated = false;
    private bool isErrorHappendWhenInit = false;
    private bool installRequested = false;

    #endregion VARIABLES

    #region PROPERTIES

    public string ErrorMessage { get; private set; }

    public Transform HUDCanvas { get; private set; }
    public Transform Managers { get; private set; }
    public Transform Others { get; private set; }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }
    private void Start()
    {
       
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
                UIManager.Instance.QuitButton(QUIT_DELAY);
            }
        }
    }
    private void Update()
    {
        AsyncTask.Update();
        ARSession.Update();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

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
        else
        {
            ErrorMessage = "This device does not support AR Engine. Exit.";
            UIManager.Instance.QuitButton(QUIT_DELAY);
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
            UIManager.Instance.QuitButton(QUIT_DELAY);
            return;
        }
        catch (ARUnavailableDeviceNotCompatibleException /*e*/)
        {
            ErrorMessage = "This Device does not support AR!";
            UIManager.Instance.QuitButton(QUIT_DELAY);
            return;
        }
        catch (ARUnavailableEmuiNotCompatibleException /*e*/)
        {
            ErrorMessage = "This EMUI does not support AR!";
            UIManager.Instance.QuitButton(QUIT_DELAY);
            return;
        }
        catch (ARUnavailableUserDeclinedInstallationException /*e*/)
        {
            ErrorMessage = "User decline installation right now, quit";
            UIManager.Instance.QuitButton(QUIT_DELAY);
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
                UIManager.Instance.QuitButton(QUIT_DELAY);
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
            UIManager.Instance.QuitButton(QUIT_DELAY);
        }
        catch (ARUnavailableDeviceNotCompatibleException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This device does not support AR";
            UIManager.Instance.QuitButton(QUIT_DELAY);
        }
        catch (ARUnavailableServiceApkTooOldException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This AR Engine is too old, please update";
            UIManager.Instance.QuitButton(QUIT_DELAY);
        }
        catch (ARUnavailableServiceNotInstalledException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This app depend on AREngine.apk, please install it";
            UIManager.Instance.QuitButton(QUIT_DELAY);
        }
        catch (ARUnSupportedConfigurationException /*e*/)
        {
            isErrorHappendWhenInit = true;
            ErrorMessage = "This config is not supported on this device, exit now.";
            UIManager.Instance.QuitButton(QUIT_DELAY);
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
