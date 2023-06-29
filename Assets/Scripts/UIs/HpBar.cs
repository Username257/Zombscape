using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        GameManager.Data.OnCurLifeChanged += SetHpBar;
    }

    public void SetHpBar()
    {
        slider.value = GameManager.Data.CurLife;
    }
}
