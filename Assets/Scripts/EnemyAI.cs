using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target; //transform because it´s looking for where an object is in the world
    [SerializeField] float chaseRange = 5f; //how close you need to be for the enemy to chase
    [SerializeField] float turnSpeed = 5f;

    EnemyHealth enemyHealth;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity; //if it's not initialized like this it wll probably be 0 and the enemy will start chasing?
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyHealth.IsDead);
        EnemyProvoked();
    }

    private void EnemyProvoked()
    {
        if (enemyHealth.IsDead)
        {
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position); //returns a float of the value of the distance

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            //navMeshAgent.SetDestination(target.position);
        }


    }

    public void OnDamageTaken() // interesting way to call this method from other class without needing to change de isProvoked to public
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance) //stoppingDistance é a distancia que o inimigo ficará do player quando segui-lo ao máximo, para não ficar na cara dele
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move"); // mudando trigger la no animator
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true); // mudando o bool lá no animator
        Debug.Log("ATTACKING!!!!");
    }

    private void FaceTarget() //for the enemy to rotate towards the target location when the target is too close
    {
        Vector3 direction = (target.position - transform.position).normalized; //normalized returns this vector with a magnitude of 1, just want to know the direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected() //para desenhar um campo em volta do inimigo para vermos o seu range de ação
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange); //metodo bom para ajudar no game design
    }
}

