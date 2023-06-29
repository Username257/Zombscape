using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventoryExtraButtonAnim : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;
    GameObject parent;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] PlayerWeaponHolder weaponHolder;
    int index;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
        weaponHolder = GameObject.FindWithTag("WeaponHolder").GetComponent<PlayerWeaponHolder>();
    }
    public void Start()
    {
        anim.SetTrigger("Normal");

    }
    public void GetParentAndIndex(GameObject parent, int index)
    {
        this.parent = parent;
        this.index = index;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("Highlighted");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.SetTrigger("Pressed");

        WhenItsClicked();
    }

    public void WhenItsClicked()
    {
        if (!parent.GetComponent<InventoryButton>().isClickedForUse)
        {
            Item obj;
            for (int i = 0; i < weaponHolder.transform.childCount; i++)
            {
                weaponHolder.transform.GetChild(i).gameObject.SetActive(true);
                obj = weaponHolder.transform.GetChild(i).gameObject.GetComponent<Weapon>();
                weaponHolder.transform.GetChild(i).gameObject.SetActive(false);

                Debug.Log($"{i}번째 obj는 {obj}");
                Debug.Log($"inventoryUI의 items리스트에 obj가 있냐면 {inventoryUI.items.Contains(obj)}");
                int index = inventoryUI.items.FindIndex(a => a == obj);

                if (obj == inventoryUI.items[index].gameObject.GetComponent<Weapon>())
                {
                    obj.gameObject.SetActive(true);
                    weaponHolder.GetComponent<PlayerWeaponHolder>().HoldWeapon(obj.GetComponent<Weapon>());

                    parent.GetComponent<InventoryButton>().isClickedForUse = true;
                }
            }

        }
        else
        {
            Item obj = inventoryUI.items[index].gameObject.GetComponent<Weapon>();
            weaponHolder.GetComponent<PlayerWeaponHolder>().RemoveWeapon(obj.GetComponent<Weapon>());

            parent.GetComponent<InventoryButton>().isClickedForUse = false;
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("먹기"))
        {
            //먹기
        }

        if (transform.Find("throwText"))
        {
            //버리기
        }

        Destroy(parent.transform.Find("InventoryExtraButton(Clone)").gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("Normal");
    }
}
