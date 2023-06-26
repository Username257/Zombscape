using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CrowBar : Weapon
{
    public CrowBar()
    {
        itemName = "¼èÁö·¿´ë";
        itemType = "¹«±â";
        description = "¸Þ·Õ";
    }

    public override void Start()
    {
        base.Start();
    }

    public override void HoldWeapon()
    {
        damage = 3;
        speed = 2f;
        angle = 60f;
        range = 1.5f;
        freezeTime = 1f;
        legSpeed = 1f;
        durability = 30;
        base.HoldWeapon();
    }


}
