using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [Header("UI")]
    public GameObject dayPanel;   // панель с текстом// "День X"
    public TextMeshProUGUI dayText;
    [Header("Настройки")]
    public float showDuration = 2f;

    private int currentDay = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // можно исключить меню
        if (scene.name == "00_Menu")
            return;

        StartCoroutine(ShowDayRoutine());
    }

    private IEnumerator ShowDayRoutine()
    {
        // стоп игры
        Time.timeScale = 0f;

        dayPanel.SetActive(true);
        dayText.text = "День " + currentDay;

        yield return new WaitForSecondsRealtime(showDuration);

        dayPanel.SetActive(false);

        // запуск игры
        Time.timeScale = 1f;
    }

    public void NextDay()
    {
        currentDay++;
    }

    public void ResetDays()
    {
        currentDay = 1;
    }
}