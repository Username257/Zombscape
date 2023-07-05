using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class ButtonDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2 originPos;
    Transform originParent;
    [SerializeField] Inventory mineInventory;
    [SerializeField] public Inventory targetInventory;
    [SerializeField] public ItemData itemData;
    public void Start()
    {
        originParent = transform.parent;
    }

    public void SetMineInventory(Inventory mine)
    {
        mineInventory = mine;
        if (mineInventory.GetComponent<PlayersInventory>() == null)
            targetInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    public void SetTargetInventory(Inventory target)
    {
        targetInventory = target;
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
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originParent);
        transform.localPosition = originPos;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == null || targetInventory == null)
            return;

        if (other.tag == targetInventory.inventoryUI.tag)
        {
            targetInventory.AddItem(itemData);
            mineInventory.RemoveItem(itemData);
        }
        
    }

}
