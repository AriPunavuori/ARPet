using UnityEngine;

public interface IDraggable {
    void OnDragStart(IDragger dragger);
    void OnDragEnd();
    void OnDragContinue(Vector3 target);
    void OnDragContinue(Vector3 target, Quaternion rotation);
    bool IsCurrentlyDraggable();
}

public interface IDragger {
    void BreakDrag();
}