using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherObject : MonoBehaviour, IFarmingable
{
    public UnityEvent OnShowInventory;
    InventoryUI otherInventoryUI;
    OthersInventory inventory;
    Collider trigger;

    private void Start()
    {
        inventory = gameObject.GetComponent<OthersInventory>();
        otherInventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            ShowMyInventory();
    }

    public void ShowMyInventory()
    {
        otherInventoryUI.UpdateUI(inventory);
    }

    public void GivePlayersInventory()
    {
    }

    public void TakePlayersInventory()
    {
    }
}
