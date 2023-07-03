using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonDrag : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2 originPos;
    Transform originParent;
    bool isOthersInventory;
    public bool isDropedToAnother;
    Inventory mineInventory;
    Inventory TargetInventory;
    ItemData itemData;
    public void Start()
    {
        originParent = transform.parent;
        mineInventory = transform.parent.parent.parent.parent.GetComponent<InventoryUI>().inventory;
    }

    public void GetItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = eventData.position;
        transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isDropedToAnother)
        {
            mineInventory.RemoveItem(itemData);
            TargetInventory.AddItem(itemData);
        }
        else
            eventData.position = originPos;

        isDropedToAnother = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OthersInventoryUI" || collision.tag == "InventoryUI")
        {
            Debug.Log("TriggerEntered");
            TargetInventory = collision.transform.parent.parent.parent.parent.GetComponent<InventoryUI>().inventory;
            isDropedToAnother = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originPos;
        transform.SetParent(originParent);
    }
}
