using UnityEngine;

public class DecalPainter : MonoBehaviour
{
    void Update()
    {
        if (DecalSelector.CurrentDecalPrefab == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var attachedTo = hit.collider.GetComponentInParent<AttachedTo>();
                if (attachedTo == null)
                    return;
                if (DecalPreviewer.previewInstance != null && DecalPreviewer.currentTarget != null)
                {
                    GameObject finalDecal = DecalPreviewer.previewInstance;
                    DecalPreviewer.previewInstance = null;

                    // Compute local position/rotation relative to target
                    finalDecal.transform.SetParent(DecalPreviewer.currentTarget.transform);
                    finalDecal.transform.position += finalDecal.transform.forward * DecalPreviewer.instance.projectionOffset;
                    finalDecal.transform.localRotation = Quaternion.Inverse(DecalPreviewer.currentTarget.transform.rotation) * finalDecal.transform.rotation;

                    finalDecal.name = "PlacedDecal";
                }
            }
        }
    }
}