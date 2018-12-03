using HuaweiARUnitySDK;
using UnityEngine;
using UnityEngine.XR;

public class CameraEngine : Singelton<CameraEngine>
{
    #region VARIABLES

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

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        MainCamera = GetComponent<Camera>();
        ARBackground_mat = Resources.Load<Material>("Materials/ARBackground_mat") as Material;
    }

    public void Update()
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

        Pose pose = ARFrame.GetPose();

        MainCamera.transform.SetPositionAndRotation(pose.position, pose.rotation);

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

    #endregion UNITY_FUNCTIONS
}
