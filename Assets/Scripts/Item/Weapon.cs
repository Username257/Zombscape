using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Weapon : MonoBehaviour, IUseable, IHoldable, IGetable
{
    PlayerHolder holder;
    [SerializeField] WeaponData weaponData;
    public WeaponData WeaponData { get { return weaponData; } }
    int useTime;
    int durability;
    public void Start()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
        durability = weaponData.durability;
    }

    public void Hold(GameObject obj)
    {
        holder.GrabItem(obj);
    }
    public void Release(GameObject obj)
    {
        holder.ReleaseItem(obj);
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
            Release(gameObject);
        }
    }

 
}
