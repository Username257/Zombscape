using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryExtraButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform parentButton;
    PlayerHolder holder;
    void Start()
    {
        holder = GameObject.FindWithTag("Holder").GetComponent<PlayerHolder>();
    }

    public void SetParentButton(Transform go)
    {
        parentButton = go;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        WhenItsClicked();
        Destroy(gameObject);
    }

    public void WhenItsClicked()
    {
        string itemName = parentButton.transform.GetChild(0).GetComponent<TMP_Text>().text;

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("ÀåÂø"))
        {
            if (holder.isGrabingSomething)
                holder.ReleaseItem();
            GameManager.Inventory.HoldItem(itemName);
            parentButton.GetComponent<InventoryButton>().isClickedForUse = true;
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("ÀåÂø ÇØÁ¦"))
        {
            holder.ReleaseItem();
            parentButton.GetComponent<InventoryButton>().isClickedForUse = false;
        }

        if (transform.Find("useText").GetComponent<TMP_Text>().text.Equals("¸Ô±â"))
        {
            if (holder.isGrabingSomething)
                holder.ReleaseItem();
            GameManager.Inventory.HoldItem(itemName);
        }

        if (transform.Find("throwText"))
        {
            //¹ö¸®±â
        }

    }

}
