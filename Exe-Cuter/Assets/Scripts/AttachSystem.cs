using UnityEngine;

public class AttachSystem : MonoBehaviour
{
    public static Attachable selectedAttachable;
    public void SelectNewAttachable(Attachable newAttachable)
    {
        // Deselect the previous one
        if (selectedAttachable != null && selectedAttachable != newAttachable)
            ClearSelectedAttachable();

        // Select the new one
        selectedAttachable = newAttachable;
        selectedAttachable.SelectEffect();
    }

    public static void ClearSelectedAttachable()
    {
        if (selectedAttachable != null)
        {
            selectedAttachable.DeselectEffect();
            selectedAttachable = null;
        }
    }
}
