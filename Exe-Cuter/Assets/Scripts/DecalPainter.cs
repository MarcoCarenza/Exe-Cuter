using UnityEngine;

public class DecalPainter : MonoBehaviour
{
    public DecalCube cube;
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
                    //MARCO MAKING CHANGED BELOW IS ORIGINAL
                    // finalDecal.transform.SetParent(DecalPreviewer.currentTarget.transform);
                    //  finalDecal.transform.position += finalDecal.transform.forward * DecalPreviewer.instance.projectionOffset;
                    //  finalDecal.transform.localRotation = Quaternion.Inverse(DecalPreviewer.currentTarget.transform.rotation) * finalDecal.transform.rotation;


                    //new code
                   
                    Vector3 scale = new Vector3(.1f,.1f,.1f);

                    finalDecal.transform.localScale = scale;
                    finalDecal.transform.SetParent(DecalPreviewer.currentTarget.transform);
                    finalDecal.transform.position = cube.transform.position;
                  

                    finalDecal.name = "PlacedDecal";
                   
                }
            }
        }
    }
}