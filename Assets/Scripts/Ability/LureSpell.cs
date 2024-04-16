using System.Linq;
using UnityEngine;

public class LureSpell : MonoBehaviour
{
    [Range(1, 100)]
    public float Range;

    public void Lure(RaycastHit hit)
    {
        var guards = FindObjectsOfType<AIAgent>()
            .Where(g => Vector3.Distance(hit.point, g.transform.position) <= Range);

        foreach (var guard in guards)
        {
            guard.Lure(hit.point);
        }

        Destroy(gameObject);
    }
}