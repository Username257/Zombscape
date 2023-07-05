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
        MakeButtonPool();

        if (!isOther)
            inventory = GameManager.Inventory.GetComponent<PlayersInventory>();
        else
            othersInventory = GameManager.Inventory.GetComponent<PlayersInventory>();
    }

    public void MakeButtonPool()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject buttonInstance = MakeButtonInstance();
            buttonInstance.GetComponent<InventoryButton>().itemData = null;
            buttonInstance.gameObject.SetActive(false);
            buttonInstance.transform.SetParent(content);
            buttons[i] = buttonInstance;
        }

    }
    public void UpdateUI(Inventory inventory)
    {
        RemoveAll();

        if (inventory != null)
        {
            for (int i = 0; i < 15; i++)
            {
                if (inventory.itemList[i] == null)
                    continue;

                if (inventory.itemList[i].itemName == null)
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
            buttons[i].GetComponent<InventoryButton>().itemData = null;
        }
    }

    public void AddButton(ItemData item)
    {
        if (inventory != null)
        {
            int index = FindEmptySlot();

            if (index == -1)
                return;

            buttons[index].SetActive(true);

            buttons[index].transform.GetChild(0).GetComponent<TMP_Text>().text = $"{item.itemName}";
            buttons[index].transform.GetChild(2).GetComponent<TMP_Text>().text = $"{item.itemType}";

            float nameTextPos = buttons[index].transform.GetChild(0).GetComponent<RectTransform>().localPosition.x;
            float spacing = buttons[index].transform.GetChild(0).GetComponent<TMP_Text>().text.Length * 15f;
            buttons[index].transform.GetChild(1).GetComponent<RectTransform>().localPosition = new Vector3(nameTextPos + spacing, 0, 0);

            buttons[index].transform.GetChild(1).GetComponent<TMP_Text>().text = "";

            buttons[index].GetComponent<ButtonDrag>().itemData = item;
            buttons[index].GetComponent<InventoryButton>().itemData = item;
            buttons[index].GetComponent<ButtonDrag>().SetMineInventory(inventory);
            buttons[index].GetComponent<ButtonDrag>().SetTargetInventory(othersInventory);
        }
    }
    public int FindEmptySlot()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<InventoryButton>().itemData == null)
                return i;
        }
        return -1;
    }

    private GameObject MakeButtonInstance()
    {
        GameObject buttonInstance = GameManager.Resource.Instantiate<GameObject>(buttonPrefab);
        buttonInstance.transform.SetParent(content.transform, false);
        buttonInstance.transform.position = Vector3.zero;
        if (isGetAnim1)
            buttonInstance.GetComponent<Animator>().runtimeAnimatorController = anim1;
        else
            buttonInstance.GetComponent<Animator>().runtimeAnimatorController = anim2;
        isGetAnim1 = !isGetAnim1;
        
        return buttonInstance;
    }

    public void SetButtonsItemCount(string itemName, int itemAmount)
    {
        int index = FindButton(itemName);
        if (index != -1)
        {
            if (itemAmount <= 1)
            {
                buttons[index].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "";
                return;
            }
            buttons[index].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = $"({itemAmount})";
        }
    }

    public int FindButton(string itemName)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            
            if (buttons[i].GetComponent<InventoryButton>().itemData == null)
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
            buttons[index].gameObject.SetActive(false);
            buttons[index].GetComponent<InventoryButton>().itemData = null;
        }
        
    }

    public void ContentBoxGrowUp()
    {
        content.sizeDelta += new Vector2(0, 30);
    }
}
