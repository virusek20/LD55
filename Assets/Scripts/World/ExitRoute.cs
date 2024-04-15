using System.Collections;
using UnityEngine;

public class ExitRoute : MonoBehaviour
{
    private ObjectiveManager _objective;

    [SerializeField]
    private GameObject _reminder;

    [SerializeField]
    private GameObject _summary;

    void Start()
    {
        _objective = FindObjectOfType<ObjectiveManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) TryExit();
    }

    public void TryExit()
    {
        if (!_objective.CanLeave())
        {
            StartCoroutine(NoLeaveCoroutine());
            return;
        }

        _summary.SetActive(true);
    }

    private IEnumerator NoLeaveCoroutine()
    {
        _reminder.SetActive(true);
        yield return new WaitForSeconds(4f);
        _reminder.SetActive(false);
    }
}
