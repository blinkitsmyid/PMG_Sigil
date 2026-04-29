using System.Collections;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    public static LoseManager Instance;
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
    public void LoseLevel()
    {
        Debug.Log("Проигрыш уровня");
        PlayerController.Instance.TeleportToEndSpawnPoint();
        PlayerController.Instance.SetMovementEnabled(false); 
        StartCoroutine(LoseCoroutine());
    }
    private static IEnumerator LoseCoroutine()
    {
        Debug.Log("Проигрышная сцена");
        yield return new WaitForSecondsRealtime(2f);
        PanelManager.Instance.Lose();
    }

}