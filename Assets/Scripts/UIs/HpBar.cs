using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        GameManager.Data.OnCurLifeChanged += SetHpBar;
    }
    private void OnDisable()
    {
        GameManager.Data.OnCurLifeChanged -= SetHpBar;
    }

    public void SetHpBar(int curLife)
    {
        slider.value = curLife;
    }


}
