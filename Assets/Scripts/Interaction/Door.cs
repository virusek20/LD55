using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 TargetRotation;
    public float RotationSpeed = 1.0f;
    public bool doorOpen = false; // Start with the door closed

    private Coroutine currentCoroutine;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void ToggleDoor()
    {
        doorOpen = !doorOpen;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        audioSource.pitch = Random.Range(0.8f, 1.2f);

        if (doorOpen)
        {
            currentCoroutine = StartCoroutine(OpenDoorCoroutine());
            audioSource.PlayOneShot(doorOpenSound);
        }
        else
        {
            currentCoroutine = StartCoroutine(CloseDoorCoroutine());
            audioSource.PlayOneShot(doorCloseSound);
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
