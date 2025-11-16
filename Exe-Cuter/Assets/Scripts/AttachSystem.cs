using UnityEngine;

public class AttachSystem : MonoBehaviour
{
    public static Attachable selectedAttachable;
    private AudioSource _as;
    public AudioClip pickupSFXGem;
    public AudioClip placeDownSFXGem;
    public AudioClip pickupSFXPlushies;
    public AudioClip placeDownSFXPlushies;
    
    void Start()
    {
        _as = GetComponent<AudioSource>();
    }
    
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
                Attachable attachable = hit.collider.GetComponentInParent<Attachable>();
                if (attachable != null)
                {
                    // Deselect the previous one
                    ClearSelectedAttachable();
                    // Select the new one
                    selectedAttachable = attachable;
                    selectedAttachable.SelectEffect();
                    playPickupSFX();
                }
            }
        }
    }

    public static void ClearSelectedAttachable()
    {
        if (selectedAttachable != null)
        {
            selectedAttachable.DeselectEffect();
            selectedAttachable = null;
        }
    }

    public void playPlaceDownSFX()
    {
        _as.Stop();
        _as.clip = selectedAttachable.isAGem ? placeDownSFXGem : placeDownSFXPlushies;
        _as.Play();
    }
    
    public void playPickupSFX()
    {
        _as.Stop();
        _as.clip = selectedAttachable.isAGem ? pickupSFXGem : pickupSFXPlushies;
        _as.Play();
    }
}
