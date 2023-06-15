using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRender : MonoBehaviour, IHideable
{
    public void Start()
    {
        
    }
    public void Hide()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void Show()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
