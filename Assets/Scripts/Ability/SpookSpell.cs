using System.Collections;
using UnityEngine;

public class SpookSpell : MonoBehaviour
{
    [Range(1, 100)]
    public float Duration = 10;

    public void Spook(RaycastHit target)
    {
        if (!target.transform.TryGetComponent<AIAgent>(out var enemy))
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(SpookCoroutine(enemy));
    }

    private IEnumerator SpookCoroutine(AIAgent enemy)
    {
        enemy.SetState(AIState.Spooked);
        yield return new WaitForSeconds(Duration);
        enemy.SetState(AIState.Patrolling);
        Destroy(gameObject);
    }
}
