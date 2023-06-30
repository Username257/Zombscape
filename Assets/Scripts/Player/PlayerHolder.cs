using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    PlayerAttacker attacker;
    public bool isGrabingSomething;
    GameObject holdingObj;

    private void Awake()
    {
        attacker = gameObject.GetComponentInParent<PlayerAttacker>();
    }

    public void GrabItem(GameObject item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.gameObject.SetActive(true);
        holdingObj = item;

        isGrabingSomething = true;

        if (item.GetComponent<Weapon>() != null)
        {
            attacker.isHoldingWeapon = true;
            attacker.HoldingWeapon(item.GetComponent<Weapon>());
        }
    }

    public void ReleaseItem()
    {
        Destroy(holdingObj.gameObject);

        isGrabingSomething = false;

        if (holdingObj.GetComponent<Weapon>() != null)
        {
            attacker.isHoldingWeapon = false;
            attacker.NotHoldingWeapon();
        }
    }


}
