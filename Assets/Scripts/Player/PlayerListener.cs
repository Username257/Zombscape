using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<ZombieEnabler>().EnableTrue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<ZombieEnabler>().EnableFalse();
        }
    }
}
