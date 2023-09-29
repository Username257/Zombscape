using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string playerName;
    public string PlayerName { get { return playerName; } set { playerName = value; } }

    public event UnityAction OnCurLifeChanged;

    [SerializeField] private int curLife;
    public int CurLife
    {
        get { return curLife; }
        set
        {
            curLife = value;
            OnCurLifeChanged?.Invoke();
        }
    }

    public event UnityAction OnCurHungerChanged;
    [SerializeField] private float curHunger;
    public float CurHunger
    {
        get { return curHunger; }
        set
        {
            curHunger = value;
            OnCurHungerChanged?.Invoke();
        }
    }

    public enum State { None, Scar, Bleeding, Bited, Bandaged };

    [SerializeField] private State neck = State.None;
    public State Neck
    {
        get { return neck; }
        set
        {
            neck = value;
            GameObject.FindWithTag("BodyUI").GetComponent<BodyUI>().ChangeImage(0);
            GameObject.FindWithTag("BodyInfoTextBox").GetComponent<BodyInfoTextBox>().GetText(0);
        }
    }

    [SerializeField] private State rLeg = State.None;
    public State RLeg
    {
        get { return rLeg; }
        set
        {
            rLeg = value;
            GameObject.FindWithTag("BodyUI").GetComponent<BodyUI>().ChangeImage(3);
            GameObject.FindWithTag("BodyInfoTextBox").GetComponent<BodyInfoTextBox>().GetText(3);
        }
    }

    [SerializeField] private State lLeg = State.None;
    public State LLeg
    {
        get { return lLeg; }
        set
        {
            lLeg = value;
            GameObject.FindWithTag("BodyUI").GetComponent<BodyUI>().ChangeImage(4);
            GameObject.FindWithTag("BodyInfoTextBox").GetComponent<BodyInfoTextBox>().GetText(4);
        }
    }


    [SerializeField] private State rArm = State.None;
    public State RArm
    {
        get { return rArm; }
        set
        {
            rArm = value;
            GameObject.FindWithTag("BodyUI").GetComponent<BodyUI>().ChangeImage(1);
            GameObject.FindWithTag("BodyInfoTextBox").GetComponent<BodyInfoTextBox>().GetText(1);
        }
    }

    [SerializeField] private State lArm = State.None;
    public State LArm
    {
        get { return lArm; }
        set
        {
            lArm = value;
            GameObject.FindWithTag("BodyUI").GetComponent<BodyUI>().ChangeImage(2);
            GameObject.FindWithTag("BodyInfoTextBox").GetComponent<BodyInfoTextBox>().GetText(2);
        }
    }

    public void Awake()
    {
        if (playerName == null)
        {
            playerName = "¾Æ¹«°³";
        }
    }



}
