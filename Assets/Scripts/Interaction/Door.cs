using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 TargetRotation;
    public float RotationSpeed = 1.0f;
    private bool doorOpen = false; // Start with the door closed

    private Coroutine currentCoroutine;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ToggleDoor()
    {
        doorOpen = !doorOpen;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        if (doorOpen)
        {
            currentCoroutine = StartCoroutine(OpenDoorCoroutine());
        }
        else
        {
            currentCoroutine = StartCoroutine(CloseDoorCoroutine());
        }
    }

    IEnumerator OpenDoorCoroutine()
    {
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(TargetRotation);
        float progress = 0f;
        while (progress < 1f)
        {
            progress += RotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }
    }

    IEnumerator CloseDoorCoroutine()
    {
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(Vector3.zero);
        float progress = 0f;
        while (progress < 1f)
        {
            progress += RotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }
    }
}
