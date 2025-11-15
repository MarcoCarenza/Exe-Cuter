using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalPreviewer : MonoBehaviour
{
    public static GameObject previewInstance;
    public static DecalPreviewer instance;

    public static AttachedTo currentTarget { get; private set; }

    [Header("Rotation & Scale Settings")]
    public float rotationSpeed = 90f;
    public float scaleSpeed = 0.1f;
    public float minScale = 0.1f;
    public float maxScale = 5f;
    public float projectionOffset = 0.01f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    public float currentScale = 1f;   // <-- used by final decal

    private DecalProjector decalProjector;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (DecalSelector.CurrentDecalPrefab == null)
        {
            DestroyPreview();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            DestroyPreview();
            return;
        }

        var target = hit.collider.GetComponentInParent<AttachedTo>();
        if (target == null)
        {
            DestroyPreview();
            return;
        }

        currentTarget = target;

        if (previewInstance == null)
            CreatePreview();

        UpdatePreviewTransform(hit);
        HandleRotation();
        HandleScaling();
    }

    private void CreatePreview()
    {
        previewInstance = Instantiate(DecalSelector.CurrentDecalPrefab);

        decalProjector = previewInstance.GetComponent<DecalProjector>();

        // Initial projector size
        decalProjector.size = Vector3.one * currentScale;

        MakeTransparent(previewInstance);
    }

    private void UpdatePreviewTransform(RaycastHit hit)
    {
        if (!previewInstance || decalProjector == null)
            return;

        // Position
        Vector3 pos = hit.point + hit.normal * projectionOffset;
        decalProjector.transform.position = pos;

        // Rotation
        decalProjector.transform.rotation =
            Quaternion.LookRotation(hit.normal) *
            Quaternion.Euler(rotationX, rotationY, 0);

        // Scaling (size, not transform scale!)
        decalProjector.size = Vector3.one * currentScale;
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A)) rotationY -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) rotationY += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) rotationX -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) rotationX += rotationSpeed * Time.deltaTime;
    }

    private void HandleScaling()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentScale = Mathf.Clamp(currentScale + scroll * scaleSpeed, minScale, maxScale);
            decalProjector.size = Vector3.one * currentScale;

            Debug.Log("Preview projector size = " + decalProjector.size);
        }
    }

    private void MakeTransparent(GameObject obj)
    {
        foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
        {
            if (!r.material) continue;
            Material m = new Material(r.material);
            Color c = m.color;
            c.a = 0.5f;
            m.color = c;
            r.material = m;
        }
    }

    public void DestroyPreview()
    {
        if (previewInstance)
            Destroy(previewInstance);

        previewInstance = null;
        rotationX = rotationY = 0f;
        currentScale = 1f;
    }
}
