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
    public void SelectNewAttachable(Attachable newAttachable)
    {
        // Deselect the previous one
        if (selectedAttachable != null && selectedAttachable != newAttachable)
            ClearSelectedAttachable();

        // Select the new one
        selectedAttachable = newAttachable;
        selectedAttachable.SelectEffect();
        playPickupSFX();
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
