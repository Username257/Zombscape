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
    private static GameManager instance;
    private static DataManager dataManager;
    private static ResourceManager resourceManager;
    private static PoolManager poolManager;
    private static UIManager uiManager;
    private static Item item;

    public static GameManager Instance { get { return instance; } }
    public static DataManager Data {  get { return dataManager; } }
    public static ResourceManager Resource { get { return resourceManager; } }
    public static PoolManager Pool { get { return poolManager; } }
    public static UIManager UI { get { return uiManager; } }
    public static Item Item { get { return item; } }

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
        GameObject dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform;
        dataManager = dataObj.AddComponent<DataManager>();

        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        resourceManager = resourceObj.AddComponent<ResourceManager>();

        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        poolManager = poolObj.AddComponent<PoolManager>();

        GameObject uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform;
        uiManager = uiObj.AddComponent<UIManager>();

        GameObject itemObj = new GameObject();
        itemObj.name = "Item";
        itemObj.transform.parent = transform;
        item = itemObj.AddComponent<Item>();
    }
}