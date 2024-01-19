using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float hitPoints = 100f;
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath(); //interesting! Chamando método de outra classe sem precisar instanciar ou assign no serializeField...
            Debug.Log("DEAD");
        }
    }
}
