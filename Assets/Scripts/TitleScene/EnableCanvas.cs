using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    public void enableCanvas()
    {
        canvas.SetActive(true);
    }
}
