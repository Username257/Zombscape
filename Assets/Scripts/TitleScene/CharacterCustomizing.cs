using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizing : MonoBehaviour
{
    [SerializeField] GameObject characterSkin;
    [SerializeField] GameObject characterHair;


    [SerializeField] TMP_Text skinText;
    int skinIndex = 0;

    [SerializeField] TMP_Text hairText;
    int hairIndex = 0;

    [SerializeField] List<string> skinNames = new List<string>();
    [SerializeField] List<string> hairNames = new List<string>();

    public void Start()
    {
        changeSkin();
        changeHair();
    }

    public void prevSkinIndex()
    {
        characterSkin.transform.GetChild(skinIndex).gameObject.SetActive(false);

        skinIndex = Mathf.Clamp(skinIndex - 1, 0, skinNames.Count - 1);
        changeSkin();
    }

    public void nextSkinIndex()
    {
        characterSkin.transform.GetChild(skinIndex).gameObject.SetActive(false);

        skinIndex = Mathf.Clamp(skinIndex + 1, 0, skinNames.Count - 1);
        changeSkin();
    }

    public void randomSkinIndex()
    {
        characterSkin.transform.GetChild(skinIndex).gameObject.SetActive(false);

        skinIndex = Random.Range(0, skinNames.Count);
        changeSkin();
    }

    public void changeSkin()
    {
        characterSkin.transform.GetChild(skinIndex).gameObject.SetActive(true);

        skinText.SetText(skinNames[skinIndex]);
    }



    public void prevHairIndex()
    {
        characterHair.transform.GetChild(hairIndex).gameObject.SetActive(false);

        hairIndex = Mathf.Clamp(hairIndex -1, 0, hairNames.Count - 1);
        changeHair();
    }

    public void nextHairIndex()
    {
        characterHair.transform.GetChild(hairIndex).gameObject.SetActive(false);

        hairIndex = Mathf.Clamp(hairIndex + 1, 0, hairNames.Count - 1);
        changeHair();
    }

    public void randomHairIndex()
    {
        characterHair.transform.GetChild(hairIndex).gameObject.SetActive(false);

        hairIndex = Random.Range(0, hairNames.Count);
        changeHair();
    }

    public void changeHair()
    {
        characterHair.transform.GetChild(hairIndex).gameObject.SetActive(true);

        hairText.SetText(hairNames[hairIndex]);

    }
}
