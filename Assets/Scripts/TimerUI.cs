using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // или TextMeshProUGUI
    
    private void Start()
    {
        if (PanelManager.Instance != null)
        {
            PanelManager.Instance.OnTimerUpdate += UpdateTimerDisplay;
            UpdateTimerDisplay(PanelManager.Instance.GetCurrentTime());
        }
    }
    
    private void UpdateTimerDisplay(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
            
        }
    }
    
    private void OnDestroy()
    {
        if (PanelManager.Instance != null)
            PanelManager.Instance.OnTimerUpdate -= UpdateTimerDisplay;
    }
}