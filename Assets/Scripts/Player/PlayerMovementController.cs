using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickedPoint();
        }
    }

    void MoveToClickedPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
