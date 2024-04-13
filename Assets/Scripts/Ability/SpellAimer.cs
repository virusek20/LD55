using UnityEngine;
using UnityEngine.Events;

public class SpellAimer : MonoBehaviour
{
    public PlayerMovementController MovementController;
    public UnityEvent<RaycastHit> OnCast;
    public GameObject MarkerPrefab;
    public LayerMask TargetMask;
    public bool NeedsLineOfSight;

    private GameObject Marker;

    void Start()
    {
        MovementController = FindObjectOfType<PlayerMovementController>();
        Marker = Instantiate(MarkerPrefab, transform);
        MovementController.DisableMovement = true;
    }

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;

        var target = hit.point;
        target.y = 0.01f;
        Marker.transform.position = target;

        if (!Input.GetMouseButtonDown(0)) return;

        if (!NeedsLineOfSight)
        {
            OnCast.Invoke(hit);
            MovementController.DisableMovement = false;
            Destroy(Marker);
            Destroy(this);
        }

        ray = new Ray(transform.position, hit.point - transform.position);
        if (!Physics.Raycast(ray, out hit, TargetMask)) return;

        target = hit.point;
        target.y = 1f;
        OnCast.Invoke(hit);
        MovementController.DisableMovement = false;

        Destroy(Marker);
        Destroy(this);
    }
}
