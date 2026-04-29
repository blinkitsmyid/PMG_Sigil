using System.Collections;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void WinLevel()
    {
        Debug.Log("Победа уровня");
        PlayerController.Instance.TeleportToEndSpawnPoint();
        PlayerController.Instance.SetMovementEnabled(false); 
        StartCoroutine(WinCoroutine());
    }
    private static IEnumerator WinCoroutine()
    {
        Debug.Log("Проигрышная сцена");
        yield return new WaitForSecondsRealtime(4f);
        PanelManager.Instance.Win();
    }

}