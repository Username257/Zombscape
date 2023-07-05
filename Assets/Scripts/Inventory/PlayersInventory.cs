using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInventory : Inventory
{
    PlayerHolder holder;

    public void Awake()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public void HoldItem(string itemName)
    {
        if (!holder.isGrabingSomething)
        {
            ItemData itemData = FindItem(itemName);
            GameObject item = MakeItemInstanceToHold(itemData);
            holder.GrabItem(item);
        }
    }

    public GameObject MakeItemInstanceToHold(ItemData itemData)
    {
        GameObject item = GameManager.Resource.Instantiate<GameObject>($"Item/{itemData.name}");
        item.SetActive(false);
        return item;
    }

}
