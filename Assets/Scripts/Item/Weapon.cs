using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Weapon : Item, IUseable, IGetable
{
    PlayerHolder holder;
    PlayerAttacker attacker;
    [SerializeField] WeaponData weaponData;
    public WeaponData WeaponData { get { return weaponData; } }
    [SerializeField] int useTime;
    int durability;

    public Weapon(ItemData data) : base(data)
    {
        data = weaponData;
    }

    public void Start()
    { 
        attacker = GameObject.FindWithTag("Player").GetComponent<PlayerAttacker>();
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        durability = weaponData.durability;

        attacker.OnWeild += CountUseTime;

        if (this.gameObject.GetComponent<Weapon>().weaponData.itemName == "ÈÄ¶óÀÌÆÒ")
        {
            gameObject.transform.SetParent(holder.transform.Find("FryingPanHolder").gameObject.transform);
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
            holder.ReleaseItem();
            RemoveInInventory();
            useTime = 0;
        }
    }

 
}
