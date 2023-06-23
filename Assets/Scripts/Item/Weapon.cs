using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    GameObject player;
    PlayerAttacker attacker;
    protected int damage;
    protected float speed;
    protected float freezeTime;
    protected float legSpeed;

    public static GameObject weaponHolder;

    private void Awake()
    {
        weaponHolder = GameObject.FindWithTag("WeaponHolder");
    }


    public virtual void HoldWeapon()
    {
        player = GameObject.FindWithTag("Player");
        attacker = player.GetComponent<PlayerAttacker>();

        attacker.damage = damage;
        attacker.weaponSpeed = speed;
        attacker.legSpeed = legSpeed;
        attacker.freezeTime = freezeTime;
        attacker.HoldingWeapon();

    }

    public void OnDisable()
    {
        attacker.NotHoldingWeapon();
    }
}
