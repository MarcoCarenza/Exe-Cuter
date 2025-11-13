using UnityEngine;

public class AttachedTo : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (AttachSystem.selectedAttachable == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Align attachable to surface
            var attachable = AttachSystem.selectedAttachable.transform;
            attachable.position = hit.point;

            // Rotate attachable so its forward (Z) faces away from the surface
            attachable.rotation = Quaternion.LookRotation(-hit.normal);

            // Parent attachable to this object
            attachable.SetParent(transform);

            AttachSystem.ClearSelectedAttachable();
        }
    }
}