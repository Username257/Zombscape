using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class AddCrowBar : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public GameObject weaponHolder;

    private void Start()
    {
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    public void Add(GameObject crowBar)
    {
        bool isCrowBarAlreadyExist = weaponHolder.transform.Find("Crowbar(Clone)");

        GameObject obj;
        if (!isCrowBarAlreadyExist)
        {
            obj = GameManager.Resource.Instantiate<GameObject>("Weapon/Crowbar");
            obj.transform.SetParent(weaponHolder.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = new Vector3(1f, 1f, 1f);

            obj.SetActive(false);
        }
        else
        {
            obj = weaponHolder.transform.Find("Crowbar(Clone)").gameObject;
        }

        GameObject inventoryObj = GameObject.FindGameObjectWithTag("InventoryUI");
        inventoryObj.GetComponent<InventoryUI>().AddItem(obj.GetComponent<Item>());

    }
}
