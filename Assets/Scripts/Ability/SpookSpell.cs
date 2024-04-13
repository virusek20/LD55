using UnityEngine;

public class SpookSpell : MonoBehaviour
{
    public void Spook(RaycastHit target)
    {
        Destroy(target.transform.gameObject);
        Destroy(gameObject);
    }
}
