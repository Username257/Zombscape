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
    public bool isEnterAnother;
    [SerializeField] Inventory mineInventory;
    [SerializeField] Inventory TargetInventory;
    [SerializeField] public ItemData itemData;
    public void Start()
    {
        originParent = transform.parent;
        mineInventory = transform.parent.parent.parent.parent.GetComponent<InventoryUI>().inventory;

        if (mineInventory.gameObject.GetComponent<OthersInventory>() != null)
             TargetInventory = GameObject.FindWithTag("InventoryUI").gameObject.GetComponent<InventoryUI>().inventory;
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
        if (isEnterAnother)
        {
            if (TargetInventory != null)
            {
                TargetInventory.AddItem(itemData);
                mineInventory.RemoveItem(itemData);
            }
        }
        else
            eventData.position = originPos;

        isEnterAnother = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OthersInventoryUI" || collision.tag == "InventoryUI")
        {
            isEnterAnother = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isEnterAnother)
        {
            transform.position = originPos;
            transform.SetParent(originParent);
        }
        else
            Destroy(gameObject);
    }
}
