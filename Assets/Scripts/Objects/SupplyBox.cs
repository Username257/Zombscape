using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : OtherObject
{
    private new void Start()
    {
        GetComponent<OthersInventory>().Init();

        base.Start();
        objName = "보급품 상자";
    }
}
