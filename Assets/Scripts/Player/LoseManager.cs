using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    public static LoseManager Instance;
    
    [Header("Ghost Appearance")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float targetAlpha = 0.7f;
    public Image ghostImage;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoseLevel()
    {
        Debug.Log("Проигрыш уровня");

        PlayerController.Instance.TeleportToEndSpawnPoint();
        PlayerController.Instance.SetMovementEnabled(false);

        StartCoroutine(LoseSequence());
        ghostImage.gameObject.SetActive(false);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoseSequence()
    {
        
        // 2. Появление спрайта
        yield return StartCoroutine(FadeInGhostUI());

        // 3. Задержка перед проигрышем
        yield return new WaitForSecondsRealtime(0.5f);

        // 4. Финал
        yield return StartCoroutine(LoseCoroutine());
        
    }

   

    private IEnumerator FadeInGhostUI()
    {
        if (ghostImage == null)
            yield break;

        Color color = ghostImage.color;
        color.a = 0f;
        ghostImage.color = color;

        ghostImage.gameObject.SetActive(true);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / fadeDuration;

            color.a = Mathf.Lerp(0f, targetAlpha, t);
            ghostImage.color = color;

            yield return null;
        }
        
        color.a = targetAlpha;
        ghostImage.color = color;
        
    }

    private static IEnumerator LoseCoroutine()
    {
        
        Debug.Log("Проигрышная сцена");
        yield return new WaitForSecondsRealtime(1f);
        PanelManager.Instance.Lose();
    }
}