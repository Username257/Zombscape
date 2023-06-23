using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCrowBar : MonoBehaviour
{
    [SerializeField] CrowBar crowBar;
    [SerializeField] PlayerWeaponHolder weaponHolder;

    public void Get()
    {
        crowBar.gameObject.SetActive(true);
        weaponHolder.HoldWeapon();
    }
}
