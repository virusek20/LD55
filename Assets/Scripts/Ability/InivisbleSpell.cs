using System.Collections;
using UnityEngine;

class InvisibleSpell : MonoBehaviour
{
    [Range(1, 100)]
    public int Duration = 5;

    IEnumerator Start()
    {
        var controller = FindObjectOfType<PlayerMovementController>();

        controller.IsInvisible = true;
        yield return new WaitForSeconds(Duration);
        controller.IsInvisible = false;

        Destroy(gameObject);
    }
}