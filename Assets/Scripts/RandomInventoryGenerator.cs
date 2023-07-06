using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomInventoryGenerator : MonoBehaviour
{
    [SerializeField] private List<ItemData> weaponDatas;
    [SerializeField] private List<ItemData> foodDatas;
    static System.Random random = new System.Random();
    int randNum;
    Inventory inventory;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        int num = ItemNums();
        for (int i = 0; i < num; i++)
        {
            ItemData item = GetItem();
            if (inventory == null)
                Debug.Log("인벤토리가 null");
            inventory.AddItem(item);
        }
    }


    private int GetRandomNum(int start, int end)
    {
        return randNum = random.Next(start, end);
    }

    private ItemData GetItem()
    {
        int i = GetRandomNum(0, 100);

        if (i < 70)
        {
            i = GetRandomNum(0, 11);
            return foodDatas[i];
        }
        else
        {
            i = GetRandomNum(0, 2);
            return weaponDatas[i];
        }
    }

    private int ItemNums()
    {
        return randNum = random.Next(0, 14);
    }
}
