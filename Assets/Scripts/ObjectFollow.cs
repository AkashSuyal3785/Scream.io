using UnityEngine;
using System.Collections;

public class ObjectFollow : MonoBehaviour
{
    public Transform target; // The object to follow
    public float smoothing = 5f; // Smoothing speed
    public Vector3 offset; // Initial offset from the target

    void Start()
    {
        // Set initial position at x = 20
        transform.position = new Vector3(20f, target.position.y + offset.y, -10f);

        // Start the animation moving the camera to x = 0
        StartCoroutine(AnimateCameraToZeroX());
    }

    void FixedUpdate()
    {

        // Create a position the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        //Follow only in Y Position..
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, 0f, -10f), new Vector3(targetCamPos.x, 0f, -10f), smoothing * Time.fixedDeltaTime);
    }

    IEnumerator AnimateCameraToZeroX()
    {
        float duration = 2f; // Duration of the animation
        float elapsed = 0f;
        Vector3 startPos = new Vector3(0, 20f, 0);
        Vector3 endPos = new Vector3(0, 0, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        // Ensure it exactly reaches the target position
        transform.position = endPos;
    }
}
