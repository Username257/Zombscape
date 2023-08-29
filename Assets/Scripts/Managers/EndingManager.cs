using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingManager : MonoBehaviour
{
    public enum deathState { Alive, dByHunger, dByBleed, dByAttack, dByInfected };
    public deathState curState;
    [SerializeField] public GameObject endingCanvas;

    public deathState CurState
    {
        get { return curState; }
        set { curState = value; endingCanvas.GetComponent<EndingCanvas>().SetEndingCanvas(); }
    }

    public UnityAction OnCurStateChange;

    private void Awake()
    {
        curState = deathState.Alive;
    }

}
