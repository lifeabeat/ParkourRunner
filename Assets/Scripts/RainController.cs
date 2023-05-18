using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    private RainScript2D rainController => GetComponent<RainScript2D>();
    [Range(0.0f, 1.0f)]
    [SerializeField] private float rainIntensity;
    [SerializeField] private float targetRainIntensity;

    [SerializeField] private float changeRate = .05f;
    [SerializeField] private float minValue = .2f;
    [SerializeField] private float maxValue = .49f;

    [SerializeField] private float chanceToRain = 40;
    [SerializeField] private float rainCheckCooldown;

    private float rainCheckTimer;

    private bool canChangIntensity;
    private void Update()
    {
        rainCheckTimer -= Time.deltaTime;
        rainController.RainIntensity = rainIntensity;


        if(Input.GetKeyDown(KeyCode.R))
        {
            canChangIntensity = true;
        }

        CheckForRain();

        if (canChangIntensity)
        {
            ChangeIntensity();
        }

        
    }

    private void CheckForRain()
    {
        if(rainCheckTimer < 0 )
        {
            rainCheckTimer = rainCheckCooldown;
            canChangIntensity = true;

            if (Random.Range(0,100) < chanceToRain)
            {
                targetRainIntensity = Random.Range(minValue, maxValue);
            }
            else { targetRainIntensity = 0; }
            
            canChangIntensity = true;
        }
    }
    private void ChangeIntensity()
    {
        if (rainIntensity < targetRainIntensity)
        {
            rainIntensity += changeRate * Time.deltaTime;
            if (rainIntensity >= targetRainIntensity)
            {
                rainIntensity = targetRainIntensity;
                canChangIntensity = false;
            }
        }
        if (rainIntensity > targetRainIntensity)
        {
            rainIntensity -= changeRate * Time.deltaTime;
            if (rainIntensity <= targetRainIntensity)
            {
                rainIntensity = targetRainIntensity;
                canChangIntensity = false;
            }
        } 

    }
}
