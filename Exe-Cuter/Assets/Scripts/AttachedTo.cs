using UnityEngine;

public class AttachedTo : MonoBehaviour
{
    void OnMouseOver()
    {
        if (AttachSystem.selectedAttachable == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            AttachSystem.selectedAttachable.UpdatePreview(hit);
        }
    }

    void OnMouseExit()
    {
        if (AttachSystem.selectedAttachable != null)
            AttachSystem.selectedAttachable.DestroyPreview();
    }

    void OnMouseDown()
    {
        if (AttachSystem.selectedAttachable == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var at = AttachSystem.selectedAttachable;

            // Final placement
            at.ApplyFinalTransform(hit.point, hit.normal, transform);

            at.attachSystem.playPlaceDownSFX();

            AttachSystem.ClearSelectedAttachable();
        }
    }
}