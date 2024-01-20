using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{

    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedIntFOV = 20f;
    [SerializeField] float zoomFOVSpeed = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    RigidbodyFirstPersonController fpsController;

    bool zoomedInToggle = false;

    private void OnDisable()
    {
        //ZoomOut(); //fixing "bug" where não dava zoomout quando trocava de arma
        ZoomOutDisableGun();
    }

    

    void Start()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
        fpsCamera.fieldOfView = zoomedOutFOV;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }


    private void ZoomIn()
    {
        zoomedInToggle = true;

        StartCoroutine(ZoomSmoothly());
        //fpsCamera.fieldOfView = zoomedIntFOVSpeed;

        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
        //GetComponent<RigidbodyFirstPersonController>().mouseLook.YSensitivity = zoomInSensitivity; //not indicated to use like this because will retrieve the class everytime in update and we only need this one time. Can be heavy
    }
    IEnumerator ZoomSmoothly()
    {
        
        while (fpsCamera.fieldOfView > zoomedIntFOV && zoomedInToggle)
        {
            fpsCamera.fieldOfView -= zoomFOVSpeed * Time.deltaTime;
            //Debug.Log("Field of View: " + fpsCamera.fieldOfView);

            yield return null;
        }

        if (zoomedInToggle)
        {
            Debug.Log("EU VIM PRA ESSA TERRA DE MEU DEUS COMM UITOS SONHOS E POUCO TEMPO!!");
            fpsCamera.fieldOfView = zoomedIntFOV;
        }
        

        while (fpsCamera.fieldOfView < zoomedOutFOV && !zoomedInToggle)
        {
            fpsCamera.fieldOfView += zoomFOVSpeed * Time.deltaTime;

            Debug.Log("Field of View: " + fpsCamera.fieldOfView + "  " + zoomFOVSpeed);

            yield return null;
        }

        if (!zoomedInToggle)
        {
            fpsCamera.fieldOfView = zoomedOutFOV;
        }
        


    }
    private void ZoomOut()
    {
        zoomedInToggle = false;
        StartCoroutine(ZoomSmoothly());

        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    }

    private void ZoomOutDisableGun()
    {
        zoomedInToggle = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    }
}

    
