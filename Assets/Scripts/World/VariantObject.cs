using System.Collections.Generic;
using UnityEngine;

public class VariantObject : MonoBehaviour
{
    public int SelectedVariantIndex => _selectedVariantIndex;
    public string SelectedVariantName => _variants == null || _variants.Count == 0 ? "none" : _variants[_selectedVariantIndex].name;
    public int VariantCount => _variants == null ? 0 : _variants.Count;

    [SerializeField] List<Transform> _variants;
    [SerializeField] bool _cleanupOnAwake = false;
    [SerializeField] int _selectedVariantIndex;

    private void Awake()
    {
        SelectVariant(_selectedVariantIndex);

        if (_cleanupOnAwake)
            Cleanup();
    }

    public void SelectVariant(int index)
    {
        if (index < 0) return;
        if (index >= VariantCount) return;

        _selectedVariantIndex = index;
        _selectedVariantIndex = Mathf.Min(_selectedVariantIndex, _variants.Count - 1);
        _selectedVariantIndex = Mathf.Max(_selectedVariantIndex, 0);

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        transform.GetChild(index).gameObject.SetActive(true);
    }

    private void Cleanup()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                _variants.Remove(child);
                Destroy(child.gameObject);
            }
        }
        Destroy(this);
    }
}
