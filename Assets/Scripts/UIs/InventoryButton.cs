using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class InventoryButton : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] GameObject extraButtonPrefab;
    string itemName;
    public ItemData itemData;
    public bool isClickedForUse;
    public bool isOthersInventoryUI;
    [SerializeField] GameObject tempButton;
    public Inventory inventory;
    public Inventory othersInventory;
    PlayerAttacker attacker;
    public void Awake()
    {
        tempButton = GameObject.FindWithTag("tempButton");

    }
    public void Start()
    {
        tempButton.gameObject.SetActive(false);
        itemName = transform.GetChild(0).GetComponent<TMP_Text>().text;
        attacker = GameObject.FindWithTag("Player").GetComponent<PlayerAttacker>();
    }
    public void SetInventory(Inventory mine, Inventory other)
    {
        inventory = mine;
        othersInventory = other;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOthersInventoryUI)
            return;
        MakeExtraButton();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        tempButton.GetComponent<ButtonDrag>().isUsed = false;
        tempButton.GetComponent<ButtonDrag>().targetInventory = othersInventory;
        tempButton.GetComponent<ButtonDrag>().mineInventory = inventory;
        tempButton.GetComponent<ButtonDrag>().button = this;
        tempButton.GetComponent<ButtonDrag>().itemData = itemData;
        tempButton.transform.GetChild(0).GetComponent<TMP_Text>().text = transform.GetChild(0).GetComponent<TMP_Text>().text;
        tempButton.transform.GetChild(1).GetComponent<TMP_Text>().text = transform.GetChild(1).GetComponent<TMP_Text>().text;
        tempButton.transform.GetChild(2).GetComponent<TMP_Text>().text = transform.GetChild(2).GetComponent<TMP_Text>().text;
    }

    public void OnDrag(PointerEventData eventData)
    {
        tempButton.gameObject.SetActive(true);
        tempButton.transform.position = eventData.position;
        tempButton.GetComponent<ButtonDrag>().OnDrag(eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        tempButton.gameObject.SetActive(false);
    }

    public void SetIsClickedForUse()
    {
        isClickedForUse = !isClickedForUse;
    }

    private void MakeExtraButton()
    {
        if (GameObject.FindWithTag("InventoryExtraButton") != null)
            Destroy(GameObject.FindWithTag("InventoryExtraButton").gameObject);

        GameObject extraButton = GameManager.Resource.Instantiate(extraButtonPrefab);
        extraButton.transform.SetParent(transform);
        extraButton.transform.localPosition = new Vector2(290f, -27f);
        extraButton.transform.GetChild(0).GetComponent<InventoryExtraButton>().SetParentButton(gameObject.transform);
        extraButton.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);

        GameObject useButton = extraButton.transform.Find("UseButton").gameObject;

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("¹«±â"))
        {
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "ÀåÂø";

            if (attacker.isHoldingWeapon)
                useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "ÀåÂø ÇØÁ¦";
        }

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("À½½Ä"))
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "¸Ô±â";
    }


}
