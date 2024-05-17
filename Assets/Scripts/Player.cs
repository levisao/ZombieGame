using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();


        if (item)
        {
            inventory.AddItem(item.Itemm, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
