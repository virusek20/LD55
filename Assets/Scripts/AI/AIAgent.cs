using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Patrolling,
    Chasing,
    Searching,
    Spooked,
    Lure,
    LostPlayer,
    Shoot
}

public class AIAgent : MonoBehaviour
{
    public List<Transform> PatrolPoints;
    public PlayerMovementController player;
    public Transform playerTransform;
    public float visionAngle = 45f;
    public float visionDistance = 10f;
    public float searchDuration = 3f;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private bool movingForward = true;
    private AIState currentState = AIState.Patrolling;
    public Vector3 lastKnownPlayerPosition;
    private float searchTimer = 0f;
    private float searchLookTimer = 0f;
    private float spookTimer = 0f;
    private float lockOnTimer = 0f;
    public float LockOnDuration = 3.0f;

    public Animator anim;

    public float RadioDelay = 30;
    private float _radioDelayTimer = 0;
    public AudioSource AudioSource;
    public AudioClip Huh;
    public AudioClip Radio;
    public AudioClip MovementSound;

    [SerializeField]
    private float _lureIdleLength = 5f;
    private float _lureDelay;

    [SerializeField]
    private float _lostDelayLength = 3.5f;
    private float _lostDelay;

    private float _stepDelay;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();

        SetDestination();
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Patrolling:
                agent.speed = 1.7f;
                PatrollingUpdate();
                break;
            case AIState.Chasing:
                agent.speed = 4.5f;
                ChasingUpdate();
                break;
            case AIState.Searching:
                SearchingUpdate();
                break;
            case AIState.Lure:
                LureUpdate();
                break;
            case AIState.LostPlayer:
                LostPlayerUpdate();
                break;
            case AIState.Spooked:
                SpookedUpdate();
                break;
        }

        anim.SetFloat("Velocity", agent.velocity.magnitude);
        anim.SetBool("Spook", currentState == AIState.Spooked);
        anim.SetBool("SeesPlayer", currentState == AIState.Chasing);
        anim.SetBool("HasTarget", currentState == AIState.Searching || currentState == AIState.Chasing || currentState == AIState.Lure);

        if (agent.velocity.magnitude > 0.5f && _stepDelay < 0f)
        {
            AudioSource.pitch = Random.Range(0.8f, 1.2f);
            AudioSource.PlayOneShot(MovementSound);
            _stepDelay = 1.5f;
        }
        else
        {
            _stepDelay -= Time.deltaTime * agent.velocity.magnitude;
        }
    }

    public void SetState(AIState state)
    {
        currentState = state;

        if (state == AIState.Spooked)
        {
            spookTimer = 0f;
            anim.SetTrigger("CanSpook");
        }

        anim.ResetTrigger("CanSpook");
    }

    public void Lure(Vector3 target)
    {
        currentState = AIState.Lure;
        AudioSource.PlayOneShot(Huh);
        agent.SetDestination(target);
    }

    void PatrollingUpdate()
    {
        if (Random.value > 0.999f && _radioDelayTimer <= 0f)
        {
            AudioSource.PlayOneShot(Radio);
            _radioDelayTimer = RadioDelay;
        }
        else _radioDelayTimer -= Time.deltaTime;

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToNextPatrolPoint();
        }

        if (CanSeePlayer())
        {
            currentState = AIState.Chasing;
            AudioSource.PlayOneShot(Huh);
            lastKnownPlayerPosition = playerTransform.position;
        }
    }

    void ChasingUpdate()
    {
        agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(transform.position, playerTransform.position) < 1.5f)
        {
            player.Die();
            anim.SetBool("Shoot", true);
            currentState = AIState.Shoot;
        }

        lockOnTimer += Time.deltaTime;
        if (lockOnTimer >= LockOnDuration)
        {
            if (!CanSeePlayer())
            {
                currentState = AIState.Searching;
                searchTimer = 0f;
            }
            lockOnTimer = 0f;
        }
    }

    void LostPlayerUpdate()
    {
        _lostDelay += Time.deltaTime;
        if (_lostDelay >= _lostDelayLength)
        {
            currentState = AIState.Patrolling;
            _lureDelay = 0;
        }
    }

    void LureUpdate()
    {
        if (Vector3.Distance(agent.destination, transform.position) < 2f)
        {
            _lureDelay += Time.deltaTime;
            if (_lureDelay >= _lureIdleLength)
            {
                currentState = AIState.LostPlayer;
                AudioSource.PlayOneShot(Huh);
                _lureDelay = 0;
            }
        }
    }

    void SearchingUpdate()
    {
        searchTimer += Time.deltaTime;
        searchLookTimer += Time.deltaTime;

        if (searchTimer >= searchDuration)
        {
            currentState = AIState.Patrolling;
            SetDestination();
        }

        if (searchLookTimer >= 1.0)
        {
            var doors = GameObject.FindGameObjectsWithTag("Door");
            foreach (var door in doors)
            {
                var doorScript = door.GetComponent<Door>();
                if (Vector3.Distance(door.transform.position, lastKnownPlayerPosition) < 5f)
                {
                    if (doorScript.doorOpen) continue;
                    if (doorScript.Locked) continue;
                    doorScript.ToggleDoor();
                }
            }

            searchLookTimer = 0f;
            var sphere = Random.insideUnitSphere * 5f;
            sphere.y = 0;

            var randomPosition = transform.position + sphere;
            agent.SetDestination(randomPosition);
        }
    }

    void SpookedUpdate()
    {
        spookTimer += Time.deltaTime;

        if (spookTimer >= 1.0)
        {
            spookTimer = 0f;
            var sphere = Random.insideUnitSphere * 5f;
            sphere.y = 0;

            var randomPosition = transform.position + sphere;
            agent.SetDestination(randomPosition);
        }
    }

    void SetDestination()
    {
        agent.SetDestination(PatrolPoints[currentPatrolIndex].position);
    }

    void MoveToNextPatrolPoint()
    {
        if (movingForward)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= PatrolPoints.Count)
            {
                currentPatrolIndex = PatrolPoints.Count - 2;
                movingForward = false;
            }
        }
        else
        {
            currentPatrolIndex--;
            if (currentPatrolIndex < 0)
            {
                currentPatrolIndex = 1;
                movingForward = true;
            }
        }

        SetDestination();
    }

    bool CanSeePlayer()
    {
        if (player.IsInvisible) return false;

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < visionAngle / 2f && directionToPlayer.magnitude < visionDistance)
        {
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer.normalized);
            float maxDotProduct = Mathf.Cos(Mathf.Deg2Rad * (visionAngle / 2f));

            if (dotProduct >= maxDotProduct)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
                {
                    if (hit.transform == playerTransform)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

}
