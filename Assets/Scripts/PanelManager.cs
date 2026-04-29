using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    private PlayerInputActions _playerInputActions;
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject bookPanel;
    public GameObject termometrPanel;
    public GameObject TimerUI;
  
    [Header("Timer Settings")]
    [SerializeField] private float gameTimeSeconds = 60f; 
    private float currentTime;
    private bool isTimerRunning = true;
    private Coroutine timerCoroutine;
    
    private bool isPaused = false;
    private bool isOpenBook = false;
    private bool isOpenTermometr = false;

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
       
        ResetAndStartTimer();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.UI.Pause.performed += _ => Pause();
        _playerInputActions.UI.OpenBook.performed += _ => OpenBook();
        _playerInputActions.UI.Termometr.performed += _ => OpenTermometr();
    }
    
    private void OnDisable()
    {
        _playerInputActions.Disable();
    }
    
    
    private void ResetAndStartTimer()
    {
        currentTime = gameTimeSeconds;
        isTimerRunning = true;
        
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
        
        timerCoroutine = StartCoroutine(TimerRoutine());
    }
    
    private IEnumerator TimerRoutine()
    {
        while (isTimerRunning && currentTime > 0)
        {
            
            yield return new WaitForSeconds(1f);
            
           
            if (Time.timeScale > 0)
            {
                currentTime -= 1f;
                
               
                OnTimerUpdate?.Invoke(currentTime);
                
              
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    isTimerRunning = false;
                    Lose(); 
                }
            }
        }
    }
    
  
    public System.Action<float> OnTimerUpdate;
    
    
    public float GetCurrentTime()
    {
        return currentTime;
    }
    
    
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        return $"{minutes:00}:{seconds:00}";
    }
    
    
    public void SetTimerPaused(bool paused)
    {
        // Таймер автоматически приостановится благодаря проверке Time.timeScale в корутине
        // Этот метод можно использовать для дополнительной логики
    }
    
    public void OpenBook()
    {
        if (GamesManager.Instance != null && GamesManager.Instance.IsGameActive)
            return;
        if (isOpenBook)
        {
            bookPanel.SetActive(false);
            isOpenBook = false;
        }
        else
        {
            if (isOpenTermometr)
            {
                termometrPanel.SetActive(false);
                isOpenTermometr = false;
            }
            bookPanel.SetActive(true);
            isOpenBook = true;
        }
    }
    
    public void CloseBook()
    {
        bookPanel.SetActive(false);
        isOpenBook = false;
    }
    
    public void OpenTermometr()
    {
        if (GamesManager.Instance != null && GamesManager.Instance.IsGameActive)
            return;
        if (isOpenTermometr)
        {
            termometrPanel.SetActive(false);
            isOpenTermometr = false;
            
        }
        else
        {
            if (isOpenBook)
            {
                bookPanel.SetActive(false);
                isOpenBook = false;
            }
            termometrPanel.SetActive(true);
            isOpenTermometr = true;
            
        }
    }
    
    public void Pause()
    {
        if (isPaused)
        {
            // закрываем
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
            TimerUI.SetActive(true);
        }
        else
        {
            if (isOpenTermometr)
            {
                termometrPanel.SetActive(false);
                isOpenTermometr = false;
            }
            if (isOpenBook)
            {
                bookPanel.SetActive(false);
                isOpenBook = false;
            }
            pausePanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
            TimerUI.SetActive(false);
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void Lose()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
        
        isTimerRunning = false;
        AudioManager.Instance.musicSource.Stop();
        TimerUI.SetActive(false);
        losePanel.SetActive(true);
        AudioManager.Instance.PlayLose();
        
        Time.timeScale = 0f;
        
    }

    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }
    // 🏆 WIN
    public void Win()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
        
        isTimerRunning = false;
        AudioManager.Instance.musicSource.Stop();
        TimerUI.SetActive(false);
        //winPanel.SetActive(true);
        DayManager.Instance.NextDay();
        SceneLoader.Instance.LoadNextLevel();
        AudioManager.Instance.PlayWin();
        Time.timeScale = 0f;
    }
}