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

    public void Awake()
    {
        anim = GetComponent<Animator>();

        inventoryUI = GameObject.FindWithTag("InventoryUI").GetComponent<InventoryUI>();
        index = inventoryUI.items.Count - 1;
    }

    public void Start()
    {
        anim.SetTrigger("Normal");
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

    private void MakeExtraButton()
    {
        extraButton = GameManager.Resource.Instantiate(extraButtonPrefab);
        extraButton.transform.SetParent(transform);
        extraButton.transform.localPosition = new Vector2(290f, -27f);

        GameObject useButton = extraButton.transform.Find("UseButton").gameObject;

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("����"))
        {
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "����";

            if (isClickedForUse)
            {
                GameObject.Find("useText").GetComponent<TMP_Text>().text = "���� ����";
            }
        }

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("����"))
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "�Ա�";

        useButton.GetComponent<InventoryExtraButtonAnim>().GetParentAndIndex(gameObject, index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("Normal");
    }

}
