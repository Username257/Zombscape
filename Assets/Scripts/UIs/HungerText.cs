using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HungerText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private void Start()
    {
        GameManager.Data.OnCurHungerChanged += SetHungerText;
    }
    public void SetHungerText()
    {
        text.SetText(GameManager.Data.CurHunger + "%".ToString());
    }
}
