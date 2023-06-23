using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    Weapon weapon;
    PlayerAttacker attacker;
    bool isGetWeapon;

    private void Awake()
    {
        attacker = gameObject.GetComponentInParent<PlayerAttacker>();
    }

    public void HoldWeapon()
    {
        attacker.isHoldingWeapon = true;
        weapon = gameObject.GetComponentInChildren<Weapon>();
        weapon.HoldWeapon();
    }

    public void RemoveWeapon()
    {
        weapon.gameObject.SetActive(false);
        attacker.isHoldingWeapon = false;
    }

}
