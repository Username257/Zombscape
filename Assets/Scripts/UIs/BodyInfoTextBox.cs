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

        playerGetInjured.OnGetInjured += GetText;
        playerGetInjured.OnGetBited += GetText;
    }

    private void OnDisable()
    {
        playerGetInjured.OnGetInjured -= GetText;
        playerGetInjured.OnGetBited -= GetText;
    }


    public void GetText(int body)
    {

        if (body == 0)
        {
            curBody = Body.Neck;

            //��
            if (GameManager.Data.Neck == DataManager.State.Scar)
            {
                
                text = "�� ��ó�� ��";
            }

            if (GameManager.Data.Neck == DataManager.State.Bleeding)
                text = "�񿡼� �ǰ� �帧";

            if (GameManager.Data.Neck == DataManager.State.Bited)
            {
                text = "���� ����";
            }
        }

        if (body == 1)
        {
            curBody = Body.RArm;

            //������
            if (GameManager.Data.RArm == DataManager.State.Scar)
            {
               
                text = "�����ȿ� ��ó�� ��";
            }

            if (GameManager.Data.RArm == DataManager.State.Bleeding)
                text = "�����ȿ��� �ǰ� �帧";

            if (GameManager.Data.RArm == DataManager.State.Bited)
            {
                text = "�������� ����";
               
            }
               
        }


        if (body == 2)
        {
            curBody = Body.LArm;

            //����
            if (GameManager.Data.LArm == DataManager.State.Scar)
            {
              
                text = "���ȿ� ��ó�� ��";
            }

            if (GameManager.Data.LArm == DataManager.State.Bleeding)
                text = "���ȿ��� �ǰ� �帧";

            if (GameManager.Data.LArm == DataManager.State.Bited)
            {

                text = "������ ����";
            }
        }
        

        if (body == 3)
        {
            curBody = Body.RLeg;

            //�����ٸ�
            if (GameManager.Data.RLeg == DataManager.State.Scar)
                text = "�����ٸ��� ��ó�� ��";

            if (GameManager.Data.RLeg == DataManager.State.Bleeding)
                text = "�����ٸ����� �ǰ� �帧";

            if (GameManager.Data.RLeg == DataManager.State.Bited)
            {
                text = "�����ٸ��� ����";
            }
                
        }


        if (body == 4)
        {
            curBody = Body.LLeg;

            //�޴ٸ�
            if (GameManager.Data.LLeg == DataManager.State.Scar)
            {
                text = "�޴ٸ��� ��ó�� ��";
            }
                

            if (GameManager.Data.LLeg == DataManager.State.Bleeding)
                text = "�޴ٸ����� �ǰ� �帧";

            if (GameManager.Data.LLeg == DataManager.State.Bited)
            {
                text = "�޴ٸ��� ����";
            }
            
        }

        MakeButtonGetText();


    }
}
