using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : PopUpUI
{
    private bool isAble;
    protected override void Awake()
    {
        base.Awake();

        //buttons["CloseButton"].onClick.AddListener(() => { if (isAble) Able(); else Disable(); });;
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
        buttons["HPButton"].gameObject.SetActive(false);
        buttons["InfoButton"].gameObject.SetActive(false);
        transforms["UIBaseImage"].gameObject.SetActive(false);
        transforms["BodyInfoTextBox"].gameObject.SetActive(false);
        transforms["HpBar"].gameObject.SetActive(false);
        transforms["BodyUI"].gameObject.SetActive(false);

        isAble = !isAble;
    }

    private void Able()
    {
        buttons["HPButton"].gameObject.SetActive(true);
        buttons["InfoButton"].gameObject.SetActive(true);
        transforms["UIBaseImage"].gameObject.SetActive(true);
        transforms["BodyInfoTextBox"].gameObject.SetActive(true);
        transforms["HpBar"].gameObject.SetActive(true);
        transforms["BodyUI"].gameObject.SetActive(true);

        isAble = !isAble;
    }
}
