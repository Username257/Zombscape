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
        itemName = "��������";
        itemType = "����";
        description = "�޷�";
        prefabRoute = "Weapon/Crowbar";
    }

    public void Hold()
    {
        //GameManager.Resource.Instantiate<Item>("Weapon/Crowbar", weaponHolder);
        //obj.transform.SetParent(weaponHolder.transform);
        //obj.transform.localPosition = Vector3.zero;
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
