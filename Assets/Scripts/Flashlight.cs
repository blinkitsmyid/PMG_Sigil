using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light2D flashlight;

    void Start()
    {
        GameInput.Instance.OnFlashlightToggle += GameInput_OnFlashlightToggle;
        flashlight.enabled = false;
    }
    private void GameInput_OnFlashlightToggle(object sender, EventArgs e)
    {
        flashlight.enabled = !flashlight.enabled;
        Debug.Log("Flashlight toggled!");
    }
    
    private void OnDestroy()
    {
        // Важно отписываться при уничтожении объекта
        if (GameInput.Instance != null)
            GameInput.Instance.OnFlashlightToggle -= GameInput_OnFlashlightToggle;
    }
 
   
}