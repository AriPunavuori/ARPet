using HuaweiARUnitySDK;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private ARAnchor arAnchor;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Initialize(ARAnchor arAnchor)
    {
        this.arAnchor = arAnchor;
    }

    private void Update()
    {
        if (arAnchor == null)
        {
            meshRenderer.enabled = false;
            return;
        }
        switch (arAnchor.GetTrackingState())
        {
            case ARTrackable.TrackingState.TRACKING:
                Pose p = arAnchor.GetPose();
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
        SessionManager.Instance.DetachARAnchor(arAnchor);
    }
}
