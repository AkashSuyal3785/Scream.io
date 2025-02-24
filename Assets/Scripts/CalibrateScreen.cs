using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalibrateScreen : MonoBehaviour
{
    public AudioLoudnessDetection LoudnessDetector;
    public TMP_Text LoudnessText;
    public TMP_Text Status;

    public float Sensitivity = 1.0f; // Default value

    public Slider SensitivitySlider; // Fixed spelling

    public GameManager GameManager;

    [Header("Loudness Thresholds")]
    public float badLoudness = 0.7f;
    public float averageLoudness = 0.4f;
    public float goodLoudness = 0.2f;

    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
        if (!LoudnessDetector)
        {
            LoudnessDetector = FindAnyObjectByType<AudioLoudnessDetection>();
        }

        // Load saved sensitivity value
        LoadBestSensitivity();

        // Ensure slider isn't null and add listener
        if (SensitivitySlider)
        {
            SensitivitySlider.onValueChanged.AddListener(CalibrateSensitivity);
            SensitivitySlider.value = Sensitivity; // Apply saved value
        }
    }

    void Update()
    {
        if (LoudnessDetector == null) return; // Prevent null reference error

        float loudness = LoudnessDetector.GetLoudnessFromMicrophone() * Sensitivity;
        LoudnessText.text = loudness.ToString("F2");  // Format to 2 decimal places

        CheckLoudness(loudness);
    }

    private void CheckLoudness(float loudness)
    {
        if (loudness > badLoudness)
        {
            Debug.Log("Loudness Level: BAD");
            Status.text = "BAD";
            Status.color = Color.red;
        }
        else if (loudness > averageLoudness)
        {
            Debug.Log("Loudness Level: AVERAGE");
            Status.text = "AVERAGE";
            Status.color = Color.yellow;
            
        }
        else if (loudness > goodLoudness)
        {
            Debug.Log("Loudness Level: GOOD");
            Status.text = "GOOD";
            Status.color = Color.green;
            
        }
        else
        {
            Debug.Log("Loudness Level: TOO QUIET");
            Status.text = "TOO QUIET";
            Status.color = Color.black;
        }
    }

    private void CalibrateSensitivity(float value) // Pass value directly
    {
        Sensitivity = value;
        Debug.Log("Sensitivity updated: " + Sensitivity);
    }

    public void SaveBestSensitivity() // Fixed spelling
    {
        PlayerPrefs.SetFloat("PerfectSensitivity", Sensitivity);
        PlayerPrefs.Save();
        Debug.Log("Sensitivity saved: " + Sensitivity);
    }

    private void LoadBestSensitivity()
    {
        if (PlayerPrefs.HasKey("PerfectSensitivity"))
        {
            Sensitivity = PlayerPrefs.GetFloat("PerfectSensitivity");
            Debug.Log("Loaded saved sensitivity: " + Sensitivity);
        }
    }

    public void StartGame()
    {
        //EggsCollected = PlayerPrefs.GetInt("EggsCollected", 0);
        SceneManager.LoadScene("Level 1");
    }
}
