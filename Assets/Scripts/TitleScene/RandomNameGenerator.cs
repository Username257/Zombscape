using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomNameGenerator : MonoBehaviour
{
    [SerializeField] public List<string> names = new List<string>();
    [SerializeField] Button Dice;
    [SerializeField] TMP_InputField inputField;

    public void GenerateRandomName()
    {
        int index = Random.Range(0, names.Count);

        inputField.text = names[index];
    }
}
