using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryExtraButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        WhenItsClicked();
    }

    public void WhenItsClicked()
    {
        GameObject parentButton = transform.parent.parent.gameObject;
        string itemName = parentButton.transform.GetChild(0).GetComponent<TMP_Text>().text;

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("����"))
        {
            GameManager.Inventory.HoldItem(itemName);
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("�Ա�"))
        {
            //�Ա�
        }

        if (transform.Find("throwText"))
        {
            //������
        }

    }

}
