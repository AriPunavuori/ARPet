using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region VARIABLES

    private const float MAX_RAY_DISTANCE = 100f;

    [SerializeField]
    private LayerMask mouseInteractLayer;

    #endregion VARIABLES

    #region PROPERTIES

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        MouseButton(0);
        MouseButton(1);
    }

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS

    private void Initialize()
    {

    }

    private void MouseButton(int mouseButtonIndex)
    {
        switch (mouseButtonIndex)
        {
            case 0:
                if (Input.GetMouseButtonDown(0))
                {
                   
                }
                else if (Input.GetMouseButton(0))
                {
                    var ray = CameraEngine.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                    var hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit, MAX_RAY_DISTANCE, mouseInteractLayer))
                    {
                        Debug.Log(hit.collider.name);
                        Debug.DrawRay(ray.origin, ray.direction * MAX_RAY_DISTANCE, Color.red);
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {

                }
                break;

            case 1:
                if (Input.GetMouseButtonDown(1))
                {

                }
                else if (Input.GetMouseButton(1))
                {

                }
                else if (Input.GetMouseButtonUp(1))
                {

                }
                break;

            default:

                break;
        }
    }

    #endregion CUSTOM_FUNCTIONS
}
