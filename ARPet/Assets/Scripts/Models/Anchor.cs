using HuaweiARUnitySDK;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private ARAnchor arAnchor;

    public void Initialize(ARAnchor arAnchor)
    {
        this.arAnchor = arAnchor;
    }

    private void OnDestroy()
    {
        SessionManager.Instance.DetachARAnchor(arAnchor);
    }
}
