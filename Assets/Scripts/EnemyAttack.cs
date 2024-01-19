using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    PlayerHealth target;
    [SerializeField] float damage = 40f;


    void Start()
    {
        target = FindObjectOfType<PlayerHealth>(); //do not put this on update, it's heavy?
    }

    public void AttackHitEvent() //called in the event of the enemy attack animation
    {
        if (target == null)
        {
            return;
        }
        Debug.Log("ATTACKK!!!!!");
        target.TakeDamage(damage);
    }
}
