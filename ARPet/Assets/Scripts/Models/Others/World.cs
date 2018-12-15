using HuaweiARUnitySDK;
using UnityEngine;
using UnityEngine.AI;

public class World : MonoBehaviour
{
    private Collider worldCollider;
    private MeshRenderer meshRenderer;
    private NavMeshSurface navMeshSurface;

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

        //transform.SetPositionAndRotation(WorldAnchorPose.position, WorldAnchorPose.rotation);

        //meshRenderer.enabled = false;
    }

    private void Awake()
    {
        worldCollider = GetComponentInChildren<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        navMeshSurface.BuildNavMesh();

        Instantiate(ResourceManager.Instance.HuabotPrefab, transform.position + new Vector3(0, .1f, 0), Quaternion.identity);
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
       //TrackWorld();
    }

    private void OnDestroy()
    {
        SessionManager.Instance.DetachARAnchor(WorldAnchor);
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

                transform.Rotate(0f, 225f, 0f, Space.Self);

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
