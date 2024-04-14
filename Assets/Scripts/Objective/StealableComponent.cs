using UnityEngine;

public class StealableComponent : MonoBehaviour
{
    public string ObjectiveId;

    public bool IsLocked;
    public GameObject LockIcon;


    private ObjectiveManager manager;

    void Start()
    {
        manager = FindObjectOfType<ObjectiveManager>();
    }

    public void Steal()
    {
        if (IsLocked) return;

        manager.AddCounter(ObjectiveId);
        Destroy(gameObject);
    }

    public void ShowLockIcon()
    {
        if (IsLocked) LockIcon.SetActive(true);
    }

    public void HideLockIcon()
    {
        if (IsLocked) LockIcon.SetActive(false);
    }
}
