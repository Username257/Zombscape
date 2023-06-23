using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : Weapon
{
    public override void Awake()
    {
        itemName = "��������";
        itemType = "����";
        description = "������ ƴ�� �� �Ӹ��� ���� ������ ������ ���� ���� �� �ִ� ������.";
    }
    public override void HoldWeapon()
    {
        damage = 3;
        speed = 2f;
        freezeTime = 1f;
        legSpeed = 1f;
        base.HoldWeapon();
    }
    public void AddInInventory()
    {
        inventoryUI.AddItem(this);
    }
}
