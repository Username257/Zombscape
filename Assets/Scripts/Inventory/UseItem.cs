using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public CrowBar crowBar;
    public PlayerWeaponHolder weaponHolder;

    public void Start()
    {
        weaponHolder = GameObject.FindWithTag("WeaponHolder").GetComponent<PlayerWeaponHolder>();
    }
    public void Use()
    {
        crowBar.HoldCrowBar();
        weaponHolder.GetComponent<PlayerWeaponHolder>().HoldWeapon(crowBar);
    }
}
