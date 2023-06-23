using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Button buttonPrefab;
    [SerializeField] RectTransform content;
    List<Item> items;
    List<int> itemsAmount;
    List<Button> buttons;

    public void Awake()
    {
        items = new List<Item>();
        buttons = new List<Button>();
        itemsAmount = new List<int>();
    }
    public void AddItem(Item item)
    {
        if (items.Contains(item))
        {
            int index = items.FindIndex(a => a == item);
            itemsAmount[index] += 1;
            buttons[index].transform.Find("nameText").GetComponent<TMP_Text>().text = $"{items[items.Count - 1].itemName} ({itemsAmount[itemsAmount.Count - 1]})";
        }
        else
        {
            items.Add(item);
            itemsAmount.Add(1);

            Button button = GameManager.Resource.Instantiate(buttonPrefab);
            button.transform.SetParent(content);
            button.transform.Find("nameText").GetComponent<TMP_Text>().text = $"{items[items.Count - 1].itemName} ({itemsAmount[itemsAmount.Count - 1]})";
            button.transform.Find("typeText").GetComponent<TMP_Text>().text = items[items.Count - 1].itemType;

            buttons.Add(button);
        }


    }

    public void RemoveButton(Item item)
    {
        int index = items.FindIndex(a => a == item);
        itemsAmount[index]--;

        if (itemsAmount[index] <= 0)
        {
            GameManager.Resource.Destroy(buttons[index]);
        }
        
    }
}
