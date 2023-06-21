using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyInfoTextBox : MonoBehaviour
{
    RectTransform Box;
    string text;
    
    public void ChangeText()
    {
        if (GameManager.Data.Neck == DataManager.State.Scar)
            text = "�� ��ó�� ��";

        if (GameManager.Data.Neck == DataManager.State.Bleeding)
            text = "�񿡼� �ǰ� �帧";

        if (GameManager.Data.Neck == DataManager.State.Bited)
            text = "���� ����";
    }
}
