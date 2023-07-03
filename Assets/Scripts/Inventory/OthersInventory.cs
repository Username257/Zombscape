using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class OthersInventory : Inventory
{
    public void Start()
    {
        inventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();
    }
}
