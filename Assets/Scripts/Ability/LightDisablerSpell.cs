using System.Collections;
using UnityEngine;

public class LightDisablerSpell : MonoBehaviour
{
    [Range(1, 100)]
    public float Duration;

    private TogglebleLight[] _lights;

    void Start()
    {
        _lights = FindObjectsOfType<TogglebleLight>();
        StartCoroutine(CastCoroutine());
    }

    private IEnumerator CastCoroutine()
    {
        foreach (var light in _lights) light.DisableLight();
        yield return new WaitForSeconds(Duration);
        foreach (var light in _lights) light.EnableLight();
    }
}