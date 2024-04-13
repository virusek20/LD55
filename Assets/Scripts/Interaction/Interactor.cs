using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float MaxDistance = 100f;
    public LayerMask BlockingMask;

    private RaycastHit _hit;
    private Ray _ray;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        // Camera -> Object
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit)) return;

        if (!_hit.transform.TryGetComponent<Interactible>(out var interactible)) return;

        // Player -> Object
        _ray = new Ray(transform.position, _hit.transform.position - transform.position);
        if (!Physics.Raycast(_ray, out _hit, MaxDistance, BlockingMask)) return;

        // Something in the way
        if (_hit.transform != interactible.transform) return;
        interactible.OnInteract.Invoke();
    }
}
