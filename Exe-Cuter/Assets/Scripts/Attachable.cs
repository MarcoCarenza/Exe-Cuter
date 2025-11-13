using UnityEngine;

public class Attachable : MonoBehaviour
{
    public AttachSystem attachSystem;
    private Renderer rend;
    private Color originalColor;
    [SerializeField] private Color highlightColor = Color.yellow;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        attachSystem.SelectNewAttachable(this);
    }

    public void SetHighlight(bool enabled)
    {
        if (rend == null) return;
        rend.material.color = enabled ? highlightColor : originalColor;
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
