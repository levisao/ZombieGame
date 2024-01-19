using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    void Start()
    {
        SetWeaponActive();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput(); //mudar arma apertando 1 2 ou 3
        ProcessScrollWheel(); // rodinha do mouse

        if(previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) //vai de -1 a 1? nesse caso irá contar se mover o mouse em uma direção
        {
            if (currentWeapon >= transform.childCount - 1) // se currentWeapon for já o total de armas (childCount - 1 pq o currentweapon começa do 0)
            {
                currentWeapon = 0; //volta para 0 para poder loopar pelas armas de novo
            }
            else
            {
                currentWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0) 
            {
                currentWeapon = transform.childCount - 1; 
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 on the keyboard
        {
            currentWeapon = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform) // pega transform de child tbm // vai loopar por aqui por todos os no Weapon, na ordem ficará pistol = 0, shotgun = 1 e carbine = 2 
        {
            if (weaponIndex == currentWeapon)  //searching for the weapon of the index fornecido
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

}
