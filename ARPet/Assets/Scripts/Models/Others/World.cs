using HuaweiARUnitySDK;
using UnityEngine;
using UnityEngine.AI;

public class World : MonoBehaviour
{
    private Collider worldCollider;
    private MeshRenderer meshRenderer;
    private NavMeshSurface[] navMeshSurfaces;

    public ARAnchor WorldAnchor
    {
        get;
        private set;
    }
    public Pose WorldAnchorPose
    {
        get
        {
            return WorldAnchor.GetPose();
        }
    }
    public Bounds WorldBounds
    {
        get
        {
            return worldCollider.bounds;
        }
    }

    public void Initialize(Anchor anchor)
    {
        WorldAnchor = anchor.ARAnchor;

        transform.SetPositionAndRotation(WorldAnchorPose.position, WorldAnchorPose.rotation);

        //meshRenderer.enabled = false;
    }

    private void Awake()
    {
        worldCollider = GetComponentInChildren<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        navMeshSurfaces = GetComponents<NavMeshSurface>();
    }

    private void Start()
    {
        BuildNavMesh(navMeshSurfaces);
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
       TrackWorld();
    }

    private void OnDestroy()
    {
        SessionManager.Instance.DetachARAnchor(WorldAnchor);
    }

    public void BuildNavMesh(NavMeshSurface[] navMeshSurfaces)
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    private void TrackWorld()
    {
        if (WorldAnchor == null)
        {
            meshRenderer.enabled = false;
            return;
        }
        switch (WorldAnchor.GetTrackingState())
        {
            case ARTrackable.TrackingState.TRACKING:

                MoveWorld(WorldAnchorPose);

                // transform.Rotate(0f, 225f, 0f, Space.Self);

                meshRenderer.enabled = true;
                break;
            case ARTrackable.TrackingState.PAUSED:
                meshRenderer.enabled = false;
                break;
            case ARTrackable.TrackingState.STOPPED:
            default:
                meshRenderer.enabled = false;
                Destroy(gameObject);
                break;
        }
    }

    public void MoveWorld(Pose newPose)
    {
        transform.SetPositionAndRotation(newPose.position, newPose.rotation);
    }
}
