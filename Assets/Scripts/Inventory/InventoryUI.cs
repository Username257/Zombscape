using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryButton buttonPrefab;
    [SerializeField] RectTransform content;
    public List<GameObject> items;
    List<int> itemsAmount;
    List<InventoryButton> buttons;

    public void Awake()
    {
        items = new List<GameObject>();
        buttons = new List<InventoryButton>();
        itemsAmount = new List<int>();
    }


    public void AddItem(GameObject item)
    {
        //if (item.gameObject.scene.name != null) Debug.Log("AddItem: It's a gameObject!");
        //if (item.gameObject.scene.name == null) Debug.Log("AddItem: It's a prefab!");

        if (items.Contains(item))
        {
            int index = items.FindIndex(a => a == item);

            itemsAmount[index] += 1;
            buttons[index].transform.Find("nameText").GetComponent<TMP_Text>().text = $"{items[items.Count - 1].GetComponent<Item>().itemName} ({itemsAmount[itemsAmount.Count - 1]})";
            
        }
        else
        {
            items.Add(item);
            itemsAmount.Add(1);

            InventoryButton button = GameManager.Resource.Instantiate(buttonPrefab);
            button.transform.SetParent(content);
            
            button.transform.Find("nameText").GetComponent<TMP_Text>().text = $"{items[items.Count - 1].GetComponent<Item>().itemName} ({itemsAmount[itemsAmount.Count - 1]})";
            button.transform.Find("typeText").GetComponent<TMP_Text>().text = items[items.Count - 1].GetComponent<Item>().itemType;

            buttons.Add(button);

        }

    }

    public void RemoveItem(GameObject item)
    {
        //if (item.gameObject.scene.name == null) Debug.Log("RemoveItem: It's a prefab!");
        //if (item.gameObject.scene.name != null) Debug.Log("RemoveItem: It's a gameObject!");

        int index = items.FindIndex(a => a == item);

        itemsAmount[index]--;

        if (itemsAmount[index] <= 0)
        {
            GameManager.Resource.Destroy(buttons[index].gameObject);
        }
     
    }

    public void ContentBoxGrowUp()
    {
        if (itemsAmount.Count > 8)
        {
            content.sizeDelta += new Vector2(0, 30);
        }
    }
}
