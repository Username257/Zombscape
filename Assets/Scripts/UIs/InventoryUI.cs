using System;
using System.Collections;
using System.Collections.Generic;
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
    bool isGetAnim1;
    List<GameObject> buttons = new List<GameObject>();

    public void AddButton(ItemData item)
    {
        GameObject buttoninstance = MakeButtonInstance(item);
        buttons.Add(buttoninstance);

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
        GameObject button = FindButton(itemName);
        if (button != null)
        {
            button.transform.Find("amountText").GetComponent<TMP_Text>().text = $"({itemAmount})";
        }
    }

    public GameObject FindButton(string itemName)
    {
        GameObject whichButton = null;

        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].transform.Find("nameText").GetComponent<TMP_Text>().text == itemName)
                whichButton = buttons[i];
        }

        return whichButton;
    }

    public void RemoveButton(ItemData item)
    {
        GameObject button = FindButton(item.name);
        Destroy(button);
        buttons.Remove(button);
    }

    public void ContentBoxGrowUp()
    {
        content.sizeDelta += new Vector2(0, 30);
    }
}
