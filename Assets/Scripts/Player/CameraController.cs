using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float velocityOffsetFactor = 0.1f;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 playerVelocity = (playerTransform.position - lastPlayerPosition) / Time.deltaTime;
            lastPlayerPosition = playerTransform.position;

            Vector3 desiredPosition = playerTransform.position + offset + playerVelocity * velocityOffsetFactor;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
