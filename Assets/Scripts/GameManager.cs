using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int EggsCollected;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    [ContextMenu("Load")]
    public void StartGame()
    {
        EggsCollected = PlayerPrefs.GetInt("EggsCollected", 0);
        SceneManager.LoadScene("Level 1");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void quit()
    {
        Application.Quit();
    }

    public void LevelCompleted()
    {
        //Lvl Completed
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("EggsCollected",EggsCollected);
        PlayerPrefs.Save();
    }
}
