using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGetInjured : MonoBehaviour
{
    public int randNum = 0;
    private Body isBleeding;
    public event UnityAction<int> OnGetInjured;
    public UnityEvent OnDie;
    public enum Body { Neck, RArm, LArm, RLeg, LLeg, Retry }

    public void GetInjured()
    {
        Body whichBody = BodySelect();

        //none에서 scar, scar에서 bleeding이 됨
        //bleeding은 bodySelect에서 제외됨

        switch (whichBody)
        {
            case Body.Neck:
                GameManager.Data.Neck++;
                OnGetInjured?.Invoke(0);
                break;
            case Body.RArm:
                GameManager.Data.RArm++;
                OnGetInjured?.Invoke(1);
                break;
            case Body.LArm:
                GameManager.Data.LArm++;
                OnGetInjured?.Invoke(2);
                break;
            case Body.RLeg:
                GameManager.Data.RLeg++;
                OnGetInjured?.Invoke(3);
                break;
            case Body.LLeg:
                GameManager.Data.LLeg++;
                OnGetInjured?.Invoke(4);
                break;
            case Body.Retry:
                GetInjured();
                break;
        }

        CheckUrgent();
    }

    public Body BodySelect()
    {
        //다치는 부위는 목, 오른팔, 왼팔, 오른다리, 왼다리
        //오른팔, 왼팔 다칠 확률 70, 오른다리 왼다리 20, 목 10
        //그러나 이미 피 흘리고 있는 부위는 더이상 다치지 않음
        randNum = Random.Range(0, 101);

        if (randNum < 70)
        {
            //오른팔이 더 다칠 확률 높음
            randNum = Random.Range(0, 101);
            if (randNum < 70)
            {
                if (GameManager.Data.RArm == DataManager.State.Bleeding)
                    return Body.Retry;
                else 
                    return Body.RArm;
            }
            else
            {
                if (GameManager.Data.LArm == DataManager.State.Bleeding)
                    return Body.Retry;
                else
                    return Body.LArm;
            }
        }
        else if (randNum >= 70 && randNum < 90)
        {
            randNum = Random.Range(0, 101);
            if (randNum < 50)
            {
                if (GameManager.Data.RLeg == DataManager.State.Bleeding)
                    return Body.Retry;
                else
                    return Body.RLeg;
            }
            else
            {
                if (GameManager.Data.LLeg == DataManager.State.Bleeding)
                    return Body.Retry;
                else
                    return Body.LLeg;
            }
        }
        else
        {
            if (GameManager.Data.Neck == DataManager.State.Bleeding)
                return Body.Retry;
            else
                return Body.Neck;
        }
    }

    public void CheckUrgent()
    {
        //만약 전신에 피를 흘리면 과다출혈로 죽음
        if (GameManager.Data.Neck == DataManager.State.Bleeding &&
            GameManager.Data.RArm == DataManager.State.Bleeding &&
            GameManager.Data.LArm == DataManager.State.Bleeding &&
            GameManager.Data.RLeg == DataManager.State.Bleeding &&
            GameManager.Data.LLeg == DataManager.State.Bleeding)
        {
            GameManager.Data.CurLife = 0;
            OnDie.Invoke();
        }
            
    }

}
