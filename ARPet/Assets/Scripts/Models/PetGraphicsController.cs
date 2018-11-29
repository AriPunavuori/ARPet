using UnityEngine;

public class PetGraphicsController : MonoBehaviour
{
    #region VARIABLES

    private MeshRenderer meshRenderer;

    #endregion VARIABLES

    #region PROPERTIES

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void ChangeMaterialColor(Color newColor)
    {
        meshRenderer.material.color = newColor;
    }

    #endregion CUSTOM_FUNCTIONS
}
