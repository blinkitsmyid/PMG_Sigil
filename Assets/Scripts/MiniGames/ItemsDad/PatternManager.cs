using System.Collections.Generic;
using UnityEngine;

namespace DragDropGame
{
    public class PatternManager : MonoBehaviour
    {
        public static PatternManager Instance { get; private set; }

        [Header("Все доступные паттерны")]
        public List<PatternConfig> allPatterns = new List<PatternConfig>();

        public PatternConfig currentPattern { get; private set; }
        public int itemsPlacedCount = 0;
        private int totalItems = 5;

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
            }
        }

        private void Start()
        {
            SelectRandomPattern();
        }

        public void SelectRandomPattern()
        {
            if (allPatterns == null || allPatterns.Count == 0)
            {
                Debug.LogError("Нет ни одного паттерна!");
                return;
            }

            int randomIndex = Random.Range(0, allPatterns.Count);
            currentPattern = allPatterns[randomIndex];
            Debug.Log($"🎲 Выбран паттерн: {currentPattern.patternName} (ID: {currentPattern.patternID})");
            
            // Выводим все назначения паттерна
            foreach (var assignment in currentPattern.assignments)
            {
                Debug.Log($"   Паттерн: слот {assignment.slotIndex} → {assignment.itemType}");
            }
        }

        public void OnItemPlaced()
        {
            itemsPlacedCount++;
            Debug.Log($"📦 Предметов расставлено: {itemsPlacedCount}/{totalItems}");

            if (itemsPlacedCount >= totalItems)
            {
                if (GameController.Instance != null)
                    GameController.Instance.CheckWin();
            }
        }

        public void ResetGame()
        {
            itemsPlacedCount = 0;
            SelectRandomPattern();
        }
    }
}