using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemData;

public class AddCrowBar : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public GameObject weaponHolder;

    public void Add(CrowBar crowBar)
    {
        GameManager.Resource.Instantiate<GameObject>("Weapon/Crowbar", weaponHolder.transform);
        GameObject inventoryObj = GameObject.FindGameObjectWithTag("InventoryUI");
        inventoryObj.GetComponent<InventoryUI>().AddItem(crowBar);
    }
}
