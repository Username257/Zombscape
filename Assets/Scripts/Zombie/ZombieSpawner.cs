using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private List<ZombieData> zombieDatas;
    [SerializeField] private GameObject zombiePrefab;
    private ZombieData curZombData;
    static System.Random random = new System.Random();
    [SerializeField]
    int randNum;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Spawn()
    {
        randNum = random.Next(0, 3);
        Zombie newZombie = Instantiate(zombiePrefab).GetComponent<Zombie>();
        newZombie.zombieData = zombieDatas[randNum];
        newZombie.transform.position = new Vector3(player.transform.position.x + 10f, 0, player.transform.position.z + 10f);
        newZombie.OtherInventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();
    }
}
