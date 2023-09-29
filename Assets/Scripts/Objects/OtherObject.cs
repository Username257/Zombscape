using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class OtherObject : MonoBehaviour, IFarmingable
{
    [SerializeField] protected InventoryUI otherInventoryUI;
    RandomInventoryGenerator generator;
    public InventoryUI OtherInventoryUI { get { return otherInventoryUI; } set { otherInventoryUI = value; } }
    [SerializeField] protected Inventory inventory;
    public bool canShowInventory = true;

    [SerializeField] public string objName;

    public void Start()
    {
        StartCoroutine(FindRoutine());
    }
    IEnumerator FindRoutine()
    {
        yield return new WaitUntil(() => { return GameObject.FindWithTag("OthersInventoryUI"); });

        Init();

        yield break;
    }

    public void Init()
    {

        inventory = gameObject.AddComponent<OthersInventory>();
        otherInventoryUI = GameObject.FindWithTag("OthersInventoryUI").GetComponent<InventoryUI>();

        GetComponent<OthersInventory>().Init();
        InventoryGenerate();
    }

    public void InventoryGenerate()
    {
        generator = GameObject.FindWithTag("RandomInventoryGenerator").GetComponent<RandomInventoryGenerator>();
        generator.SetInventory(inventory);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canShowInventory)
        {
            ShowMyInventory();
            GameManager.Inventory.inventoryUI.othersInventory = inventory;
            otherInventoryUI.SetObjName($"{objName}의 인벤토리");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (otherInventoryUI != null)
        {
            otherInventoryUI.RemoveAll();
            otherInventoryUI.inventory = null;
            otherInventoryUI.SetObjName(" ");
        }
        
    }

    public void ShowMyInventory()
    {
        otherInventoryUI.inventory = inventory;
        otherInventoryUI.UpdateUI(inventory);
        GameManager.Inventory.inventoryUI.othersInventory = inventory;
    }

}
