using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerStartPosition : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<Transform> positions = new List<Transform>();
    int index;

    public void Awake()
    {
        index = Random.Range(0, positions.Count);
        player.transform.position = positions[index].position;
    }
}
