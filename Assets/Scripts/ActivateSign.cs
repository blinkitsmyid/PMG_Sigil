using UnityEngine;

public class ActivateOnUltraviolet : MonoBehaviour
{
    [Header("Что включать")]
    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ultrafiolet"))
        {
            spriteRenderer.enabled = true;
            Debug.Log("должен активироваться");
        }

        Debug.Log("В триггере");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ultrafiolet"))
        {
            spriteRenderer.enabled = false;
        }
    }
}
