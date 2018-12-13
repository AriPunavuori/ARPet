using UnityEngine;

public class Block : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private Vector3 lastPlacementPosition;

    private bool isFirstPlacement;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void StartPlacing()
    {
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
    }

    public void SetNewPosition(Vector3 newPosition)
    {
        rigidbody.transform.position = newPosition;
    }

    public void NewPlacement(Vector3 newPlacementPosition)
    {
        rigidbody.isKinematic = true;
        isFirstPlacement = true;
        rigidbody.useGravity = true;

        lastPlacementPosition = newPlacementPosition;
    }

    public void CancelPlacement()
    {
        if (isFirstPlacement)
        {
            rigidbody.transform.position = lastPlacementPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
