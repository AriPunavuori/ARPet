using System.Collections.Generic;
using HuaweiARUnitySDK;
using UnityEngine;

public class PlaneVisualizer : MonoBehaviour
{
    #region VARIABLES

    private static int planeCount = 0;

    private readonly Color[] planeColors = new Color[]
    {
            new Color(1.0f, 1.0f, 1.0f),
            new Color(0.5f,0.3f,0.9f),
            new Color(0.8f,0.4f,0.8f),
            new Color(0.5f,0.8f,0.4f),
            new Color(0.5f,0.9f,0.8f)
    };

    private ARPlane trackedPlane;

    // Keep previous frame's mesh polygon to avoid mesh update every frame.
    private List<Vector3> previousFrameMeshVertices = new List<Vector3>();
    private List<Vector3> meshVertices3D = new List<Vector3>();
    private List<Vector2> meshVertices2D = new List<Vector2>();

    private readonly List<Color> meshColors = new List<Color>();

    private Mesh mesh;
    private MeshRenderer meshRenderer;

    #endregion VARIABLES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (trackedPlane == null)
        {
            return;
        }
        else if (trackedPlane.GetSubsumedBy() != null
            || trackedPlane.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
        {
            Destroy(gameObject);
            return;
        }
        else if (trackedPlane.GetTrackingState() == ARTrackable.TrackingState.PAUSED) // whether to destory gameobject if not tracking
        {
            meshRenderer.enabled = false;
            return;
        }

        meshRenderer.enabled = true;
        UpdateMeshIfNeeded();

        GameMaster.Instance.CheckCanWeBuild(mesh);
    }
    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    public void Initialize(ARPlane plane)
    {
        trackedPlane = plane;

        meshRenderer.material.SetColor("_GridColor", planeColors[planeCount++ % planeColors.Length]);

        Update();
    }

    private void UpdateMeshIfNeeded()
    {
        meshVertices3D.Clear();
        trackedPlane.GetPlanePolygon(meshVertices3D);

        if (AreVerticesListsEqual(previousFrameMeshVertices, meshVertices3D))
        {
            return;
        }

        Pose centerPose = trackedPlane.GetCenterPose();
        for (int i = 0; i < meshVertices3D.Count; i++)
        {
            meshVertices3D[i] = centerPose.rotation * meshVertices3D[i] + centerPose.position;
        }

        Vector3 planeNormal = centerPose.rotation * Vector3.up;
        meshRenderer.material.SetVector("_PlaneNormal", planeNormal);

        previousFrameMeshVertices.Clear();
        previousFrameMeshVertices.AddRange(meshVertices3D);

        meshVertices2D.Clear();
        trackedPlane.GetPlanePolygon(ref meshVertices2D);

        Triangulator triangulator = new Triangulator(meshVertices2D);

        mesh.Clear();
        mesh.SetVertices(meshVertices3D);
        mesh.SetIndices(triangulator.Triangulate(), MeshTopology.Triangles, 0);
        mesh.SetColors(meshColors);
    }

    private bool AreVerticesListsEqual(List<Vector3> firstList, List<Vector3> secondList)
    {
        if (firstList.Count != secondList.Count)
        {
            return false;
        }

        for (int i = 0; i < firstList.Count; i++)
        {
            if (firstList[i] != secondList[i])
            {
                return false;
            }
        }

        return true;
    }

    #endregion CUSTOM_FUNCTIONS
}
