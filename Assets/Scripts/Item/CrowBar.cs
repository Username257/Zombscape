using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : Weapon
{
    public static Item crowBar;
    public InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public CrowBar()
    {
        itemName = "¼èÁö·¿´ë";
        itemType = "¹«±â";
        description = "¸Þ·Õ";
    }


    public override void HoldWeapon()
    {
        damage = 3;
        speed = 2f;
        freezeTime = 1f;
        legSpeed = 1f;
        base.HoldWeapon();
    }

    public void AddInInventory()
    {
        inventoryUI.AddItem(crowBar);
    }
}
