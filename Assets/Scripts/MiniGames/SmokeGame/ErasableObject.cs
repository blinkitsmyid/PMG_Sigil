using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ErasableObject : MonoBehaviour
{
    [Header("Настройки прозрачности")]
    public float fadeStep = 0.5f;      // на сколько уменьшаем за раз
    public float fadeDuration = 0.3f;  // скорость плавного исчезновения

    private SpriteRenderer sr;
    private float currentAlpha = 1f;
    private Coroutine fadeRoutine;
    private bool canErase = true;
    public float eraseCooldown = 0.5f;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        currentAlpha = sr.color.a;
    }

    public void ApplyErase()
    {
        if (!canErase) return;

        canErase = false;
        StartCoroutine(EraseCooldown());

        float targetAlpha = currentAlpha - fadeStep;

        if (targetAlpha <= 0.4f)
        {
            DestroyObject();
            AudioManager.Instance.PlaySmoke();
            return;
        }

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTo(targetAlpha));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = currentAlpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            SetAlpha(newAlpha);

            yield return null;
        }

        currentAlpha = targetAlpha;
        SetAlpha(currentAlpha);
    }
    private IEnumerator EraseCooldown()
    {
        yield return new WaitForSeconds(eraseCooldown);
        canErase = true;
    }
    private void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }

    private void DestroyObject()
    {
        // уведомляем менеджер
        SmokeGameManager.Instance.RemoveTarget(gameObject);

        Destroy(gameObject);
    }
}