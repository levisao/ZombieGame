using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false; // não permitir trocar armas quando morre
        Cursor.lockState = CursorLockMode.None; //botando para o cursosr parar de ficar travado com o player
        Cursor.visible = true; //botando o cursor para ficar visivel 
    }
}
