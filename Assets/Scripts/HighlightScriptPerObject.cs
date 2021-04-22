using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScriptPerObject : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;

    private Renderer selectionRenderer;

    private void Start()
    {
        selectionRenderer = this.GetComponent<Renderer>();
        selectionRenderer.material = defaultMaterial;
    }

    public void HighLight(bool enable)
    {
        if (selectionRenderer != null && enable)
        {
            selectionRenderer.material = highlightMaterial;
        }
        else if (!enable)
        {
            selectionRenderer.material = defaultMaterial;
        }
    }
}