using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // First point
    public Transform pointB;  // Second point
    public float speed = 2f;  // Movement speed
    public float pauseTime = 1f; // Pause time at each point

    private Vector3 target;
    private bool isWaiting = false;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        if (!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                StartCoroutine(PauseBeforeMoving());
            }
        }
    }

    private System.Collections.IEnumerator PauseBeforeMoving()
    {
        isWaiting = true;
        yield return new WaitForSeconds(pauseTime);
        target = (target == pointA.position) ? pointB.position : pointA.position;
        isWaiting = false;
    }
}
