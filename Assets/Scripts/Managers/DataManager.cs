using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{

    public event UnityAction<int> OnCurLifeChanged;

    [SerializeField] private int curLife = 100;
    public int CurLife
    {
        get { return curLife; }
        set
        {
            curLife = value;
            OnCurLifeChanged?.Invoke(curLife);
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
        }
    }

    [SerializeField] private State rLeg = State.None;
    public State RLeg
    {
        get { return rLeg; }
        set
        {
            rLeg = value;
        }
    }

    [SerializeField] private State lLeg = State.None;
    public State LLeg
    {
        get { return lLeg; }
        set
        {
            lLeg = value;
        }
    }


    [SerializeField] private State rArm = State.None;
    public State RArm
    {
        get { return rArm; }
        set
        {
            rArm = value;
        }
    }

    [SerializeField] private State lArm = State.None;
    public State LArm
    {
        get { return lArm; }
        set
        {
            lArm = value;
        }
    }


}
