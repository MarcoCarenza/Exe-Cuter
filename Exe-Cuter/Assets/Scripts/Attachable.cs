using UnityEngine;

public class Attachable : MonoBehaviour
{
    public AttachSystem attachSystem;
    public AudioClip pickupSFX;
    public AudioClip placeDownSFX;

    private Renderer[] renderers;
    private Color[] originalColors;
    [SerializeField] private Color highlightColor = Color.yellow;
    
    private GameObject previewInstance;
    [SerializeField] public Color previewColor = new Color(0.1f, 0.1f, 0.1f, 0.1f);
    

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;
    }

    private void OnMouseDown()
    {
        attachSystem.SelectNewAttachable(this);
    }
    
    public void UpdatePreview(RaycastHit hit)
    {
        if (previewInstance == null)
            CreatePreview();

        previewInstance.transform.position = hit.point;
        previewInstance.transform.rotation = Quaternion.LookRotation(-hit.normal);
    }
    
    private void CreatePreview()
    {
        previewInstance = Instantiate(gameObject);
        DestroyPreviewColliders(previewInstance);

        Renderer[] previewRenderers = previewInstance.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in previewRenderers)
        {
            Material m = new Material(r.material);
            m.color = previewColor;
            r.material = m;
        }
    }

    private void DestroyPreviewColliders(GameObject obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
            Destroy(c);
    }

    public void DestroyPreview()
    {
        if (previewInstance != null)
        {
            // Unparent to avoid being destroyed along with anything else
            previewInstance.transform.parent = null;
            Destroy(previewInstance);
            previewInstance = null;
        }
    }
    
    public void MarkAsAttached()
    {
        DestroyPreview();

        // Disable colliders so it can't be clicked again
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (var c in cols)
            c.enabled = false;
    }


    private void SetHighlight(bool enabled)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = enabled ? highlightColor : originalColors[i];
        }
    }
    
    public void DeselectEffect()
    {
        SetHighlight(false);
    }
    
    public void SelectEffect()
    {
        SetHighlight(true);
    }
}
