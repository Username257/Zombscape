using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIPopUp : PopUpUI
{
    private bool isAble;
    protected override void Awake()
    {
        base.Awake();
    }

    public void OnCloseButton()
    {
        if (isAble)
            Able();
        else
            Disable();
    }

    private void Disable()
    {
        transforms["Image"].gameObject.SetActive(false);
        transforms["ScrollView"].gameObject.SetActive(false);

        isAble = !isAble;
    }

    private void Able()
    {
        transforms["Image"].gameObject.SetActive(true);
        transforms["ScrollView"].gameObject.SetActive(true);

        isAble = !isAble;
    }
}
