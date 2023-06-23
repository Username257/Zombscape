using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class AddCrowBar : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public GameObject weaponHolder;

    public void Add(CrowBar crowBar)
    {
        GameObject obj = GameManager.Resource.Instantiate<GameObject>("Weapon/Crowbar");
        obj.transform.SetParent(weaponHolder.transform);
        obj.transform.localPosition = Vector3.zero;
        GameObject inventoryObj = GameObject.FindGameObjectWithTag("InventoryUI");
        inventoryObj.GetComponent<InventoryUI>().AddItem(crowBar);
        //crowBar.Hold();
        weaponHolder.GetComponent<PlayerWeaponHolder>().HoldWeapon(crowBar);
    }
}
