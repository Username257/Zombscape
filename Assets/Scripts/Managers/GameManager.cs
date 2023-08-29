using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject timeManagerPrefab;
    [SerializeField] GameObject endingManagerPrefab;

    [SerializeField] public bool initData;
    [SerializeField] public bool initResource;
    [SerializeField] public bool initPool;
    [SerializeField] public bool initUi;
    [SerializeField] public bool initInventory;
    [SerializeField] public bool initTime;
    [SerializeField] public bool initEnding;

    private static GameManager instance;
    private static DataManager dataManager;
    private static ResourceManager resourceManager;
    private static PoolManager poolManager;
    private static UIManager uiManager;
    private static PlayersInventory inventory;
    private static TimeManager timeManager;
    private static EndingManager endingManager;

    public static GameManager Instance { get { return instance; } }
    public static DataManager Data {  get { return dataManager; } }
    public static ResourceManager Resource { get { return resourceManager; } }
    public static PoolManager Pool { get { return poolManager; } }
    public static UIManager UI { get { return uiManager; } }
    public static PlayersInventory Inventory { get { return inventory; } }
    public static TimeManager Time { get { return timeManager; } }
    public static EndingManager Ending { get { return endingManager; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public static Canvas ui;

    private void InitManagers()
    {
        if (initData)
        {
            GameObject dataObj = new GameObject();
            dataObj.name = "DataManager";
            dataObj.transform.parent = transform;
            dataManager = dataObj.AddComponent<DataManager>();
        }

        if (initResource)
        {
            GameObject resourceObj = new GameObject();
            resourceObj.name = "ResourceManager";
            resourceObj.transform.parent = transform;
            resourceManager = resourceObj.AddComponent<ResourceManager>();
        }

        if (initPool)
        {
            GameObject poolObj = new GameObject();
            poolObj.name = "PoolManager";
            poolObj.transform.parent = transform;
            poolManager = poolObj.AddComponent<PoolManager>();
        }

        if (initUi)
        {
            GameObject uiObj = new GameObject();
            uiObj.name = "UIManager";
            uiObj.transform.parent = transform;
            uiManager = uiObj.AddComponent<UIManager>();
        }

        if (initInventory)
        {
            GameObject inventoryObj = new GameObject();
            inventoryObj.name = "Inventory";
            inventoryObj.transform.parent = transform;
            inventory = inventoryObj.AddComponent<PlayersInventory>();
        }

        if (initTime)
        {
            GameObject timeObj = resourceManager.Instantiate<GameObject>(timeManagerPrefab);
            timeObj.name = "TimeManager";
            timeObj.transform.parent = transform;
        }

        if (initEnding)
        {
            GameObject endingObj = resourceManager.Instantiate<GameObject>(endingManagerPrefab);
            endingObj.name = "EndingManager";
            endingObj.transform.parent = transform;
        }
    }
}