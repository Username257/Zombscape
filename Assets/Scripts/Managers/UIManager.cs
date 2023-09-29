using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private Canvas popUpCanvas;
    private Stack<PopUpUI> popUpStack;

    private Canvas windowCanvas;

    [SerializeField] private GameObject uiCanvas;


    public void Init()
    {
        //eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");

        uiCanvas = GameManager.Resource.Instantiate<GameObject>("UI/UICanvas");
        uiCanvas.transform.SetParent(GameObject.FindWithTag("RootCanvas").transform);
        uiCanvas.gameObject.name = "UICanvas";
        uiCanvas.GetComponent<Canvas>().sortingOrder = 100;

        uiCanvas.transform.GetChild(1).GetComponent<InventoryUI>().Init();
        uiCanvas.transform.GetChild(2).GetComponent<InventoryUI>().Init();
        GameManager.Inventory.Init();

    }

    public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
    {
        if (popUpStack.Count > 0)
        {
            PopUpUI prevUI = popUpStack.Peek();
            prevUI.gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI<T>(popUpUI);
        ui.transform.SetParent(popUpCanvas.transform, false);
        popUpStack.Push(ui);

        Time.timeScale = 0f;
        return ui;
    }

    public T ShowPopUpUI<T>(string path) where T : PopUpUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowPopUpUI(ui);
    }

    public void ClosePopUpUI()
    {
        PopUpUI ui = popUpStack.Pop();
        GameManager.Pool.ReleaseUI(ui.gameObject);

        if (popUpStack.Count > 0)
        {
            PopUpUI curUI = popUpStack.Peek();
            curUI.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ClearPopUpUI()
    {
        while (popUpStack.Count > 0)
        {
            ClosePopUpUI();
        }
    }


    public T ShowWindowUI<T>(T windowUI) where T : WindowUI
    {
        T ui = GameManager.Pool.GetUI(windowUI);
        ui.transform.SetParent(windowCanvas.transform, false);
        return ui;
    }

    public T ShowWindowUI<T>(string path) where T : WindowUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowWindowUI(ui);
    }

    public void SelectWindowUI<T>(T windowUI) where T : WindowUI
    {
        windowUI.transform.SetAsLastSibling();
    }

    public void CloseWindowUI<T>(T windowUI) where T : WindowUI
    {
        GameManager.Pool.ReleaseUI(windowUI.gameObject);
    }

    public void ClearWindowUI()
    {
        WindowUI[] windows = windowCanvas.GetComponentsInChildren<WindowUI>();

        foreach (WindowUI windowUI in windows)
        {
            GameManager.Pool.ReleaseUI(windowUI.gameObject);
        }
    }
}
