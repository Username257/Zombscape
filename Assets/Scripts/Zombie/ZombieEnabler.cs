using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnabler : MonoBehaviour
{
    [SerializeField] GameObject sound;
    public void Awake()
    {
        transform.GetComponent<Zombie>().enabled = false;

        for (int i = 0; i < sound.transform.childCount; i++)
            sound.transform.GetChild(i).transform.gameObject.SetActive(false);
    }
    public void EnableTrue()
    {
        transform.GetComponent<Zombie>().enabled = true;

        for (int i = 0; i < sound.transform.childCount; i++)
            sound.transform.GetChild(i).transform.gameObject.SetActive(true);
    }
    public void EnableFalse()
    {
        transform.GetComponent<Zombie>().enabled = false;

        for (int i = 0; i < sound.transform.childCount; i++)
            sound.transform.GetChild(i).transform.gameObject.SetActive(false);
    }
}
