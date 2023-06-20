using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<ZombieData> zombieDatas;
    [SerializeField] private GameObject zombiePrefab;
    private ZombieData curZombData;
    static System.Random random = new System.Random();
    [SerializeField]
    int randNum;

    public void Spawn()
    {
        randNum = random.Next(0, 3);
        Zombie newZombie = Instantiate(zombiePrefab).GetComponent<Zombie>();
        newZombie.ZombieData = zombieDatas[randNum];
        newZombie.transform.position = new Vector3(73, 0, -69);
    }
}
