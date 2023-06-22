using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : Weapon
{
    public override void Start()
    {
        itemName = "��������";
        itemType = "����";
        description = "������ ƴ�� �� �Ӹ��� ���� ������ ������ ���� ���� �� �ִ� ������.";
    }
    public override void OnEnable()
    {
        damage = 3;
        speed = 2f;
        freezeTime = 1f;
        legSpeed = 1f;
        base.OnEnable();
    }
    public void AddInInventory()
    {
        inventoryUI.AddItem(GetComponent<CrowBar>());
    }
}
