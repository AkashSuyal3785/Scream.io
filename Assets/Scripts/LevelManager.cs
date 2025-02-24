using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    
    public GameObject GameOverPanel;
    public GameObject LevelCompletePanel;
    public TMP_Text CollectablesVal;

    private void OnEnable()
    {
        //PlayerPrefs.GetString()
    }

    private void FixedUpdate()
    {
        CollectablesVal.text = GameManager.Instance.EggsCollected.ToString();
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }
    public void LevelComplete()
    {
        LevelCompletePanel.SetActive(true);
        GameManager.Instance.EggsCollected++;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level 2");
    }

}
