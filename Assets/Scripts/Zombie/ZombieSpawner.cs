using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] int spawnNum;
    private RandomPositionGenerator RPG;
    private GameObject player;
    [SerializeField] private List<ZombieData> zombieDatas;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] GameObject parent;
    private ZombieData curZombData;
    static System.Random random = new System.Random();
    int randNum;
    Vector3 pos;

    public void Start()
    {
        RPG = GameObject.FindWithTag("RandomPositionGenerator").GetComponent<RandomPositionGenerator>();
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            randNum = random.Next(0, zombieDatas.Count);

            pos = RPG.SetPosition();

            while (pos == Vector3.zero)
            {
                pos = RPG.SetPosition();
                if (pos != Vector3.zero)
                    break;
            }


            Zombie newZombie = Instantiate(zombiePrefab, pos, Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<Zombie>();
            newZombie.zombieData = zombieDatas[randNum];
        }
    }
}
