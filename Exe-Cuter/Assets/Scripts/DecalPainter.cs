using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalPainter : MonoBehaviour
{
    void Update()
    {
        if (DecalSelector.CurrentDecalPrefab == null)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;
        
        if (DecalPreviewer.previewInstance != null && DecalPreviewer.currentTarget != null)
        {
            GameObject finalDecal = DecalPreviewer.previewInstance;
            DecalPreviewer.previewInstance = null;

            DecalProjector projector = finalDecal.GetComponent<DecalProjector>();
            Transform target = DecalPreviewer.currentTarget.transform;

            // --- NEW: Save world transform BEFORE parenting ---
            Vector3 worldPos = projector.transform.position;
            Quaternion worldRot = projector.transform.rotation;
            Vector3 size = projector.size;   // Keep chosen scale

            // Parent under target
            projector.transform.SetParent(target, worldPositionStays: false);

            // Restore world → local
            projector.transform.position = worldPos;
            projector.transform.rotation = worldRot;

            // Restore size
            projector.size = size;

            finalDecal.name = "PlacedDecal";
        }
    }
}