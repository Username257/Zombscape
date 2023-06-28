using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class Item : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public string description;
    public int durability;
    public InventoryUI inventoryUI;

    public virtual void Start()
    {
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public void AddInInventory()
    {
        inventoryUI.AddItem(this.gameObject.GetComponent<Item>());
    }
    
    public virtual void RemoveInInventory()
    {
        inventoryUI.RemoveItem(this.gameObject.GetComponent<Item>());
    }
}
