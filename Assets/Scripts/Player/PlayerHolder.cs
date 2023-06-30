using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    PlayerAttacker attacker;
    public bool isGrabingSomething;

    private void Awake()
    {
        attacker = gameObject.GetComponentInParent<PlayerAttacker>();
    }

    public void GrabItem(GameObject item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(true);

        isGrabingSomething = true;

        if (item.GetComponent<Weapon>() != null)
        {
            attacker.isHoldingWeapon = true;
            attacker.HoldingWeapon(item.GetComponent<Weapon>());
        }
    }

    public void ReleaseItem(GameObject item)
    {
        item.gameObject.SetActive(false);

        isGrabingSomething = false;

        if (item.GetComponent<Weapon>() != null)
        {
            attacker.isHoldingWeapon = false;
            attacker.NotHoldingWeapon();
        }
    }


}
