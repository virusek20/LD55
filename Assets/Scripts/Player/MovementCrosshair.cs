using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCrosshair : MonoBehaviour
{
    public float easeInTime = 0.5f; // Time taken to ease in
    public float easeOutTime = 0.5f; // Time taken to ease out after reaching zero scale
    public float idleTime = 1.0f; // Time spent at zero scale before destroying

    private float elapsedTime = 0f;
    public Vector3 scale;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < easeInTime) // Ease in
        {
            float t = Mathf.Clamp01(elapsedTime / easeInTime);
            float easeT = Ease.QuadIn(t);
            transform.localScale = Vector3.Lerp(Vector3.zero, scale, easeT);
        }
        else if (elapsedTime >= easeInTime && elapsedTime < easeInTime + idleTime) // Idle at full scale
        {
            transform.localScale = scale;
        }
        else // Ease out and destroy
        {
            float t = Mathf.Clamp01((elapsedTime - easeInTime - idleTime) / easeOutTime);
            float easeT = Ease.QuadOut(t);
            transform.localScale = Vector3.Lerp(scale, Vector3.zero, easeT);

            if (elapsedTime >= easeInTime + idleTime + easeOutTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
