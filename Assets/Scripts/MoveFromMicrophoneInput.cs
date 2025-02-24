using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class MoveFromMicrophoneInput : MonoBehaviour
{
    public static MoveFromMicrophoneInput Instance;
    public AudioSource source;
    public AudioLoudnessDetection detector;
    public ParticleSystem Dust;
    public LevelManager levelManager;
    public int Score;
    public TMP_Text ScoreText;
    public Animator animator;
    public float loudnessSensibility;
    public float walkThreshold = 0.1f;
    public float jumpThreshold = 0.1f;
    public float jumpForce = 5f;
    public float moveSpeed = 2f;
    public TMP_Text LastText;

    public Slider senstivitySlider;
    public Slider walkThresholdSlider;
    public Slider jumpThreshlodSlider;

    private Rigidbody2D rb;
    private bool isMoving;

    void Awake()
    {
        // Singleton Initialization
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager=FindAnyObjectByType<LevelManager>();
        // Ensure "PerfectSensitivity" is set in PlayerPrefs before retrieving
        if (PlayerPrefs.HasKey("PerfectSensitivity"))
        {
            loudnessSensibility = PlayerPrefs.GetFloat("PerfectSensitivity");
        }
        else
        {
            loudnessSensibility = 1f; // Default value if not found
        }

        Debug.Log("Loudness Sensibility: " + loudnessSensibility);
    }

    void FixedUpdate()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        rb.linearVelocity = new Vector2(loudness * moveSpeed, rb.linearVelocity.y);

        if (loudness > jumpThreshold)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        if(rb.linearVelocityY > 0.1f)
        {
            Dust.Play();
            animator.SetInteger("anim", 2);
        }
        else if(rb.linearVelocity.x > 0.1f)
        {
            Dust.Play();
            animator.SetInteger("anim", 1);
        }
        else
        {
            animator.SetInteger("anim", 0);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.EggsCollected++;
                Score++;
                ScoreText.text = Score.ToString();
                Debug.Log("Collected");
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }
        }

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Level Completed");

            if (levelManager!= null)
            {
                levelManager.LevelComplete();
            }
            else
            {
                Debug.LogError("LevelManager instance is missing!");
            }
        }

        if (collision.CompareTag("End"))
        {
            Debug.Log("Game Over");

            if (levelManager != null)
            {
                levelManager.GameOver();
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("LevelManager instance is missing!");
            }
        }
    }
   
    public void SlidersApply()
    {
        loudnessSensibility = senstivitySlider.value;
        walkThreshold = walkThresholdSlider.value;
        jumpThreshold = jumpThreshlodSlider.value;
    }
    
}
