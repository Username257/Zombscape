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
                death = "���ָ�";
                break;
            case EndingManager.deathState.dByAttack:
                death = "������ ����";
                break;
            case EndingManager.deathState.dByBleed:
                death = "��������";
                break;
            case EndingManager.deathState.dByInfected:
                death = "����";
                break;
            case EndingManager.deathState.Alive:
                break;
        }

        message = $"{GameManager.Data.PlayerName}(��)�� ����Ͽ����ϴ�. \n�����ϼ� : {GameManager.Time.curDay} \n������� : {death}";

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
