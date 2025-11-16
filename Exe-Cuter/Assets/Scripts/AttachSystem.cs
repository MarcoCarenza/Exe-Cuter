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
        // ----------------------------
        // ESC CANCEL
        // ----------------------------
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectedAttachable != null)
            {
                selectedAttachable.DestroyPreview();
                ClearSelectedAttachable();
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (RaycastHit hit in hits)
            {
                Attachable at = hit.collider.GetComponentInParent<Attachable>();
                if (at != null)
                {
                    ClearSelectedAttachable();
                    selectedAttachable = at;
                    selectedAttachable.SelectEffect();
                    playPickupSFX();
                    return;
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
