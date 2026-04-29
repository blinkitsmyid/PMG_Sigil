using UnityEngine;
using UnityEngine.UI;

public class Candle : MonoBehaviour
{
    [Header("Настройки")]
    public int candleIndex;

    [Header("Состояние")]
    public bool isLit;

    [Header("Ссылки")]
    public Image candleImage;
    public Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // 🔥 Зажечь свечу
    public void Light()
    {
        isLit = true;

        if (animator != null)
        {
            animator.SetBool("isLit", true);
        }
    }

    // ❄️ Потушить свечу
    public void Extinguish()
    {
        isLit = false;

        if (animator != null)
        {
            animator.SetBool("isLit", false);
        }
    }

    // 🔄 Полный сброс
    public void ResetCandle()
    {
        isLit = false;

        if (animator != null)
        {
            animator.SetBool("isLit", false);
            
        }
    }
}