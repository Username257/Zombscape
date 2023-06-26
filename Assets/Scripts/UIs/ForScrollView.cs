using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ForScrollView : MonoBehaviour
{
    static ScrollRect scrollRect;
    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    public static void ScrollToTop()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    public static void ScrollToBottom()
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
