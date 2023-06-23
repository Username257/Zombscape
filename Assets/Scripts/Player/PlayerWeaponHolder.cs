using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    PlayerAttacker attacker;

    private void Awake()
    {
        attacker = gameObject.GetComponentInParent<PlayerAttacker>();
    }

    public void HoldWeapon(Weapon weapon)
    {
        attacker.isHoldingWeapon = true;
        weapon = gameObject.GetComponentInChildren<Weapon>();
        weapon.HoldWeapon();
    }

    public void RemoveWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(false);
        attacker.isHoldingWeapon = false;
    }

}
