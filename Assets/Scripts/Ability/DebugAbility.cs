using UnityEngine;

public class DebugAbility : MonoBehaviour
{
    void Start()
    {
        print("Debug");
        Destroy(gameObject);
    }
}
