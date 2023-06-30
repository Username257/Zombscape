using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> itemList = new List<ItemData>();
    public List<ItemData> ItemList { get { return itemList; } }
    [SerializeField] private List<int> itemAmount = new List<int>();
    InventoryUI inventoryUI;
    PlayerHolder holder;


    public void Awake()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public void AddItem(ItemData item)
    {
        string itemName = item.itemName;
        int index = CheckExistance(item);
        if (index == -1)
        {
            itemList.Add(item);
            inventoryUI.AddButton(item);
            itemAmount.Add(1);
        }
        else
        {
            itemAmount[index]++;
            inventoryUI.SetButtonsItemCount(itemName, itemAmount[index]);
        }
    }

    public int CheckExistance(ItemData item)
    {
        if (itemList.Count != 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i] == item)
                    return i;
            }
        }
        return -1;
    }

    public void RemoveItem(ItemData item)
    {
        string itemName = item.name;
        int index = CheckExistance(item);
        if (index == -1)
            Debug.Log("아이템이 인벤토리에 존재 안 하는데 삭제하려고 시도함.");
        else
        {
            itemAmount[index]--;
            inventoryUI.SetButtonsItemCount(item.name, itemAmount[index]);
        }

        if (itemAmount[index] <= 0)
            inventoryUI.RemoveButton(item);
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

    private ItemData FindItem(string itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemName)
                return itemList[i];
        }
        return null;
    }

    public GameObject MakeItemInstanceToHold(ItemData itemData)
    {
        GameObject item = GameManager.Resource.Instantiate<GameObject>($"Weapon/{itemData.name}");
        item.SetActive(false);
        return item;
    }

}
