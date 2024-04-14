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

    public LoseCameraController loseCam;
    public Camera cam;
    public AudioSource audioSource;
    public AudioClip movementSound;

    public Animator anim;
    public bool IsDead = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _playerMaterial = _renderer.material;
        audioSource = GetComponent<AudioSource>();

        loseCam = FindObjectOfType<LoseCameraController>();
    }

    public void Die()
    {
        if (IsDead) return;

        IsDead = true;
        agent.isStopped = true;
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        agent.acceleration = 0;
        anim.Play("Caught");

        loseCam.StartCoroutine(loseCam.LoseAnimation());
    }

    void Update()
    {
        if (IsDead) return;
        _playerMaterial.color = IsInvisible ? InvisibleColor : VisibleColor;

        if (!DisableMovement && Input.GetMouseButtonDown(0))
        {
            MoveToClickedPoint();
        }

        if (IsMoving())
        {
            anim.Play("MoveSneak");
        }
        else
        {
            anim.Play("Idle1");
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
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(movementSound);
                agent.SetDestination(hit.point);
            }
        }
    }

    void SpawnCrosshair(Vector3 position)
    {
        GameObject crosshair = Instantiate(MovementCrosshairPrefab, position + new Vector3(0, 0.1f, 0), MovementCrosshairPrefab.transform.localRotation);
    }

    public bool IsMoving()
    {
        return agent.velocity.magnitude > 1f;
    }
}
