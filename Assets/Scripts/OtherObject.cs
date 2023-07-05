using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherObject : MonoBehaviour, IFarmingable
{
    InventoryUI otherInventoryUI;
    OthersInventory inventory;
    public ItemData axe;
    public ItemData coneSoup;

    private void Start()
    {
        inventory = gameObject.GetComponent<OthersInventory>();
        otherInventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();
        inventory.AddItem(axe);
        inventory.AddItem(axe);
        inventory.AddItem(axe);
        inventory.AddItem(coneSoup);
        inventory.AddItem(coneSoup);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowMyInventory();
            GameManager.Inventory.inventoryUI.othersInventory = inventory;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        otherInventoryUI.RemoveAll();
        otherInventoryUI.inventory = null;

    }

    public void ShowMyInventory()
    {
        otherInventoryUI.inventory = inventory;
        otherInventoryUI.UpdateUI(inventory);
        GameManager.Inventory.inventoryUI.othersInventory = inventory;
        GameManager.Inventory.inventoryUI.UpdateUI(GameManager.Inventory);
    }

}
