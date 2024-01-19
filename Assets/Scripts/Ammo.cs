using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ammo : MonoBehaviour
{
    //[SerializeField] int ammoAmount = 10;
    [SerializeField] AmmoSlot[] ammoSlot;

    [System.Serializable]
    private class AmmoSlot //WHAT // This class is only accessible to the Ammo class // public variables will only be accessible to the Ammo class
    {
        public AmmoType ammoType;
        public int ammoAmount;
        [Range(1f,5f)] public float ammoCriticalMultiplier = 1f;
        [Range(0.1f, 1f)] public float ammoFarMultiplier = 1f;
    }

    //public int AmmoAmount { get { return ammoAmount; } }
    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    public float GetCriticalMultiplier(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoCriticalMultiplier;
    }
    public float GetFarMultiplier(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoFarMultiplier;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType) //pegando o tipo de slot já stored e instanciado pelo player
    {
        foreach (AmmoSlot slot in ammoSlot)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

}
