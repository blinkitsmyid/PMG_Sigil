using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        DayManager.Instance.ResetDays();
        SceneOrderManager.Instance.ResetOrder();
        LoadNextLevel();
    }
    
    public void LoadLevel(int levelIndex)
    {
       
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1f;
    }
    public void LoadNextLevel()
    {
        AudioManager.Instance.PlayClick();

        int nextScene = SceneOrderManager.Instance.GetNextScene();

        if (nextScene == -1)
        {
            SceneManager.LoadScene("00_Menu");
            return;
        }

        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene("00_Menu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
    
}