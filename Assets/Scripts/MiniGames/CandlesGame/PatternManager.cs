using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager Instance;

    public List<PatternConfig> allPatterns;

    public PatternConfig currentPattern;

    public int currentStep = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Выбор случайного паттерна
    public void SelectRandomPattern()
    {
        currentPattern = allPatterns[Random.Range(0, allPatterns.Count)];
        currentStep = 0;

        Debug.Log("Выбран паттерн: " + currentPattern.patternSequence);
    }

    // Проверка следующего числа
    public bool CheckNextNumber(int pressedNumber)
    {
        int expectedNumber = currentPattern.patternSequence[currentStep] - '0';

        return pressedNumber == expectedNumber;
    }

    // При правильном нажатии
    public void OnCorrectPress(int pressedNumber)
    {
        currentStep++;

        AudioManager.Instance.PlayCandleOn_Sound();
        GameController.Instance.LightCandle(pressedNumber);

        // Проверка победы
        if (currentStep >= currentPattern.patternSequence.Length)
        {
            GameController.Instance.CheckWin();
        }
    }

    // Сброс прогресса
    public void ResetProgress()
    {
        currentStep = 0;
        AudioManager.Instance.PlayCandleOff_Sound();
        GameController.Instance.ExtinguishAllCandles();
    }
}