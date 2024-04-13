using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float MaxDistance = 100f;
    public LayerMask BlockingMask;

    private RaycastHit _hit;
    private Ray _ray;

    private Interactible _lastInteractible;

    void Update()
    {
        // Camera -> Object
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit))
        {
            if (_lastInteractible != null) _lastInteractible.OnMouseExit.Invoke();
            _lastInteractible = null;
            return;
        }

        if (!_hit.transform.TryGetComponent<Interactible>(out var interactible))
        {
            if (_lastInteractible != null) _lastInteractible.OnMouseExit.Invoke();
            _lastInteractible = null;
            return;
        }

        if (_lastInteractible != interactible)
        {
            if (_lastInteractible != null) _lastInteractible.OnMouseExit.Invoke();
            interactible.OnMouseEnter.Invoke();

            _lastInteractible = interactible;
        }

        if (!Input.GetMouseButtonDown(0)) return;

        // Player -> Object
        _ray = new Ray(transform.position, _hit.transform.position - transform.position);
        if (!Physics.Raycast(_ray, out _hit, MaxDistance, BlockingMask)) return;

        // Something in the way
        if (_hit.transform != interactible.transform) return;
        interactible.OnInteract.Invoke();
    }
}
