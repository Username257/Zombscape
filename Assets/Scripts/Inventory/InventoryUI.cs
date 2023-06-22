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

    public void AddItem(Item item)
    {
        if (items.Contains(item))
        {
            int index = items.FindIndex(a => a == item);
            itemsAmount[index]++;
        }
        else
        {
            items.Add(item);

            Button button = GameManager.Resource.Instantiate(buttonPrefab);
            button.transform.SetParent(content);
            button.transform.Find("nameText").GetComponent<TMP_Text>().text = items[items.Count].itemName;
            button.transform.Find("typeText").GetComponent<TMP_Text>().text = items[items.Count].itemType;

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
