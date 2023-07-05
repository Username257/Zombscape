using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject extraButtonPrefab;
    string itemName;
    public ItemData itemData;
    public bool isClickedForUse;
    public bool isOthersInventoryUI;

    public void Start()
    {
        itemName = transform.GetChild(0).GetComponent<TMP_Text>().text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOthersInventoryUI)
            return;
        MakeExtraButton();
    }

    public void SetIsClickedForUse()
    {
        isClickedForUse = !isClickedForUse;
    }

    private void MakeExtraButton()
    {
        if (GameObject.FindWithTag("InventoryExtraButton") != null)
            Destroy(GameObject.FindWithTag("InventoryExtraButton").gameObject);

        GameObject extraButton = GameManager.Resource.Instantiate(extraButtonPrefab);
        extraButton.transform.SetParent(transform);
        extraButton.transform.localPosition = new Vector2(290f, -27f);
        extraButton.transform.GetChild(0).GetComponent<InventoryExtraButton>().SetParentButton(gameObject.transform);
        extraButton.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);

        GameObject useButton = extraButton.transform.Find("UseButton").gameObject;

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("¹«±â"))
        {
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "ÀåÂø";

            if (isClickedForUse)
            {
                GameObject.Find("useText").GetComponent<TMP_Text>().text = "ÀåÂø ÇØÁ¦";
            }
        }

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("À½½Ä"))
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "¸Ô±â";
    }

}
