using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] RectTransform content;
    [SerializeField] RuntimeAnimatorController anim1;
    [SerializeField] RuntimeAnimatorController anim2;
    [SerializeField] bool isOther = true;

    public Inventory inventory;
    public Inventory othersInventory;
    bool isGetAnim1;
    [SerializeField] public List<GameObject> buttons = new List<GameObject>();

    public void Start()
    {
        if (!isOther)
            inventory = GameManager.Inventory.GetComponent<PlayersInventory>();
        else
            othersInventory = GameManager.Inventory.GetComponent<PlayersInventory>();
    }

    public void UpdateUI(Inventory inventory)
    {
        RemoveAll();

        if (inventory != null)
        {
            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                AddButton(inventory.itemList[i]);
                SetButtonsItemCount(inventory.itemList[i].itemName, inventory.itemAmount[i]);
            }
        }
    }

    public void RemoveAll()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
            buttons.Remove(buttons[i]);
        }
    }

    public void AddButton(ItemData item)
    {
        GameObject buttonInstance = MakeButtonInstance(item);
        buttonInstance.GetComponent<ButtonDrag>().itemData = item;
        buttonInstance.GetComponent<ButtonDrag>().SetMineInventory(inventory);
        if (buttonInstance.GetComponent<ButtonDrag>().targetInventory == null)
            buttonInstance.GetComponent<ButtonDrag>().SetTargetInventory(othersInventory);
        buttons.Add(buttonInstance);

        if (buttons.Count > 8)
            ContentBoxGrowUp();
    }

    private GameObject MakeButtonInstance(ItemData item)
    {
        GameObject buttonInstance = GameManager.Resource.Instantiate<GameObject>(buttonPrefab);
        buttonInstance.transform.SetParent(content.transform, false);
        buttonInstance.transform.position = Vector3.zero;
        if (isGetAnim1)
            buttonInstance.GetComponent<Animator>().runtimeAnimatorController = anim1;
        else
            buttonInstance.GetComponent<Animator>().runtimeAnimatorController = anim2;
        isGetAnim1 = !isGetAnim1;

        buttonInstance.transform.Find("nameText").GetComponent<TMP_Text>().text = $"{item.itemName}";
        buttonInstance.transform.Find("typeText").GetComponent<TMP_Text>().text = $"{item.itemType}";

        int spacing = item.itemName.Length * 8;
        buttonInstance.transform.Find("amountText").GetComponent<RectTransform>().localPosition += new Vector3(spacing, 0, 0);

        buttonInstance.transform.Find("amountText").GetComponent<TMP_Text>().text = "";
        return buttonInstance;
    }

    public void SetButtonsItemCount(string itemName, int itemAmount)
    {
        int index = FindButton(itemName);
        if (index != -1)
        {
            buttons[index].transform.Find("amountText").gameObject.GetComponent<TMP_Text>().text = $"({itemAmount})";
        }
    }

    public int FindButton(string itemName)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].transform.Find("nameText").GetComponent<TMP_Text>().text == itemName)
                return i;
        }
        return -1;
    }

    public void RemoveButton(ItemData item)
    {
        int index = FindButton(item.name);
        if (index != -1)
        {
            buttons.Remove(buttons[index]);
            Destroy(buttons[index].gameObject);
        }
        
    }

    public void ContentBoxGrowUp()
    {
        content.sizeDelta += new Vector2(0, 30);
    }
}
