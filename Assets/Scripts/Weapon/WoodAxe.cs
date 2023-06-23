using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAxe : Weapon
{
    public override void HoldWeapon()
    {
        damage = 7;
        speed = 1f;
        freezeTime = 2f;
        legSpeed = 0.3f;
        base.HoldWeapon();
    }
}
