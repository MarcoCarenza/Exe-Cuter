using UnityEngine;

public class AttachedTo : MonoBehaviour
{
    
    private void OnMouseOver()
    {
        if (AttachSystem.selectedAttachable == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Update preview position and rotation
            AttachSystem.selectedAttachable.UpdatePreview(hit);
        }
    }

    private void OnMouseExit()
    {
        if (AttachSystem.selectedAttachable != null)
            AttachSystem.selectedAttachable.DestroyPreview();
    }
    
    private void OnMouseDown()
    {
        if (AttachSystem.selectedAttachable == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var attachable = AttachSystem.selectedAttachable;

            // Destroy the preview BEFORE parenting
            attachable.DestroyPreview();

            // Move and rotate attachable to surface
            attachable.transform.position = hit.point;
            attachable.transform.rotation = Quaternion.LookRotation(-hit.normal);

            // Parent it to this object
            attachable.transform.SetParent(transform);
            
            // Mark as attached so it can’t be selected again
            attachable.MarkAsAttached();
            
            // Play object specific audio:
            attachable.attachSystem.playPlaceDownSFX();

            // Deselect attachable
            AttachSystem.ClearSelectedAttachable();
        }
    }
}