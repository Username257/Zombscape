using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public string description;
    public InventoryUI inventoryUI;

    public virtual void Awake()
    {
        inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUI>();
    }


}
