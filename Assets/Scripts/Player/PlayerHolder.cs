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
        int index = CheckExistance(item.gameObject);
        
        if (index == -1 || holdingObjs.Count == 0)
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
            holdingObjs[index].SetActive(true);
            holdingObj = holdingObjs[index];
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
            mover.Freeze(2.5f);
            eattingTime = StartCoroutine(HoldTime(3f));
        }
    }

    Coroutine eattingTime;

    private int CheckExistance(GameObject item)
    {
        for (int i = 0; i < holdingObjs.Count; i++)
        {
            holdingObjs[i].gameObject.SetActive(true);
            if (holdingObjs[i].gameObject.name == item.gameObject.name)
                return i;
            holdingObjs[i].gameObject.SetActive(false);
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
