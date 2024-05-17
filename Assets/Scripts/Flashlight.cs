using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;

    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;
    [SerializeField] float lightTimeToDecay = 10f;
    [SerializeField] Camera FPCamera;

    float currentTime = 0f;
    float elapsedTime = 0f;
    int pressCount = 0;
    bool lightOn = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        light = GetComponentInChildren<Light>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        LightInput2();
        DecreaseLightIntensity();
    }

    private void DecreaseLightIntensity()
    {
        //Debug.Log(elapsedTime + " = elapsed time " + currentTime+ " current time" + Time.time + " = time");

        //Debug.Log(Time.time - currentTime);
        if (light.intensity <= Mathf.Epsilon) 
        
        {
            Debug.Log("ACABOU BATE");
            return;
        }
        

        if (light.enabled == true)
        {
            
            elapsedTime += Time.deltaTime;
            //elapsedTime = Time.time - currentTime; //create a variable lightBattery and decrease it by an amount when the light is on.
            
        }
        
        
        

        if (light.enabled == true && elapsedTime >= lightTimeToDecay)
        {
            light.intensity -= lightDecay * Time.deltaTime;
        }

    }

    private void DecreaseLightAngle()
    {

    }

    private void LightInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!lightOn)
            {
                TurnLightOn();

            }
            else
            {
                TurnLightOff();
            }

        }

        
    }

    private void LightInput2()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (pressCount)
            {
                case 0:
                    TurnLightOn();
                    pressCount++;
                    break;
                case 1:
                    light.intensity *= 5f;  //TODO: criar variáveis serializadas
                    light.range += 10f;
                    light.spotAngle -= 35f;
                    pressCount++;
                    break;
                case 2:
                    TurnLightOff();
                    light.intensity /= 5f;
                    light.range -= 10f;
                    light.spotAngle += 35f;
                    pressCount = 0;
                    break;
            }
        }
    }

    private void TurnLightOff()
    {
        light.enabled = false;
        lightOn = false;
    }

    private void TurnLightOn()
    {
        
        currentTime = Time.time;
        light.enabled = true;
        lightOn = true;

        

    }

    private void ProcessRaycastFlashlight()
    {
    }

}
