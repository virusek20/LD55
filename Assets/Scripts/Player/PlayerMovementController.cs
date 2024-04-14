using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool DisableMovement = false;
    public bool IsInvisible = false;

    private UnityEngine.AI.NavMeshAgent agent;

    [SerializeField]
    private MeshRenderer _renderer;
    private Material _playerMaterial;

    public Color VisibleColor;
    public Color InvisibleColor;

    public GameObject MovementCrosshairPrefab;

    public Camera cam;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _playerMaterial = _renderer.material;
    }

    void Update()
    {
        _playerMaterial.color = IsInvisible ? InvisibleColor : VisibleColor;

        if (!DisableMovement && Input.GetMouseButtonDown(0))
        {
            MoveToClickedPoint();
        }
    }

    void MoveToClickedPoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = 1 << LayerMask.NameToLayer("UI");
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                SpawnCrosshair(hit.point);
                agent.SetDestination(hit.point);
            }
        }
    }

    void SpawnCrosshair(Vector3 position)
    {
        GameObject crosshair = Instantiate(MovementCrosshairPrefab, position + new Vector3(0, 0.1f, 0), MovementCrosshairPrefab.transform.localRotation);
    }
}
