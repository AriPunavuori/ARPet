using UnityEngine;

public class HitIndicator : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();    
    }

    public void ChangeState(bool isShowing)
    {
        meshRenderer.enabled = isShowing;
    }

    public void ChangeColor(Color newColor)
    {
        meshRenderer.material.color = newColor;
    }
}
