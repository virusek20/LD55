using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 TargetRotation;
    public float RotationSpeed = 1.0f;
    public bool doorOpen = false; // Start with the door closed
    public bool Locked = false;

    private Coroutine currentCoroutine;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip doorLockedSound;

    AudioSource audioSource;

    public GameObject LockIcon;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void ToggleDoor()
    {
        audioSource.Stop();

        if (!Locked)
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
        else
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(doorLockedSound);
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

    public void ShowLockIcon()
    {
        if (Locked)
        LockIcon.SetActive(true);
    }

    public void HideLockIcon()
    {
        if (Locked)
        LockIcon.SetActive(false);
    }
}
