using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairColor : MonoBehaviour
{
    [SerializeField] Material hairColorMat;

    public void hairColor()
    {
        hairColorMat.color = gameObject.GetComponent<Image>().color;
    }
}
