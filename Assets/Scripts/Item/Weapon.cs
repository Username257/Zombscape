using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data/Weapon", order = int.MaxValue)]
public class Weapon : Item
{
    GameObject player;
    PlayerAttacker attacker;

    public float angle;
    public float range;
    public int damage;
    public float speed;
    public float freezeTime;
    public float legSpeed;
    public int weildTime = 0;

    public static GameObject weaponHolder;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        weaponHolder = GameObject.FindWithTag("WeaponHolder");
    }

    public override void Start()
    {
        base.Start();
        attacker.OnWeild += CountWeildTime;
    }

    public virtual void HoldWeapon()
    {
        attacker = player.GetComponent<PlayerAttacker>();

        attacker.angle = angle;
        attacker.damage = damage;
        attacker.weaponSpeed = speed;
        attacker.legSpeed = legSpeed;
        attacker.freezeTime = freezeTime;
        attacker.range = range;
        attacker.HoldingWeapon();

    }

    public void CountWeildTime()
    {
        weildTime++;
        if (weildTime >= durability)
        {
            RemoveInInventory();
            Destroy(this.gameObject);
        }
    }

    public void OnDisable()
    {
        attacker = player.GetComponent<PlayerAttacker>();
        attacker.NotHoldingWeapon();
    }
}
