using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class Food : Item, IGetable, IUseable
{
    PlayerHolder holder;
    [SerializeField] FoodData foodData;
    [SerializeField] int useTime;
    int durability;

    public Food(ItemData data) : base(data)
    {
        data = foodData;
    }

    public FoodData FoodData { get { return foodData; } }

    public void Start()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        durability = foodData.durability;

        gameObject.transform.SetParent(holder.transform);
        gameObject.transform.localPosition = Vector3.zero;
    }

    public void AddInInventory()
    {
        GameManager.Inventory.AddItem(foodData);
    }

    public void RemoveInInventory()
    {
        GameManager.Inventory.RemoveItem(foodData);
    }

    public void CountUseTime()
    {
        useTime++;
        if (useTime >= durability)
        {
            RemoveInInventory();
        }
    }

}
