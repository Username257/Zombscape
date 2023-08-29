using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    [SerializeField] public TMP_Text timeText;
    [SerializeField] public TMP_Text dayText;

    [SerializeField] public GameObject directionalLight;
    [SerializeField] List<Color> skyColorList = new List<Color>();
    int skyColorIndex = 0;

    [SerializeField] float lerpSpeed;

    [SerializeField] int startHour;
    [SerializeField] int startMinute;

    [SerializeField] public int curHour;
    [SerializeField] public int curMinute;

    public int curDay = 0;

    [SerializeField] float timer;


    public void Start()
    {
        curHour = startHour;
        curMinute = startMinute;

        SetTimeText();

        if (curHour >= 22 || curHour <= 2)
            directionalLight.GetComponent<Light>().color = skyColorList[5];
        else if (curHour >= 3 || curHour <= 5)
            directionalLight.GetComponent<Light>().color = skyColorList[0];
        else if (curHour >= 6 || curHour <= 11)
            directionalLight.GetComponent<Light>().color = skyColorList[1];
        else if (curHour >= 12 || curHour <= 17)
            directionalLight.GetComponent<Light>().color = skyColorList[2];
        else if (curHour >= 18 || curHour <= 19)
            directionalLight.GetComponent<Light>().color = skyColorList[3];
        else
            directionalLight.GetComponent<Light>().color = skyColorList[4];
    }

    public void Update()
    {
        CountTimeByRealTime();
    }

    Coroutine directionalLightRoutine;
    public void CountTimeByRealTime()
    {
        timer += Time.deltaTime;

        if (timer > 2.3f)
        {
            curMinute++;
            timer = 0;
            SetTimeText();
        }

        if (curMinute >= 60)
        {
            curHour++;
            curMinute = 0;
            SetTimeText();
        }

        if (curHour >= 24)
        {
            curDay++;
            curHour = 0;
            SetTimeText();
            SetDayText();
        }


        switch (curHour)
        {
            case 3:
                if (curMinute == 0 && timer == 0)
                {
                    skyColorIndex = 0;
                    directionalLightRoutine = StartCoroutine(ChangeDirectionalLight());
                }
                break;
            case 6:
                if (curMinute == 0 && timer == 0)
                {
                    skyColorIndex = 1;
                    directionalLightRoutine = StartCoroutine(ChangeDirectionalLight());
                }
                break;
            case 11:
                if (curMinute == 0 && timer == 0)
                {
                    skyColorIndex = 2;
                    directionalLightRoutine = StartCoroutine(ChangeDirectionalLight());
                }
                break;
            case 17:
                if (curMinute == 0 && timer == 0)
                {
                    skyColorIndex = 3;
                    directionalLightRoutine = StartCoroutine(ChangeDirectionalLight());
                }
                break;
            case 19:
                if (curMinute == 0 && timer == 0)
                {
                    skyColorIndex = 4;
                    directionalLightRoutine = StartCoroutine(ChangeDirectionalLight());
                }
                break;
            case 22:
                if (curMinute == 0 && timer == 0)
                {
                    skyColorIndex = 5;
                    directionalLightRoutine = StartCoroutine(ChangeDirectionalLight());
                }
                break;
            default:
                break;
        }
    }

    public void SetDayText()
    {
        dayText.SetText($"{curDay}일째");
    }

    public void SetTimeText()
    {
        string curHourStr;
        string curMinuteStr;

        if (curHour < 10)
            curHourStr = $"0{curHour}";
        else
            curHourStr = curHour.ToString();

        if (curMinute < 10)
            curMinuteStr = $"0{curMinute}";
        else
            curMinuteStr = curMinute.ToString();

        timeText.SetText($"{curHourStr} : {curMinuteStr}");
    }

    IEnumerator ChangeDirectionalLight()
    {
        float tick = 0;

        if (skyColorIndex == 0) //새벽(0번 째)일 때 밤(5번 째)에서 시작
        {
            directionalLight.GetComponent<Light>().color = skyColorList[skyColorList.Count - 1];

            while (tick <= 1)
            {
                tick += Time.deltaTime / lerpSpeed;
                
                directionalLight.GetComponent<Light>().color = Color.Lerp(skyColorList[skyColorList.Count - 1], skyColorList[0], tick);
                yield return new WaitForEndOfFrame();
            }
            directionalLight.GetComponent<Light>().color = skyColorList[0];
        }
        else
        {
            directionalLight.GetComponent<Light>().color = skyColorList[skyColorIndex - 1];

            while (tick <= 1)
            {
                tick += Time.deltaTime / lerpSpeed;

                directionalLight.GetComponent<Light>().color = Color.Lerp(skyColorList[skyColorIndex - 1], skyColorList[skyColorIndex], tick);
                yield return new WaitForEndOfFrame();
            }
            directionalLight.GetComponent<Light>().color = skyColorList[skyColorIndex];
        }

    }
}
