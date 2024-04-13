using UnityEngine;

public class DebugAbility : MonoBehaviour
{
    void Start()
    {
        var go = GameObject.Find("Cube (1)");
        Destroy(go);
    }
}
