using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    GameObject player;
    PlayerAttacker attacker;
    protected int damage;
    protected float speed;
    protected float freezeTime;
    protected float legSpeed;
    
    public virtual void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            player = GameObject.FindWithTag("Player");
            attacker = player.GetComponent<PlayerAttacker>();

            attacker.damage = damage;
            attacker.weaponSpeed = speed;
            attacker.legSpeed = legSpeed;
            attacker.freezeTime = freezeTime;
            attacker.HoldingWeapon();
        }
        else
        {
            attacker.NotHoldingWeapon();
        }
        
    }
}
