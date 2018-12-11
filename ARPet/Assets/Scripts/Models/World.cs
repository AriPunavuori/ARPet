using HuaweiARUnitySDK;
using UnityEngine;

public class World : MonoBehaviour
{
    private Color defaultColor;
    private Color ableToPlaceColor = new Color(0, 1, 0, 0.5f);
    private Color unAbleToPlaceColor = new Color(1, 0, 0, 0.5f);

    private Pose pose;
    private Collider worldCollider;
    private MeshRenderer meshRenderer;

    public ARAnchor WorldAnchor
    {
        get;
        private set;
    }
    public Bounds WorldBounds
    {
        get
        {
            return worldCollider.bounds;
        }
    }

    public void Initialize(ARAnchor anchor)
    {
        WorldAnchor = anchor;
        pose = anchor.GetPose();

        transform.SetPositionAndRotation(pose.position, pose.rotation);

        //meshRenderer.enabled = false;
    }

    private void Awake()
    {
        worldCollider = GetComponentInChildren<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        defaultColor = meshRenderer.material.color;
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
        if (WorldAnchor == null)
        {
            meshRenderer.enabled = false;
            return;
        }
        switch (WorldAnchor.GetTrackingState())
        {
            case ARTrackable.TrackingState.TRACKING:
                Pose p = WorldAnchor.GetPose();
                gameObject.transform.position = p.position;
                gameObject.transform.rotation = p.rotation;
                gameObject.transform.Rotate(0f, 225f, 0f, Space.Self);
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

    private void OnDestroy()
    {
        SessionManager.Instance.DetachARAnchor(WorldAnchor);
    }

    public void MoveWorld(Pose newPose)
    {
        transform.SetPositionAndRotation(newPose.position, newPose.rotation);
    }
}
