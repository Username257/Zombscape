using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie Data", menuName = "Scriptable Object/Zombie Data")]
public class ZombieData : ScriptableObject
{
    [SerializeField] private string name;
    public string Name { get { return name; } }

    [SerializeField] private int hp;
    public int Hp { get { return hp; } }

    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
}
