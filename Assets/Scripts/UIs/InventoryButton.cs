using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject extraButtonPrefab;
    GameObject extraButton;
    Animator anim;
    public int index;
    InventoryUI inventoryUI;
    public bool isClickedForUse;
    public GameObject weaponHolder;

    public void Awake()
    {
        anim = GetComponent<Animator>();

        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
        index = inventoryUI.items.Count - 1;

        weaponHolder = GameObject.FindWithTag("WeaponHolder");
    }

    public void Start()
    {
        anim.SetTrigger("Normal");
        if(weaponHolder.gameObject.GetComponentInChildren<Weapon>())
            weaponHolder.gameObject.GetComponentInChildren<Weapon>().OnDestroyed += SetIsClickedForUse;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("Highlighted");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.SetTrigger("Pressed");
        anim.enabled = false;
        MakeExtraButton();
    }

    public void SetIsClickedForUse()
    {
        isClickedForUse = !isClickedForUse;
    }

    private void MakeExtraButton()
    {
        extraButton = GameManager.Resource.Instantiate(extraButtonPrefab);
        extraButton.transform.SetParent(transform);
        extraButton.transform.localPosition = new Vector2(290f, -27f);

        GameObject useButton = extraButton.transform.Find("UseButton").gameObject;

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("¹«±â"))
        {
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "ÀåÂø";

            if (isClickedForUse)
            {
                GameObject.Find("useText").GetComponent<TMP_Text>().text = "ÀåÂø ÇØÁ¦";
            }
        }

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("À½½Ä"))
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "¸Ô±â";

        useButton.GetComponent<InventoryExtraButtonAnim>().GetParentAndIndex(gameObject, index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.enabled = true;
        anim.SetTrigger("Normal");
    }

}
