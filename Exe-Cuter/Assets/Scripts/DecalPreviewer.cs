using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalPreviewer : MonoBehaviour
{
    public static GameObject previewInstance;
    public static DecalPreviewer instance;
    public static AttachedTo currentTarget { get; private set; }

    [Header("Rotation & Scale Settings")]
    public float rotationSpeed = 90f; // degrees/sec
    public float scaleSpeed = 1f;
    public float minScale = 0.1f;
    public float maxScale = 5f;
    public float projectionOffset = 0.01f; //I dont think this works
    

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float scale = 1f; //doesnt do anything???
    
    private DecalProjector decalProjector;
    
    private void Awake()
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
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var target = hit.collider.GetComponentInParent<AttachedTo>();
            if (target != null)
            {
                currentTarget = target; // store current object

                if (previewInstance == null)
                    CreatePreview();

                UpdatePreviewTransform(hit);
                HandleRotation();
                HandleScaling();
            }
            else
            {
                currentTarget = null;
                DestroyPreview();
            }
        }
        else
        {
            DestroyPreview();
        }
    }

    private void CreatePreview()
    {
        previewInstance = Instantiate(DecalSelector.CurrentDecalPrefab);

        // Reset root scale first
        previewInstance.transform.localScale = Vector3.one;

        // Then apply preview scale variable
        previewInstance.transform.localScale = Vector3.one * scale;

        MakeTransparent(previewInstance);
        
        decalProjector = previewInstance.GetComponent<DecalProjector>();
    }

    private void UpdatePreviewTransform(RaycastHit hit)
    {
        if (previewInstance == null) return;

        // Keep preview in world space while hovering
        Vector3 offsetPos = hit.point + hit.normal * projectionOffset;
        previewInstance.transform.position = offsetPos;
        previewInstance.transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(rotationX, rotationY, 0);
        if (decalProjector != null)
            decalProjector.size = Vector3.one * scale;
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
        if (previewInstance == null) return;

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            // Make the scale change noticeable
            float scaleStep = scroll * 0.1f; 
            scale = Mathf.Clamp(scale + scaleStep, 0.1f, 5f);
            if (decalProjector != null)
            {
                decalProjector.size = Vector3.one * scale;
            }
            Debug.Log("Preview scale: " + scale);
        }
    }

    private void MakeTransparent(GameObject obj)
    {
        Renderer[] rends = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            Material m = new Material(r.material);
            Color c = m.color;
            c.a = 0.5f;
            m.color = c;
            m.SetFloat("_Mode", 3); // transparent mode
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;

            r.material = m;
        }
    }

    public void DestroyPreview()
    {
        if (previewInstance != null)
            Destroy(previewInstance);

        previewInstance = null;
        rotationY = 0f;
        scale = 1f;
    }

    public GameObject ConsumePreview()
    {
        GameObject temp = previewInstance;
        previewInstance = null;
        rotationY = 0f;
        scale = 1f;
        return temp;
    }
}
