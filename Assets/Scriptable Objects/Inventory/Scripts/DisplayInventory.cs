using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;

    [SerializeField] int X_START;
    [SerializeField] int Y_START;
    [SerializeField] int X_SPACE_BETWEEN_ITEM;
    [SerializeField] int NUMBER_OF_COLUMN;
    [SerializeField] int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();
    void Start()
    {
        CreateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i); //In Unity, RectTransform is a component that is used in conjunction with the Canvas component to define the position, size, and anchoring of UI elements. It is part of the Unity UI system and is particularly important when working with UI elements in the Canvas.
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemDisplayed.Add(inventory.Container[i], obj); //adicionando ao dicionário 
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)),0);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i); //In Unity, RectTransform is a component that is used in conjunction with the Canvas component to define the position, size, and anchoring of UI elements. It is part of the Unity UI system and is particularly important when working with UI elements in the Canvas.
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
}
