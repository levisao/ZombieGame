using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;

    int pressCount = 0;
    bool lightOn = false;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        LightInput2();
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
                    light.intensity += 10f;  //TODO: criar variáveis serializadas
                    light.range += 10f;
                    light.spotAngle -= 35f;
                    pressCount++;
                    break;
                case 2:
                    TurnLightOff();
                    light.intensity -= 10f;
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
        light.enabled = true;
        lightOn = true;
    }
}
