using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    PlayerAttacker attacker;
    PlayerEater eater;
    PlayerMover mover;
    public bool isGrabingSomething;
    public List<GameObject> holdingObjs = new List<GameObject>();
    public GameObject holdingObj;

    private void Awake()
    {
        mover = gameObject.GetComponentInParent<PlayerMover>();
        attacker = gameObject.GetComponentInParent<PlayerAttacker>();
        eater = gameObject.GetComponentInParent<PlayerEater>();
    }

    public void GrabItem(GameObject item)
    {
        if (CheckExistance(item) == -1)
        {
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(true);
            holdingObjs.Add(item);
            holdingObj = item;
        }
        else
        {
            holdingObjs[CheckExistance(item)].SetActive(true);
            holdingObj = holdingObjs[CheckExistance(item)];
            Destroy(item);
        }

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

    private int CheckExistance(GameObject item)
    {
        for (int i = 0; i < holdingObjs.Count; i++)
        {
            if (holdingObjs[i].gameObject.GetComponent<Weapon>().WeaponData == item.gameObject.GetComponent<Weapon>().WeaponData)
                return i;
        }
        return -1;
    }

    IEnumerator HoldTime(float holdTime)
    {
        yield return new WaitForSeconds(holdTime);
        ReleaseItem();
    }

    public void ReleaseItem()
    {
        holdingObj.SetActive(false);

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
