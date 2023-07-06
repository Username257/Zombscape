using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class ButtonDrag : MonoBehaviour, IDragHandler
{
    public Vector2 originPos;
    [SerializeField] public Inventory mineInventory;
    [SerializeField] public Inventory targetInventory;
    [SerializeField] public ItemData itemData;
    public InventoryButton button;
    public bool isUsed;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == null || targetInventory == null)
            return;

        if (other.tag == targetInventory.inventoryUI.tag && !isUsed)
        {
            targetInventory.AddItem(itemData);
            mineInventory.RemoveItem(itemData);
            gameObject.SetActive(false);
            isUsed = true;
        }
    }

}
