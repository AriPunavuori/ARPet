using HuaweiARUnitySDK;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private Pose anchorePose;

    public Anchor(Pose anchorePose)
    {
        this.anchorePose = anchorePose;

        ARSession.AddAnchor(anchorePose);
    }
}
