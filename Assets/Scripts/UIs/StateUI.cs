using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : PopUpUI
{
    public bool isAble = true;
    public bool isState;
    protected override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        Able();
        transforms["State"].gameObject.SetActive(true);
        transforms["Info"].gameObject.SetActive(false);
    }

    public void OnStateButton()
    {
        transforms["State"].gameObject.SetActive(true);
        transforms["Info"].gameObject.SetActive(false);
    }
    public void OnInfoButton()
    {
        transforms["State"].gameObject.SetActive(false);
        transforms["Info"].gameObject.SetActive(true);
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
        transforms["Buttons"].gameObject.SetActive(false);
        transforms["UIBaseImage"].gameObject.SetActive(false);
        transforms["State"].gameObject.SetActive(false);
        transforms["Info"].gameObject.SetActive(false);

        isAble = !isAble;
    }

    private void Able()
    {
        transforms["Buttons"].gameObject.SetActive(true);
        transforms["UIBaseImage"].gameObject.SetActive(true);
        transforms["State"].gameObject.SetActive(true);
        transforms["Info"].gameObject.SetActive(true);

        isAble = !isAble;
    }
}
