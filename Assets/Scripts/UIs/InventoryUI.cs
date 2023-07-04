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
    [SerializeField] public GameObject[] buttons;
    int maxCapacity;

    public void Start()
    {
        buttons = new GameObject[15];

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
            for (int i = 0; i < inventory.itemList.Length; i++)
            {
                if (inventory.itemList[i] == null)
                    continue;

                AddButton(inventory.itemList[i]);
                SetButtonsItemCount(inventory.itemList[i].itemName, inventory.itemAmount[i]);
            }
        }
    }

    public void RemoveAll()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i]);
            buttons[i] = null;
        }
    }

    public void AddButton(ItemData item)
    {
        if (inventory != null)
        {
            GameObject buttonInstance = MakeButtonInstance(item);
            buttonInstance.GetComponent<ButtonDrag>().itemData = item;
            buttonInstance.GetComponent<InventoryButton>().itemData = item;
            buttonInstance.GetComponent<ButtonDrag>().SetMineInventory(inventory);
            buttonInstance.GetComponent<ButtonDrag>().SetTargetInventory(othersInventory);

            int index = FindEmptySlot();
            if (index != -1)
                buttons[index] = buttonInstance;

            if (index == -1)
                ContentBoxGrowUp();
        }
    }
    public int FindEmptySlot()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null)
                return i;
        }
        return -1;
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

        buttonInstance.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{item.itemName}";
        buttonInstance.transform.GetChild(2).GetComponent<TMP_Text>().text = $"{item.itemType}";

        int spacing = item.itemName.Length * 8;
        buttonInstance.transform.GetChild(1).GetComponent<RectTransform>().localPosition += new Vector3(spacing, 0, 0);

        buttonInstance.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
        
        return buttonInstance;
    }

    public void SetButtonsItemCount(string itemName, int itemAmount)
    {
        int index = FindButton(itemName);
        if (index != -1)
        {
            if (itemAmount <= 1)
                return;

            buttons[index].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = $"({itemAmount})";
        }
    }

    public int FindButton(string itemName)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null)
                continue;

            if (buttons[i].transform.GetChild(0).GetComponent<TMP_Text>().text == itemName)
                return i;
        }
        return -1;
    }

    public void RemoveButton(ItemData item)
    {
        int index = FindButton(item.itemName);
        if (index != -1)
        {
            Destroy(buttons[index].gameObject);
            buttons[index] = null;
        }
        
    }

    public void ContentBoxGrowUp()
    {
        content.sizeDelta += new Vector2(0, 30);
    }
}
