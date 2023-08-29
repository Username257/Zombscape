using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject blackScreen;
    [SerializeField] float typeSpeed;
    string message;
    string death;
    Coroutine typeRoutine;

    public void Start()
    {
        blackScreen.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void SetEndingCanvas()
    {
        blackScreen.SetActive(true);
        text.gameObject.SetActive(true);

        switch (GameManager.Ending.curState)
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

        message = $"{GameManager.Data.PlayerName}(이)가 사망하였습니다. \n생존일수 : {GameManager.Time.curDay} \n사망원인 : {death}";

        blackScreen.GetComponent<Animator>().SetTrigger("start");
        typeRoutine = StartCoroutine(Typing());
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
