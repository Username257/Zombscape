using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Weapon Data")]
public class WeaponData : ItemData
{
    public float angle;
    public float range;
    public int damage;
    public float speed;
    public float freezeTime;
    public float legSpeed;
    public float weildSpeed;
}
