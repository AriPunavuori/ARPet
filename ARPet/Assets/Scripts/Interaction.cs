using UnityEngine;

public interface IDraggable {
    void OnDragStart(IDragger dragger, Quaternion draggerRotation);
    void OnDragEnd();
    void OnDragContinue(Vector3 target);
    void OnDragContinue(Vector3 target, Quaternion draggerRotation);
    bool IsCurrentlyDraggable();
}

public interface IDragger {
    void BreakDrag();
}