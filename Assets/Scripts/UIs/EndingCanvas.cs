using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject blackScreen;
    [SerializeField] Button title;
    [SerializeField] float typeSpeed;
    string message;
    string death;
    Coroutine typeRoutine;
    EndingManager endingManager;
    TimeManager timeManager;

    public void Start()
    {
        blackScreen.SetActive(false);
        text.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        endingManager = GameObject.FindWithTag("EndingManager").GetComponent<EndingManager>();
        timeManager = GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>();
    }

    public void SetEndingCanvas()
    {
        blackScreen.SetActive(true);
        text.gameObject.SetActive(true);
        title.gameObject.SetActive(true);

        switch (endingManager.curState)
        {
            case EndingManager.deathState.dByHunger:
                death = "굶주림";
                break;
            case EndingManager.deathState.dByAttack:
                death = "좀비의 공격";
                break;
            case EndingManager.deathState.dByBleed:
                death = "과다출혈";
                break;
            case EndingManager.deathState.dByInfected:
                death = "감염";
                break;
            case EndingManager.deathState.Alive:
                break;
        }
        GameObject.FindWithTag("UICanvas").gameObject.SetActive(false);
        message = $"{GameManager.Data.PlayerName}(이)가 사망하였습니다. \n생존일수 : {timeManager.curDay} \n사망원인 : {death}";

        blackScreen.GetComponent<Animator>().SetTrigger("start");
        typeRoutine = StartCoroutine(Typing());
    }

    public void Change()
    {
        blackScreen.SetActive(false);
        text.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        SceneManager.LoadScene("Title");
    }

    IEnumerator Typing()
    {
        for (int i = 0; i < message.Length; i++)
        {
            text.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
