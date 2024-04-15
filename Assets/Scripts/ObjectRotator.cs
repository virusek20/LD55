using System.Collections;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public float Speed;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        Destroy(transform.parent.gameObject);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, Speed * Time.deltaTime);
    }
}
