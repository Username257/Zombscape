using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public string description;
    public int durability;
    public enum ItemType
    {
        무기, 음식
    }

    //public InventoryUI inventoryUI;

    public virtual void Start()
    {
        //inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public void AddInInventory()
    {
        //inventoryUI.AddItem(this.gameObject.GetComponent<Item>());
    }
    
}
