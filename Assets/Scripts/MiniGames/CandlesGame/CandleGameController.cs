using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public List<Candle> candles;
    public List<ClickZone> clickZones;

    [Header("UI")]
    public GameObject winPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeGame();
    }
    
    public void InitializeGame()
    {
        PatternManager.Instance.SelectRandomPattern();

        foreach (var candle in candles)
        {
            candle.ResetCandle();
        }
    }

  
    public void OnNumberPressed(int number)
    {
        if (PatternManager.Instance.CheckNextNumber(number))
        {
            PatternManager.Instance.OnCorrectPress(number);
        }
        else
        {
            Debug.Log("Ошибка! Сброс.");

            PatternManager.Instance.ResetProgress();
        }
    }

  
    public void LightCandle(int index)
    {
        Candle candle = candles.Find(c => c.candleIndex == index);

        if (candle != null)
        {
            Debug.Log("Зажигаем свечу: " + index);
            candle.Light();
        }
        else
        {
            Debug.LogError("Свеча не найдена: " + index);
        }
    }
    // Потушить все свечи
    public void ExtinguishAllCandles()
    {
        foreach (var candle in candles)
        {
            candle.Extinguish();
        }
    }


    public void CheckWin()
    {
        Debug.Log("ПОБЕДА!");
        StartCoroutine(WinCoroutine());

    }
    private IEnumerator WinCoroutine()
    {
        Debug.Log("WIN!");
        
        //yield return new WaitForSecondsRealtime(0.7f);
        //AudioManager.Instance.PlayWinBowlsSound();
        yield return new WaitForSecondsRealtime(3f);
        GamesManager.Instance.CloseCandlesGame();
        GhostBehaviour.Instance.ApplyExorcism(ExorcismType.DrawPentagram);
    }
  
    public void ResetGame()
    {
        InitializeGame();
    }
}