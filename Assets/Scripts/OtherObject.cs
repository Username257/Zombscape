using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherObject : MonoBehaviour, IFarmingable
{
    InventoryUI otherInventoryUI;
    OthersInventory inventory;

    private void Start()
    {
        inventory = gameObject.GetComponent<OthersInventory>();
        otherInventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();
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
    }

    public void ShowMyInventory()
    {
        otherInventoryUI.inventory = inventory;
        otherInventoryUI.UpdateUI(inventory);
    }

    public void GivePlayersInventory()
    {
    }

    public void TakePlayersInventory()
    {
    }
}
