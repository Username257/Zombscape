using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> audios = new List<AudioSource>();
    GameObject player;
    PlayerMover mover;
    PlayerAttacker attacker;
    PlayerEater eater;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        mover = player.GetComponent<PlayerMover>();
        attacker = player.GetComponent<PlayerAttacker>();
        eater = player.GetComponent<PlayerEater>();

        for (int i = 0; i < transform.childCount; i++)
        {
            audios.Add(transform.GetChild(i).GetComponent<AudioSource>());
        }
    }

    private void OnEnable()
    {
        mover.playMoveSound += PlaySound;
        attacker.playAttackSound += PlaySound;
        eater.playEatSound += PlaySound;

    }

    private void OnDisable()
    {
        mover.playMoveSound -= PlaySound;
        attacker.playAttackSound -= PlaySound;
        eater.playEatSound -= PlaySound;
    }

    private void PlaySound(string name)
    {
        for (int i = 0; i < audios.Count; i++)
        {
            if (Equals(audios[i].gameObject.name, name))
                audios[i].Play();
        }
    }
}
