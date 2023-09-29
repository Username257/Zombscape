using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInventory : Inventory
{
    PlayerHolder holder;

    private void Start()
    {
        StartCoroutine(FindPlayerRoutine());
    }

    IEnumerator FindPlayerRoutine()
    {
        yield return new WaitUntil(() => { return GameObject.FindGameObjectWithTag("Player"); });

        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();

        yield break;
    }

    public void Init()
    {
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public void HoldItem(string itemName)
    {
        if (!holder.isGrabingSomething)
        {
            if (itemName == null)
                return;
            ItemData itemData = FindItem(itemName);
            GameObject item = MakeItemInstanceToHold(itemData);
            item.transform.SetParent(holder.transform);
            holder.GrabItem(item);
        }
    }

    public GameObject MakeItemInstanceToHold(ItemData itemData)
    {
        GameObject item = GameManager.Resource.Instantiate<GameObject>($"Item/{itemData.name}");
        return item;
    }

}
