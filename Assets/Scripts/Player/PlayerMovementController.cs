using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool DisableMovement = false;

    public bool IsInvisible = false;
    private bool _lastInvisible = false;

    private UnityEngine.AI.NavMeshAgent agent;

    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    [SerializeField]
    private Material _playerMaterial;

    [SerializeField]
    private Material _ghostMaterial;

    public Color VisibleColor;
    public Color InvisibleColor;

    public GameObject MovementCrosshairPrefab;

    public LoseCameraController loseCam;
    public Camera cam;
    public AudioSource audioSource;
    public AudioClip movementSound;

    private AbilityInput _abilityInput;
    private float _idleTime;

    public Animator anim;
    public bool IsDead = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        loseCam = FindObjectOfType<LoseCameraController>();
        _abilityInput = FindObjectOfType<AbilityInput>();
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

        if (_lastInvisible != IsInvisible)
        {
            _renderer.material = IsInvisible ? _ghostMaterial : _playerMaterial;
            _lastInvisible = IsInvisible;
        }

        if (!DisableMovement && Input.GetMouseButtonDown(0))
        {
            MoveToClickedPoint();
        }

        _idleTime += Time.deltaTime;

        anim.SetBool("IsCasting", _abilityInput.CastMode);
        anim.SetFloat("Speed", agent.velocity.magnitude);
        anim.SetFloat("IdleTime", _idleTime);
    }

    void MoveToClickedPoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = 1 << LayerMask.NameToLayer("UI");
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                SpawnCrosshair(hit.point);
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(movementSound);
                agent.SetDestination(hit.point);

                _idleTime = 0f;
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
                
    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
