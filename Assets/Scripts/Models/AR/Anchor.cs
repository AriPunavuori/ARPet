using HuaweiARUnitySDK;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private Pose currentPose;

    public ARAnchor ARAnchor { get; private set; }

    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Initialize(ARAnchor arAnchor)
    {
        this.ARAnchor = arAnchor;
    }

    private void Update()
    {
        // UpdateAnchorPose();
    }

    private void UpdateAnchorPose()
    {
        if (ARAnchor == null)
        {
            meshRenderer.enabled = false;
            return;
        }
        switch (ARAnchor.GetTrackingState())
        {
            case ARTrackable.TrackingState.TRACKING:
                currentPose = ARAnchor.GetPose();
                transform.SetPositionAndRotation(currentPose.position, currentPose.rotation);
                //gameObject.transform.Rotate(0f, 225f, 0f, Space.Self);
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
        SessionManager.Instance.DetachARAnchor(ARAnchor);
    }
}
