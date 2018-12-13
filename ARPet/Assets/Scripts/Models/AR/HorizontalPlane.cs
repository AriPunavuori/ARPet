using System.Collections.Generic;
using HuaweiARUnitySDK;
using UnityEngine;

public class HorizontalPlane : MonoBehaviour
{
    private static int planeCount = 0;

    private readonly Color[] planeColor = new Color[]
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

    private MeshCollider meshCollider;
    private MeshRenderer meshRenderer;

    public Pose TrackedPlaneCenterPose
    {
        get
        {
            return trackedPlane.GetCenterPose();
        }
    }
    public Vector3 TrackedPlanePlaneNormal
    {
        get
        {
            return TrackedPlaneCenterPose.rotation * Vector3.up;
        }
    }
    public Bounds PlaneColliderBounds
    {
        get
        {
            return meshCollider.bounds;
        }
    }

    public void Awake()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        meshCollider = GetComponentInChildren<MeshCollider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Update()
    {
        UpdateHorizontalPlaneTracking();        
    }

    public void Initialize(ARPlane plane)
    {
        trackedPlane = plane;

        meshRenderer.material.SetColor("_GridColor", planeColor[planeCount++ % planeColor.Length]);
        meshRenderer.enabled = true;
        Update();
    }

    private void UpdateHorizontalPlaneTracking()
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

        UpdateARHorizontalPlane();
    }

    private void UpdateARHorizontalPlane()
    {
        meshVertices3D.Clear();
        trackedPlane.GetPlanePolygon(meshVertices3D);

        // UpdateMeshIfNeeded
        if (AreVerticesListsEqual(previousFrameMeshVertices, meshVertices3D))
        {
            return;
        }

        for (int i = 0; i < meshVertices3D.Count; i++)
        {
            meshVertices3D[i] = TrackedPlaneCenterPose.rotation * meshVertices3D[i] + TrackedPlaneCenterPose.position;
        }

        meshRenderer.material.SetVector("_PlaneNormal", TrackedPlanePlaneNormal);

        previousFrameMeshVertices.Clear();
        previousFrameMeshVertices.AddRange(meshVertices3D);

        meshVertices2D.Clear();
        trackedPlane.GetPlanePolygon(ref meshVertices2D);

        Triangulator triangulator = new Triangulator(meshVertices2D);

        mesh.Clear();
        mesh.SetVertices(meshVertices3D);
        mesh.SetIndices(triangulator.Triangulate(), MeshTopology.Triangles, 0);
        mesh.SetColors(meshColors);

        meshCollider.sharedMesh = mesh;
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
}