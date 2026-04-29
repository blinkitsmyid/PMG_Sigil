using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashingLamp : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    
    [Header("Интервал между миганиями")]
    [SerializeField] private float maxDelay = 5f;
    [SerializeField] private float minDelay = 3f;

    [Header("Интенсивности (3 шага)")]
    [SerializeField] private float minIntensity = 0.3f;
    [SerializeField] private float maxIntensity = 2.5f;


    [Header("Скорость мигания")]
    [SerializeField] private float flickerStepDelay = 0.1f;
    private Coroutine  _flashLightRoutine;
    public bool isLampFlashing = false;
    private void Start()
    {
        if (light2D == null)
        {
            Debug.LogError("Light2D не назначен!");
            return;
        }
        isLampFlashing = true;
        if (isLampFlashing)
        {
            FlashLightStart();
        }
        
    }
    public void FlashLightStart()
    {
        if (_flashLightRoutine == null && isLampFlashing)
        {
            _flashLightRoutine = StartCoroutine(FlashingRoutine());
        }
    }
    public void FlashLightStop()
    {
        if (_flashLightRoutine == null && !isLampFlashing)
        {
            StopCoroutine(_flashLightRoutine);
        }
        
    }
    private IEnumerator FlashingRoutine()
    {
        while (true)
        {
            // Ждём перед следующим миганием
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerStepDelay);
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerStepDelay);
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerStepDelay);
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerStepDelay);
        }
    }
}
