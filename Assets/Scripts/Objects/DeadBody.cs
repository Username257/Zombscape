using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : OtherObject
{
    private new void Start()
    {
        GetComponent<OthersInventory>().Init();

        base.Start();
        objName = "Ω√√º";
    }
}
