using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] float timeToDisappearWhenDead = 6f;
    

    PlayerHealth target;

    bool isDead = false;
    public bool IsDead { get { return isDead; } }

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); //calls the method with this name on every monobehaviours assigned to this object and its children


        hitPoints -= damage;
        
        //GetComponent<EnemyAI>().OnDamageTaken(); //can be like this, but used the broadcastMessage because we want this to happen in more than one class

        if (hitPoints <= 0)
        {
            Die();
            Destroy(gameObject, timeToDisappearWhenDead);
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
