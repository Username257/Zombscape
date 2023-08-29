using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class BodyInfoTextBox : MonoBehaviour
{
    [SerializeField] PlayerGetInjured playerGetInjured;
    PlayerEater eater;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] RectTransform Box;
    string text;
    GameObject[] texts;
    Body[] textsBody;
    private int pivot = 0;
    private Body curBody;
    enum Body { Neck, RArm, LArm, RLeg, LLeg, None }

    private void Start()
    {
        texts = new GameObject[5];
        textsBody = new Body[5];
        for (int i = 0; i < 5; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(Box);
            texts[i] = button;
            textsBody[i] = Body.None;
            button.SetActive(false);
        }
    }



    public void MakeButtonGetText()
    {
        GetPivot();

        texts[pivot].GetComponentInChildren<TMP_Text>().text = text;
        if (text == "")
            texts[pivot].SetActive(false);
        texts[pivot].SetActive(true);
        textsBody[pivot] = curBody;
    }

    private void GetPivot()
    {
        for (int i = 0; i < 5; i++)
        {
            if (textsBody[i] == curBody)
            {
                pivot = i;
                return;
            }

            if (textsBody[i] == Body.None)
            {
                pivot = i;
                return;
            }

            
        }


    }

    private void OnEnable()
    {
        playerGetInjured = GameObject.FindWithTag("Player").GetComponent<PlayerGetInjured>();
        eater = GameObject.FindWithTag("Player").GetComponent<PlayerEater>();

        playerGetInjured.OnGetInjured += GetText;
        playerGetInjured.OnGetBited += GetText;
        eater.OnHealed += GetText;
    }

    private void OnDisable()
    {
        playerGetInjured.OnGetInjured -= GetText;
        playerGetInjured.OnGetBited -= GetText;
        eater.OnHealed -= GetText;
    }


    public void GetText(int body)
    {

        if (body == 0)
        {
            curBody = Body.Neck;

            //목
            if (GameManager.Data.Neck == DataManager.State.Scar)
            {
                
                text = "목에 상처가 남";
            }

            if (GameManager.Data.Neck == DataManager.State.Bleeding)
                text = "목에서 피가 흐름";

            if (GameManager.Data.Neck == DataManager.State.Bited)
            {
                text = "목이 물림";
            }

            if (GameManager.Data.Neck == DataManager.State.None)
                text = "";
        }

        if (body == 1)
        {
            curBody = Body.RArm;

            //오른팔
            if (GameManager.Data.RArm == DataManager.State.Scar)
            {
               
                text = "오른팔에 상처가 남";
            }

            if (GameManager.Data.RArm == DataManager.State.Bleeding)
                text = "오른팔에서 피가 흐름";

            if (GameManager.Data.RArm == DataManager.State.Bited)
            {
                text = "오른팔이 물림";
               
            }

            if (GameManager.Data.RArm == DataManager.State.None)
                text = "";

        }


        if (body == 2)
        {
            curBody = Body.LArm;

            //왼팔
            if (GameManager.Data.LArm == DataManager.State.Scar)
            {
              
                text = "왼팔에 상처가 남";
            }

            if (GameManager.Data.LArm == DataManager.State.Bleeding)
                text = "왼팔에서 피가 흐름";

            if (GameManager.Data.LArm == DataManager.State.Bited)
            {

                text = "왼팔이 물림";
            }
            if (GameManager.Data.LArm == DataManager.State.None)
                text = "";
        }
        

        if (body == 3)
        {
            curBody = Body.RLeg;

            //오른다리
            if (GameManager.Data.RLeg == DataManager.State.Scar)
                text = "오른다리에 상처가 남";

            if (GameManager.Data.RLeg == DataManager.State.Bleeding)
                text = "오른다리에서 피가 흐름";

            if (GameManager.Data.RLeg == DataManager.State.Bited)
            {
                text = "오른다리가 물림";
            }
            if (GameManager.Data.RLeg == DataManager.State.None)
                text = "";

        }


        if (body == 4)
        {
            curBody = Body.LLeg;

            //왼다리
            if (GameManager.Data.LLeg == DataManager.State.Scar)
            {
                text = "왼다리에 상처가 남";
            }
                

            if (GameManager.Data.LLeg == DataManager.State.Bleeding)
                text = "왼다리에서 피가 흐름";

            if (GameManager.Data.LLeg == DataManager.State.Bited)
            {
                text = "왼다리가 물림";
            }
            if (GameManager.Data.LLeg == DataManager.State.None)
                text = "";

        }

        MakeButtonGetText();

    }
}
