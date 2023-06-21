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
            text = "목에 상처가 남";

        if (GameManager.Data.Neck == DataManager.State.Bleeding)
            text = "목에서 피가 흐름";

        if (GameManager.Data.Neck == DataManager.State.Bited)
            text = "목이 물림";
    }
}
