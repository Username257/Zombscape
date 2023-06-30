using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryExtraButton : MonoBehaviour, IPointerClickHandler
{
    PlayerHolder holder;
    void Start()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        WhenItsClicked();
        Destroy(gameObject);
    }

    public void WhenItsClicked()
    {
        GameObject parentButton = transform.parent.parent.gameObject;
        string itemName = parentButton.transform.GetChild(0).GetComponent<TMP_Text>().text;

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("ÀåÂø"))
        {
            GameManager.Inventory.HoldItem(itemName);
            transform.parent.parent.GetComponent<InventoryButton>().isClickedForUse = true;
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("ÀåÂø ÇØÁ¦"))
        {
            holder.ReleaseItem();
            transform.parent.parent.GetComponent<InventoryButton>().isClickedForUse = false;
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("¸Ô±â"))
        {
            //¸Ô±â
        }

        if (transform.Find("throwText"))
        {
            //¹ö¸®±â
        }

    }

}
