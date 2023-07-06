using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Weapon : MonoBehaviour, IUseable, IGetable
{
    PlayerHolder holder;
    PlayerAttacker attacker;
    [SerializeField] WeaponData weaponData;
    public WeaponData WeaponData { get { return weaponData; } }
    [SerializeField] int useTime;
    int durability;
    public void Start()
    {
        attacker = GameObject.FindWithTag("Player").GetComponent<PlayerAttacker>();
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        durability = weaponData.durability;

        attacker.OnWeild += CountUseTime;

        if (weaponData.itemName == "ÈÄ¶óÀÌÆÒ")
        {
            gameObject.transform.SetParent(holder.transform.Find("FryingPanPosition").transform);
            gameObject.transform.localPosition = Vector3.zero;
        }
    }

    public void AddInInventory()
    {
        GameManager.Inventory.AddItem(weaponData);
    }

    public void RemoveInInventory()
    {
        GameManager.Inventory.RemoveItem(weaponData);
    }
    public void CountUseTime()
    {
        useTime++;
        if (useTime >= durability)
        {
            RemoveInInventory();
        }
    }

 
}
