using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class AddCrowBar : MonoBehaviour
{
    public void Add(GameObject crowBar)
    {
        GameManager.Inventory.AddItem(crowBar.GetComponent<Weapon>().WeaponData);
    }
}
