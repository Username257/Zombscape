using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    
    [SerializeField] private int curLife;
    public int CurLife
    {
        get { return curLife; }
        set
        {
            curLife = value;
            OnCurLifeChanged?.Invoke(curLife);
        }
    }

    public event UnityAction<int> OnCurLifeChanged;
}
