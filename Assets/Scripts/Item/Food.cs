using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class Food : MonoBehaviour, IHoldable, IGetable, IUseable
{
    PlayerHolder holder;
    [SerializeField] FoodData foodData;
    [SerializeField] int useTime;
    int durability;
    public FoodData FoodData { get { return foodData; } }

    public void Start()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        durability = foodData.durability;
    }
    public void Hold(GameObject obj)
    {
        holder.GrabItem(obj);
    }
    public void Release()
    {
        holder.ReleaseItem();
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
            Release();
        }
    }

}
