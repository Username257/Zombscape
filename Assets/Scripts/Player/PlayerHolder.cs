using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    PlayerAttacker attacker;
    PlayerEater eater;
    PlayerMover mover;
    public bool isGrabingSomething;
    public GameObject holdingObj;

    private void Awake()
    {
        mover = gameObject.GetComponentInParent<PlayerMover>();
        attacker = gameObject.GetComponentInParent<PlayerAttacker>();
        eater = gameObject.GetComponentInParent<PlayerEater>();
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

        if (item.GetComponent<Food>() != null)
        {
            eater.isHoldingFood = true;
            eater.IsHoldingFood(item.GetComponent<Food>());
            mover.Freeze(2f);
            eattingTime = StartCoroutine(HoldTime(2f));
        }
    }

    Coroutine eattingTime;

    IEnumerator HoldTime(float holdTime)
    {
        yield return new WaitForSeconds(holdTime);
        ReleaseItem();
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

        if (holdingObj.GetComponent<Food>() != null)
        {
            eater.isHoldingFood = false;
            holdingObj.GetComponent<Food>().CountUseTime();
        }
    }


}
