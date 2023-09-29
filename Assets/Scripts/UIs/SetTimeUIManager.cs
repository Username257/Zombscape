using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTimeUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text dayText;

    public void Init()
    {
        TimeManager timeManager = GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>();

        timeManager.timeText = timeText;
        timeManager.dayText = dayText;
        timeManager.directionalLight = GameObject.FindWithTag("DirectionalLight");
    }
}
