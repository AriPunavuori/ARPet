using HuaweiARUnitySDK;
using UnityEngine;

public class World : MonoBehaviour
{
    private Pose pose;
    private Collider worldCollider;

    public ARAnchor WorldAnchor { get; private set; }
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
    }

    private void Awake()
    {
        worldCollider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
    
    }

    public void MoveWorld(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
