using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public UnityEvent OnInteract;
    public UnityEvent OnMouseEnter;
    public UnityEvent OnMouseExit;

    public bool CanRemoteInteract = true;

    void Start()
    {
        if (!TryGetComponent<Outline>(out var outline)) return;

        OnMouseEnter.AddListener(() => outline.enabled = true);
        OnMouseExit.AddListener(() => outline.enabled = false);
        outline.enabled = false;
    }
}
