using UnityEngine;

public class AttachSystem : MonoBehaviour
{
    public static Attachable selectedAttachable;
    private AudioSource _as;
    public AudioClip pickupSFX;
    public AudioClip placeDownSFX;

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
        _as.clip = placeDownSFX;
        _as.Play();
    }
    
    public void playPickupSFX()
    {
        _as.Stop();
        _as.clip = pickupSFX;
        _as.Play();
    }
}
