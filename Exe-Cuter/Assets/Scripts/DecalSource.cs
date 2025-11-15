using UnityEngine;

public class DecalSource : MonoBehaviour
{
    public GameObject decalPrefab;

    public Color highlightColor = Color.yellow;

    private Renderer[] renderers;
    private Color[] originals;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originals = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            originals[i] = renderers[i].material.color;
    }

    public void SelectEffect()
    {
        foreach (var r in renderers)
            r.material.color = highlightColor;
    }

    public void DeselectEffect()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originals[i];
    }
}