using UnityEngine;

public class MainMenuAesthetics : MonoBehaviour
{
    public float speed = 2f;

    private void FixedUpdate()
    {
        gameObject.transform.position += Vector3.right * speed * Time.fixedDeltaTime;
    }
}
