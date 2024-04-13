using System;
using UnityEngine;

public class TogglebleLight : MonoBehaviour
{
    private Light _light;
    private Collider _collider;

    void Start()
    {
        _light = GetComponent<Light>();
        _collider = GetComponent<Collider>();

        if (_light == null || _collider == null) throw new Exception("Expected light and collider");
    }

    public void DisableLight()
    {
        _light.enabled = false;
        _collider.enabled = false;
    }

    public void EnableLight()
    {
        _light.enabled = true;
        _collider.enabled = true;
    }
}
