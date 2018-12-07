using HuaweiARUnitySDK;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraEngine : Singelton<CameraEngine>
{
    #region VARIABLES

    private ARHitResult hitResult;
    private ARTrackable currentTrackable;
    private List<ARAnchor> planeAnchors = new List<ARAnchor>();

    private GameObject touchHitPointObject;
    private GameObject worldObject;
    private LineRenderer lineRenderer;

    private const string BACKGROUND_TEXTURE = "_MainTex";
    private const string LEFT_TOP_BOTTOM = "_UvLeftTopBottom";
    private const string RIGHT_TOP_BOTTOM = "_UvRightTopBottom";

    private ARBackgroundRenderer backgroundRenderer;
    private static readonly float[] QUAD_TEXCOORDS = { 0f, 1f, 0f, 0f, 1f, 1f, 1f, 0f };
    private float[] transformedUVCoords = QUAD_TEXCOORDS;

    private Material ARBackground_mat;

    #endregion VARIABLES

    #region PROPERTIES

    public Camera MainCamera { get; private set; }
    public Pose CameraPose { get { return ARFrame.GetPose(); } }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        MainCamera = GetComponent<Camera>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        ARBackground_mat = ResourceManager.Instance.GetFromResources<Material>("Materials", "ARBackground_mat");
        touchHitPointObject = Instantiate(ResourceManager.Instance.GetFromResources<GameObject>("Models", "TouchHitPoint"));
        worldObject = Instantiate(ResourceManager.Instance.GetFromResources<GameObject>("Models", "World"));
    }

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    public void Update()
    {
        UpdateARCamera();

        if (Input.touchCount > 0)
        {
            ShootRay(Input.GetTouch(0));
        }

        UIManager.Instance.UpdateHitObjectText = currentTrackable.GetType().Name;
        UIManager.Instance.UpdateHitDistance = hitResult.Distance.ToString();
        UIManager.Instance.UpdateHitPoseText = hitResult.HitPose.ToString();
        UIManager.Instance.UpdatePlaneAnchorsText = planeAnchors.Count.ToString();
        UIManager.Instance.UpdatePoseText = CameraPose.ToString();
    }

    #endregion UNITY_FUNCTIONS

    private void UpdateARCamera()
    {
        if (ARBackground_mat == null || !ARFrame.TextureIsAvailable())
        {
            return;
        }

        ARBackground_mat.SetTexture(BACKGROUND_TEXTURE, ARFrame.CameraTexture);

        if (ARFrame.IsDisplayGeometryChanged())
        {
            transformedUVCoords = ARFrame.GetTransformDisplayUvCoords(QUAD_TEXCOORDS);
        }

        ARBackground_mat.SetVector(LEFT_TOP_BOTTOM, new Vector4(transformedUVCoords[0], transformedUVCoords[1],
            transformedUVCoords[2], transformedUVCoords[3]));
        ARBackground_mat.SetVector(RIGHT_TOP_BOTTOM, new Vector4(transformedUVCoords[4], transformedUVCoords[5],
            transformedUVCoords[6], transformedUVCoords[7]));

        MainCamera.transform.SetPositionAndRotation(CameraPose.position, CameraPose.rotation);

        MainCamera.projectionMatrix = ARSession.GetProjectionMatrix(MainCamera.nearClipPlane, MainCamera.farClipPlane);

        if (backgroundRenderer == null)
        {
            backgroundRenderer = new ARBackgroundRenderer
            {
                backgroundMaterial = ARBackground_mat,
                camera = MainCamera,
                mode = ARRenderMode.MaterialAsBackground
            };
        }
    }

    private void ShootRay(Touch touch)
    {
        List<ARHitResult> hitResults = ARFrame.HitTest(touch);

        hitResult = hitResults[0];

        currentTrackable = hitResult.GetTrackable();

        if(currentTrackable.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
        {           
            if(currentTrackable is ARPlane)
            {
 
            }

            currentTrackable.GetAllAnchors(planeAnchors);

            lineRenderer.SetPosition(0, touch.position);
            lineRenderer.SetPosition(1, hitResult.HitPose.position);
            lineRenderer.SetPosition(2, hitResult.HitPose.up * 2);
            lineRenderer.enabled = true;

            touchHitPointObject.transform.SetPositionAndRotation(hitResult.HitPose.position, hitResult.HitPose.rotation);

            worldObject.transform.SetPositionAndRotation(hitResult.HitPose.position, hitResult.HitPose.rotation);

            touchHitPointObject.SetActive(true);                             
        }           
        

        switch (touch.phase)
        {
            case TouchPhase.Began:            

                break;

            case TouchPhase.Moved:

                

                break;

            case TouchPhase.Stationary:

                break;  
                
            case TouchPhase.Ended:

                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, Vector3.zero);
                lineRenderer.SetPosition(2, Vector3.zero);
                lineRenderer.enabled = false;

                touchHitPointObject.SetActive(false);

                break;

            case TouchPhase.Canceled:

                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, Vector3.zero);
                lineRenderer.SetPosition(2, Vector3.zero);
                lineRenderer.enabled = false;

                touchHitPointObject.SetActive(false);

                break;

            default:

                break;
        }

        UIManager.Instance.UpdateTouchPhase = touch.phase.ToString();       
    }
}
