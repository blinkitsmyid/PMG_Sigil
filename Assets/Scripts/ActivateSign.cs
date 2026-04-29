using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActivateOnUltraviolet : MonoBehaviour
{
    [Header("Что включать")]
    public SpriteRenderer spriteRenderer;
    private bool _isInUltravioletZone = false;
    [SerializeField] private Flashlight flashlightScript;
    private void Start()
    {
        if (flashlightScript == null)
            flashlightScript = FindObjectOfType<Flashlight>();

        flashlightScript.OnFlashlightChanged += OnFlashlightChanged;
    }

    private void OnFlashlightChanged(bool isOn)
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        bool isVisible = _isInUltravioletZone && flashlightScript.IsOn();
        spriteRenderer.enabled = isVisible;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ultrafiolet"))
        {
            _isInUltravioletZone = true;
            UpdateVisibility();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ultrafiolet"))
        {
            _isInUltravioletZone = false;
            UpdateVisibility();
        }
    }
}
