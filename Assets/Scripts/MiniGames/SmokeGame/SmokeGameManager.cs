using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGameManager : MonoBehaviour
{
    public static SmokeGameManager Instance;

    [Header("Все объекты, которые нужно удалить")]
    public List<GameObject> targets = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void RemoveTarget(GameObject obj)
    {
        if (targets.Contains(obj))
        {
            targets.Remove(obj);
        }

        CheckWin();
    }

    public void CheckWin()
    {
        Debug.Log("Проверка победы. Осталось: " + targets.Count);

        if (targets.Count == 0)
        {
            Debug.Log("🎉 ПОБЕДА!");
            StartCoroutine(WinCoroutine());
        }
    }

    private IEnumerator WinCoroutine()
    {
        Debug.Log("WIN!");

        yield return new WaitForSecondsRealtime(4f);
        GamesManager.Instance.CloseSmokeGame();
        GhostBehaviour.Instance.ApplyExorcism(ExorcismType.Incense);
    }
}