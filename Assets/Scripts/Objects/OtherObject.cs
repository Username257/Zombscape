using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherObject : MonoBehaviour, IFarmingable
{
    [SerializeField] protected InventoryUI otherInventoryUI;
    public InventoryUI OtherInventoryUI { get { return otherInventoryUI; } set { otherInventoryUI = value; } }
    [SerializeField] protected Inventory inventory;
    public ItemData coneSoup;
    public ItemData axe;
    public ItemData crowbar;
    public bool canShowInventory = true;

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        inventory = gameObject.GetComponent<Inventory>();
        otherInventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();
    }

    public void AddItem()
    {
        inventory.AddItem(coneSoup);
        inventory.AddItem(coneSoup);
        inventory.AddItem(coneSoup);
        inventory.AddItem(axe);
        inventory.AddItem(crowbar);
        inventory.AddItem(crowbar);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canShowInventory)
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
    }

}
