using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Patrolling,
    Chasing,
    Searching,
    Spooked
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }


    void Update()
    {
        switch (currentState)
        {
            case AIState.Patrolling:
                anim.Play("MovePatrol");
                PatrollingUpdate();
                break;
            case AIState.Chasing:
                anim.Play("MoveSuspicious");
                ChasingUpdate();
                break;
            case AIState.Searching:
                SearchingUpdate();
                break;
            case AIState.Spooked:
                SpookedUpdate();
                break;
        }
    }

    public void SetState(AIState state)
    {
        currentState = state;
    }

    void PatrollingUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToNextPatrolPoint();
        }

        if (CanSeePlayer())
        {
            currentState = AIState.Chasing;
            lastKnownPlayerPosition = playerTransform.position;
        }
    }

    void ChasingUpdate()
    {
        agent.SetDestination(playerTransform.position);

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
