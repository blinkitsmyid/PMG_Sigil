using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DragDropGame
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [Header("Ссылки на объекты на сцене")]
        public Transform itemsParent;
        public Transform slotsParent;

        [Header("UI")]
        public GameObject winPanel;

        [Header("Настройки позиций предметов")]
        public Vector2 startPosition = new Vector2(-400, 0);
        public float spacing = 150f;
        public bool randomizeItemsOrder = true;

        private List<DraggableItem> allItems = new List<DraggableItem>();
        private List<DropSlot> allSlots = new List<DropSlot>();
        [Header("Панели паттернов")]
        public List<GameObject> patternPanels; // 5 панелей
        private bool _winTriggered = false;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Invoke(nameof(DelayedInitialize), 0.1f);
            
        }

        private void DelayedInitialize()
        {
            InitializeGame();
        }

        public void InitializeGame()
        {
            FindAllItemsAndSlots();
            _winTriggered = false;
            if (PatternManager.Instance == null || PatternManager.Instance.currentPattern == null)
            {
                Debug.LogError("Паттерн не найден!");
                return;
            }

            PatternConfig pattern = PatternManager.Instance.currentPattern;

            // 🔥 НОВОЕ
            int panelIndex = pattern.patternID;
            ActivatePatternPanel(panelIndex);

            ResetAllSlots();
            ResetAllItems();

            ApplyPatternToSlots(pattern);
            ArrangeItemsByPositions(pattern);

            PatternManager.Instance.itemsPlacedCount = 0;

            if (winPanel != null)
                winPanel.SetActive(false);
        }

        private void FindAllItemsAndSlots()
        {
            GameObject activePanel = patternPanels[PatternManager.Instance.currentPattern.patternID];

            allSlots = activePanel.GetComponentsInChildren<DropSlot>(true).ToList();
        }
  
        private void ResetAllItems()
        {
            foreach (var item in allItems)
                item.ResetItem();
        }

        private void ResetAllSlots()
        {
            foreach (var slot in allSlots)
                slot.ResetSlot();
        }

        private void ApplyPatternToSlots(PatternConfig pattern)
        {
            Debug.Log($"Применяем паттерн: {pattern.patternName}");

            foreach (var slot in allSlots)
                slot.requiredType = ItemType.None;

            foreach (var assignment in pattern.assignments)
            {
                if (assignment.slotIndex >= 0 && assignment.slotIndex < allSlots.Count)
                {
                    DropSlot targetSlot = allSlots[assignment.slotIndex];

                    targetSlot.requiredType = assignment.itemType;

                    // ✅ устанавливаем спрайт силуэта
                    targetSlot.SetSilhouetteSprite(assignment.slotSprite);

                    Debug.Log($"Слот {assignment.slotIndex}: тип={assignment.itemType}");
                }
                else
                {
                    Debug.LogWarning($"Неверный индекс слота: {assignment.slotIndex}");
                }
            }
        }

        private void ArrangeItemsByPositions(PatternConfig pattern)
        {
            if (allItems.Count == 0) return;

            List<DraggableItem> sortedItems = GetItemsSortedByPattern(pattern);

            if (randomizeItemsOrder)
                sortedItems = ShuffleList(sortedItems);

            for (int i = 0; i < sortedItems.Count; i++)
            {
                DraggableItem item = sortedItems[i];
                RectTransform itemRect = item.GetComponent<RectTransform>();

                item.transform.SetParent(itemsParent);

                float xPos = startPosition.x + (i * spacing);
                float yPos = startPosition.y;

                itemRect.anchoredPosition = new Vector2(xPos, yPos);

                itemRect.anchorMin = new Vector2(0.5f, 0.5f);
                itemRect.anchorMax = new Vector2(0.5f, 0.5f);
                itemRect.pivot = new Vector2(0.5f, 0.5f);

                item.SetStartPosition(itemRect.anchoredPosition);
            }
        }

        private List<DraggableItem> GetItemsSortedByPattern(PatternConfig pattern)
        {
            Dictionary<ItemType, List<DraggableItem>> itemsByType = new Dictionary<ItemType, List<DraggableItem>>();

            foreach (var item in allItems)
            {
                if (!itemsByType.ContainsKey(item.itemType))
                    itemsByType[item.itemType] = new List<DraggableItem>();

                itemsByType[item.itemType].Add(item);
            }

            List<DraggableItem> sorted = new List<DraggableItem>();

            foreach (var assignment in pattern.assignments)
            {
                if (itemsByType.ContainsKey(assignment.itemType) && itemsByType[assignment.itemType].Count > 0)
                {
                    sorted.Add(itemsByType[assignment.itemType][0]);
                    itemsByType[assignment.itemType].RemoveAt(0);
                }
            }

            foreach (var remaining in itemsByType.Values)
                sorted.AddRange(remaining);

            return sorted;
        }

        private List<DraggableItem> ShuffleList(List<DraggableItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DraggableItem temp = list[i];
                int randomIndex = Random.Range(i, list.Count);

                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
            return list;
        }

        public void CheckWin()
        {
            Debug.Log("CHECK WIN CALLED");

            int i = 0;
            foreach (var slot in allSlots)
            {
                Debug.Log($"[{i}] {slot.name} filled = {slot.isFilled} | required = {slot.requiredType}");
                i++;
            }

            bool allFilled = allSlots.TrueForAll(slot => slot.isFilled);

            Debug.Log("ALL FILLED RESULT: " + allFilled);

            if (allFilled)
            {
                Debug.Log(">>> STARTING WIN COROUTINE");
                StartCoroutine(WinCoroutine());
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator WinCoroutine()
        {
            Debug.Log("ПОБЕДА СТАРТ");

            yield return new WaitForSecondsRealtime(1f);

            Debug.Log("ПОБЕДА ПОСЛЕ ОЖИДАНИЯ");

            GamesManager.Instance.CloseItemsGame();
            GhostBehaviour.Instance.ApplyExorcism(ExorcismType.Offering);
        }
        private void ActivatePatternPanel(int patternIndex)
        {
            if (patternPanels == null || patternPanels.Count == 0)
            {
                Debug.LogWarning("Панели не назначены!");
                return;
            }

            for (int i = 0; i < patternPanels.Count; i++)
            {
                patternPanels[i].SetActive(i == patternIndex);
            }

            Debug.Log($"Активирована панель: {patternIndex}");
        }
        public void RestartGame()
        {
            Time.timeScale = 1f;

            if (PatternManager.Instance != null)
                PatternManager.Instance.ResetGame();

            InitializeGame();
        }
    }
}