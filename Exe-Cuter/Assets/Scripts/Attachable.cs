using UnityEngine;

public class Attachable : MonoBehaviour
{
    public AttachSystem attachSystem;

    public bool isAGem = false;

    private Renderer[] renderers;
    private Color[] originalColors;
    [SerializeField] private Color highlightColor = Color.yellow;

    private GameObject previewInstance;
    [SerializeField] public Color previewColor = new Color(1f, 1f, 1f, 0.4f);

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float currentScale = 1f;

    private const float rotationSpeed = 90f;
    private const float scaleSpeed = 0.1f;
    private const float minScale = 0.2f;
    private const float maxScale = 4f;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;
    }

    // --------------------------
    // PREVIEW UPDATE
    // --------------------------
    public void UpdatePreview(RaycastHit hit)
    {
        if (previewInstance == null)
            CreatePreview();

        HandleRotationInput();
        HandleScaleInput();

        previewInstance.transform.position = hit.point;
        previewInstance.transform.rotation =
            Quaternion.LookRotation(-hit.normal) *
            Quaternion.Euler(rotationX, rotationY, 0);

        previewInstance.transform.localScale = Vector3.one * currentScale;
    }

    // --------------------------
    // PREVIEW CREATION
    // --------------------------
    private void CreatePreview()
    {
        previewInstance = Instantiate(gameObject);
        DestroyPreviewColliders(previewInstance);

        Renderer[] rds = previewInstance.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rds)
        {
            Material m = new Material(r.material);
            m.color = previewColor;
            r.material = m;
        }
    }

    private void DestroyPreviewColliders(GameObject obj)
    {
        foreach (Collider c in obj.GetComponentsInChildren<Collider>())
            Destroy(c);
    }

    public void DestroyPreview()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
    }

    // --------------------------
    // INPUT DURING PREVIEW
    // --------------------------
    private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.A)) rotationY -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) rotationY += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) rotationX -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) rotationX += rotationSpeed * Time.deltaTime;
    }

    private void HandleScaleInput()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentScale = Mathf.Clamp(
                currentScale + scroll * scaleSpeed,
                minScale,
                maxScale
            );
        }
    }

    // --------------------------
    // FINAL ATTACH
    // --------------------------
    public void ApplyFinalTransform(Vector3 pos, Vector3 normal, Transform parent)
    {
        DestroyPreview();

        transform.position = pos;
        transform.rotation =
            Quaternion.LookRotation(-normal) *
            Quaternion.Euler(rotationX, rotationY, 0);

        transform.localScale = Vector3.one * currentScale;

        transform.SetParent(parent);

        MarkAsAttached();
    }

    public void MarkAsAttached()
    {
        DestroyPreview();

        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;
    }

    // --------------------------
    // HIGHLIGHT
    // --------------------------
    public void SelectEffect()
    {
        foreach (Renderer r in renderers)
            r.material.color = highlightColor;
    }

    public void DeselectEffect()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }
}
