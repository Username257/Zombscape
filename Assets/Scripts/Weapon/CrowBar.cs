using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : Weapon
{
    public override void Awake()
    {
        itemName = "쇠지렛대";
        itemType = "무기";
        description = "갈라진 틈에 못 머리를 끼워 지레의 원리로 못을 뽑을 수 있는 공구다.";
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
        inventoryUI.AddItem(this);
    }
}
