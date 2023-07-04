using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] public static int maxSlot = 15;
    public ItemData[] itemList;
    public int[] itemAmount;
    public int index;
    public string itemName;
    public InventoryUI inventoryUI;

    public void OnEnable()
    {
        itemList = new ItemData[maxSlot];
        itemAmount = new int[maxSlot];
    }
    public virtual void AddItem(ItemData item)
    {
        itemName = item.itemName;
        index = CheckExistance(item);
        if (index == -1)
        {
            index = FindEmptySlot();
            if (index != -1)
            {
                itemList[index] = item;
                itemAmount[index]++;
                inventoryUI.AddButton(item);
            }
            else
                Debug.Log("비어있는 슬롯을 못 찾음");
        }
        else
        {
            itemAmount[index]++;
            inventoryUI.SetButtonsItemCount(itemName, itemAmount[index]);
        }
    }

    public int FindEmptySlot()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
                return i;
        }
        return -1;
    }

    public int CheckExistance(ItemData item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == item)
                return i;
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
                inventoryUI.SetButtonsItemCount(item.itemName, itemAmount[index]);
            }
            
        }

        if (itemAmount[index] == 0)
        {
            itemList[index] = null;
            Destroy(inventoryUI.buttons[index].gameObject);
            inventoryUI.RemoveButton(item);
        }
    }
            

    public ItemData FindItem(string itemName)
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (itemList[i].itemName == itemName)
                return itemList[i];
        }
        return null;
    }

}
