using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCharacter : MonoBehaviour
{
    [SerializeField] Button turnLeftButton;
    [SerializeField] Button turnRightButton;
    [SerializeField] GameObject character;
    [SerializeField] float rotateAmount;

    public void Update()
    {
        if (turnLeftButton.GetComponent<DetectIsPressing>().isPressing)
            character.transform.Rotate(new Vector3(0, -rotateAmount * Time.deltaTime, 0), Space.Self);
        if (turnRightButton.GetComponent<DetectIsPressing>().isPressing)
            character.transform.Rotate(new Vector3(0, rotateAmount * Time.deltaTime, 0), Space.Self);
    }


}
