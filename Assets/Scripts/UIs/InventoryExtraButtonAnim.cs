using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryExtraButtonAnim : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;
    GameObject parent;
    InventoryUI inventoryUI;
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
            if (inventoryUI.items[index].gameObject.GetComponent<Weapon>())
            {
                Item obj = inventoryUI.items[index].gameObject.GetComponent<Weapon>();
                obj.gameObject.SetActive(true);
                weaponHolder.GetComponent<PlayerWeaponHolder>().HoldWeapon(obj.GetComponent<Weapon>());

                parent.GetComponent<InventoryButton>().isClickedForUse = true;
            }
        }
        else
        {
            Item obj = inventoryUI.items[index].gameObject.GetComponent<Weapon>();
            weaponHolder.GetComponent<PlayerWeaponHolder>().RemoveWeapon(obj.GetComponent<Weapon>());

            parent.GetComponent<InventoryButton>().isClickedForUse = false;
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("¸Ô±â"))
        {
            //¸Ô±â
        }

        if (transform.Find("throwText"))
        {
            //¹ö¸®±â
        }

        Destroy(parent.transform.Find("InventoryExtraButton(Clone)").gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("Normal");
    }
}
