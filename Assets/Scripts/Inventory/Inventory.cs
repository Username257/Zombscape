using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<ItemData> itemList = new List<ItemData>();
    public List<int> itemAmount = new List<int>();
    public int index;
    public string itemName;
    public InventoryUI inventoryUI;


    public virtual void AddItem(ItemData item)
    {
        itemName = item.itemName;
        index = CheckExistance(item);
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
        itemName = item.name;
        int index = CheckExistance(item);
        if (index == -1)
            Debug.Log("아이템이 인벤토리에 존재하지 않는데 삭제하려고 시도함.");
        else
        {
            if (itemList[index] != null)
            {
                itemAmount[index]--;
                inventoryUI.SetButtonsItemCount(item.name, itemAmount[index]);
            }
            
        }

        if (itemAmount[index] == 0)
        {
            itemAmount.RemoveAt(index);
            itemList.RemoveAt(index);
            Destroy(inventoryUI.buttons[index].gameObject);
            inventoryUI.RemoveButton(item);
        }
    }
            

    public ItemData FindItem(string itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemName)
                return itemList[i];
        }
        return null;
    }

}
