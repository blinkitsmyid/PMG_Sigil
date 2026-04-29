using System.Collections.Generic;
using UnityEngine;

public class SceneOrderManager : MonoBehaviour
{
    public static SceneOrderManager Instance;

    [Header("Сцены (индексы из Build Settings)")]
    public List<int> levelSceneIndexes = new List<int>();

    private List<int> shuffledOrder = new List<int>();
    private int currentIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GenerateRandomOrder();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GenerateRandomOrder()
    {
        shuffledOrder = new List<int>(levelSceneIndexes);

        // перемешивание (Fisher-Yates)
        for (int i = 0; i < shuffledOrder.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledOrder.Count);
            int temp = shuffledOrder[i];
            shuffledOrder[i] = shuffledOrder[randomIndex];
            shuffledOrder[randomIndex] = temp;
        }

        currentIndex = 0;

        Debug.Log("🎲 Новый порядок сцен:");
        foreach (var i in shuffledOrder)
        {
            Debug.Log(i);
        }
    }

    public int GetNextScene()
    {
        if (currentIndex >= shuffledOrder.Count)
        {
            Debug.Log("Все уровни пройдены!");
            return -1;
        }

        int sceneIndex = shuffledOrder[currentIndex];
        currentIndex++;
        return sceneIndex;
    }

    public void ResetOrder()
    {
        GenerateRandomOrder();
    }
}