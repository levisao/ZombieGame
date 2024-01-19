using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    public float Range { get { return range; } }
    [SerializeField] float maxRange = Mathf.Infinity;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canShoot = true;

    private void OnEnable() //everytime we enable this weapon trhough the scrollwheel or numbers
    {
        canShoot = true; //to fix bug when switching weapon before the coroutine concludes and the canShoot isn't set to true
    }
    // Update is called once per frame
    void Update()
    {
        ProcessShoot();
    }

    private void ProcessShoot()
    {
        if (Input.GetButtonDown("Fire1") && canShoot == true) //default input to fire, assim pode-se atirar de vários devices diferentes, mas se tiver certeza que só será no pc use GetMouseButton(0)
        {
            StartCoroutine(Shoot());
            //Shoot();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false; //para prevenir q comecemos 294294 coroutinas
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);

        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        float currentDamage = damage;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, maxRange))  //(from where iterator's shooting the ray, the direction, "out hit" vai armazenar o resultado de quem o ray vai atingir: vai pegar depois que o método acontecer, range é o alcance total do ray)
        {
            /*
         * If a method needs to return multiple values, an out parameter might be more appropriate.
            Using out parameters can be beneficial when you want to provide additional information about the result of a method, especially in cases where the result might be ambiguous or there are multiple pieces of information to return.
         */
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
            

            if (target == null) // se não for um inimigo retorna, pois não terá o componente EnemyHealth
            {
                return;
            }

            if (Vector3.Distance(target.transform.position, transform.position) > range)
            {
                Debug.Log("TOO FAR BABY");
                currentDamage *= ammoSlot.GetFarMultiplier(ammoType);
            }

            if (hit.transform.tag == "HeadEnemy")
            {
                Debug.Log("IN THE HEAD BABY GIRL");
                currentDamage *= ammoSlot.GetCriticalMultiplier(ammoType);

            }

            target.TakeDamage(currentDamage);  
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //look at the normal location, basically um angulo de 90 graus com a face do que foi hit
        Destroy(impact, .5f);
    }
}
