using UnityEngine;

public class DecalSelector : MonoBehaviour
{
    public static GameObject CurrentDecalPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Cast ray and get ALL hits along the ray
            RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
            
            // Sort hits by distance so closer objects get priority
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (RaycastHit hit in hits)
            {
                DecalSource source = hit.collider.GetComponentInParent<DecalSource>();
                if (source != null)
                {
                    CurrentDecalPrefab = source.decalPrefab;
                    Debug.Log("Selected decal: " + source.decalPrefab.name + " at hit: " + hit.collider.name);
                    break; // stop at the first DecalSource found
                }
            }
        }
    }
}