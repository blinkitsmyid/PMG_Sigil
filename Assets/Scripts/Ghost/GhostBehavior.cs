using System.Collections;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public static GhostBehaviour Instance;
    public GhostConfig config;

    private bool isDead = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Настройки исчезновения")]
    [SerializeField] private float fadeDuration = 4f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Debug.Log("SpriteRenderer = " + spriteRenderer);
    }
    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        Debug.Log("Using renderer: " + spriteRenderer.name);
    }

    public void ApplyExorcism(ExorcismType type)
    {
        if (isDead) return;
        spriteRenderer.enabled = true;
        PanelManager.Instance.StopTimer();
        if (type == config.correctExorcism)
        {
            GhostManager.Instance.ActivateWinExorcismObject(config);
            WinManager.Instance.WinLevel();
            Debug.Log("Правильно! Призрак уничтожается");
            StartCoroutine(FadeAndDie());
        }
        else
        {
            GhostManager.Instance.ActivateLoseExorcismObject(type);
            LoseManager.Instance.LoseLevel();
            Debug.Log(type);
            //GamesManager.Instance.LoseGame(); // или твой метод
        }
    }
    
    private IEnumerator FadeAndDie()
    {
        Debug.Log("FADE START");

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is NULL!");
            yield break;
        }

        isDead = true;
        AudioManager.Instance.PlayExorcismWinSound();
        Color startColor = spriteRenderer.color;
        float time = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime; // ВАЖНО!

            float t = Mathf.Clamp01(time / fadeDuration);

            Color c = startColor;
            c.a = Mathf.Lerp(startColor.a, 0f, t);

            spriteRenderer.color = c;

            yield return null;
        }

        Debug.Log("FADE END");

        gameObject.SetActive(false);
    }
}