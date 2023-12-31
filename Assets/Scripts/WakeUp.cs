using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.initData = true;
        GameManager.Instance.initResource = true;
        GameManager.Instance.initPool = true;
        GameManager.Instance.initUi = true;
        GameManager.Instance.initInventory = true;
        GameManager.Instance.initTime = true;
        GameManager.Instance.initEnding = true;
        GameManager.Instance.InitManagers();

        GameManager.UI.Init();
    }
}
