using UnityEngine;
using System.Collections.Generic;

public class PageSwitcher : MonoBehaviour
{
    [Header("Страницы")]
    public List<GameObject> pages;

    [Header("Настройки")]
    [Tooltip("С какой страницы начать (0-4)")]
    public int startIndex = 2;

    private int currentIndex = 0;

    private void Start()
    {
        if (pages == null || pages.Count == 0)
        {
            Debug.LogError("Список страниц пуст!");
            return;
        }

        currentIndex = Mathf.Clamp(startIndex, 0, pages.Count - 1);
        UpdatePages();
    }

    // ➡️ ВПЕРЁД
    public void NextPage()
    {
        currentIndex++;

        if (currentIndex >= pages.Count)
            currentIndex = 0; // зацикливание (можно убрать если не нужно)
        AudioManager.Instance.PlayPageSound();
        UpdatePages();
    }

    // ⬅️ НАЗАД
    public void PreviousPage()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = pages.Count - 1; // зацикливание
        AudioManager.Instance.PlayPageSound();
        UpdatePages();
    }

    // 🔁 ОБНОВЛЕНИЕ
    private void UpdatePages()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i] != null)
                pages[i].SetActive(i == currentIndex);
        }

        Debug.Log($"Текущая страница: {currentIndex}");
    }

    // 👉 можно вызвать извне (например от паттерна)
    public void SetPage(int index)
    {
        currentIndex = Mathf.Clamp(index, 0, pages.Count - 1);
        UpdatePages();
    }
}