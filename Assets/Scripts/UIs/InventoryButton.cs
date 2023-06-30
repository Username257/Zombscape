using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject extraButtonPrefab;
    public bool isClickedForUse;

    public void OnPointerClick(PointerEventData eventData)
    {
        MakeExtraButton();
    }

    public void SetIsClickedForUse()
    {
        isClickedForUse = !isClickedForUse;
    }

    private void MakeExtraButton()
    {
        GameObject extraButton = GameManager.Resource.Instantiate(extraButtonPrefab);
        extraButton.transform.SetParent(transform);
        extraButton.transform.localPosition = new Vector2(290f, -27f);

        GameObject useButton = extraButton.transform.Find("UseButton").gameObject;

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("����"))
        {
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "����";

            if (isClickedForUse)
            {
                GameObject.Find("useText").GetComponent<TMP_Text>().text = "���� ����";
            }
        }

        if (transform.Find("typeText").GetComponent<TMP_Text>().text.Equals("����"))
            useButton.transform.Find("useText").GetComponent<TMP_Text>().text = "�Ա�";
    }

}
