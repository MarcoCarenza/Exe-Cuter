using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalCube : MonoBehaviour
{
    public GameObject decal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Cube click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var attachedTo = hit.collider.GetComponentInParent<AttachedTo>();
                if (attachedTo == null)
                {
                    Debug.LogWarning("NULL");
                    return;

                }

                this.gameObject.transform.SetPositionAndRotation(hit.point,this.gameObject.transform.rotation);
                this.gameObject.transform.SetParent(attachedTo.transform);

                this.gameObject.name = "Cube?";
                
            }
        }
    }
}
