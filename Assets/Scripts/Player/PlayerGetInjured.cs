using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGetInjured : MonoBehaviour
{
    public int randNum = 0;
    private Body isBleeding;
    public UnityAction<int> OnGetInjured;
    public UnityAction<int> OnGetBited;
    public UnityAction OnDie;
    private bool goBited;
    public enum Body { Neck, RArm, LArm, RLeg, LLeg, Retry }


    public void GetInjured()
    {

        Body whichBody = BodySelect();

        //none에서 scar, scar에서 bleeding이 됨
        //bleeding은 bodySelect에서 제외됨

        
        randNum = Random.Range(0, 101);

        //5%의 확률로 물림
        if (randNum < 5)
            goBited = true;

        switch (whichBody)
        {
            case Body.Neck:
                if (goBited)
                {
                    if (GameManager.Data.Neck != DataManager.State.Bited)
                    {
                        GameManager.Data.Neck = DataManager.State.Bited;
                        OnGetBited?.Invoke(0);
                    }

                    goBited = false;
                }
                else
                {
                    if (GameManager.Data.Neck < DataManager.State.Bleeding)
                    {
                        GameManager.Data.Neck++;
                        OnGetInjured?.Invoke(0);
                    }

                }
                break;

            case Body.RArm:
                if (goBited)
                {
                    if (GameManager.Data.RArm != DataManager.State.Bited)
                    {
                        GameManager.Data.RArm = DataManager.State.Bited;
                        OnGetBited?.Invoke(1);

                    }

                    goBited = false;
                }
                else
                {
                    if (GameManager.Data.RArm < DataManager.State.Bleeding)
                    {
                        GameManager.Data.RArm++;
                        OnGetInjured?.Invoke(1);
                    }
                }
                break;

            case Body.LArm:
                if (goBited)
                {
                    if (GameManager.Data.LArm != DataManager.State.Bited)
                    {
                        GameManager.Data.LArm = DataManager.State.Bited;
                        OnGetBited?.Invoke(2);


                    }
                    
                    goBited = false;
                }
                else
                {
                    if (GameManager.Data.LArm < DataManager.State.Bleeding)
                    {
                        GameManager.Data.LArm++;
                        OnGetInjured?.Invoke(2);
                    }
                }
                break;

            case Body.RLeg:
                if (goBited)
                {
                    if (GameManager.Data.RLeg != DataManager.State.Bited)
                    {
                        GameManager.Data.RLeg = DataManager.State.Bited;
                        OnGetBited?.Invoke(3);

                    }

                    goBited = false;
                }
                else
                {
                    if (GameManager.Data.RLeg < DataManager.State.Bleeding)
                    {
                        GameManager.Data.RLeg++;
                        OnGetInjured?.Invoke(3);
                    }
                }
                break;

            case Body.LLeg:
                if (goBited)
                {
                    if (GameManager.Data.LLeg != DataManager.State.Bited)
                    {
                        GameManager.Data.LLeg = DataManager.State.Bited;
                        OnGetBited?.Invoke(4);

                    }

                    goBited = false;
                }
                else
                {
                    if (GameManager.Data.LLeg < DataManager.State.Bleeding)
                    {
                        GameManager.Data.LLeg++;
                        OnGetInjured?.Invoke(4);
                    }
                }
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
                    return Body.RArm;
            }
            else
            {
                    return Body.LArm;
            }
        }
        else if (randNum >= 70 && randNum < 90)
        {
            randNum = Random.Range(0, 101);
            if (randNum < 50)
            {
                    return Body.RLeg;
            }
            else
            {
                    return Body.LLeg;
            }
        }
        else
        {
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
            GameManager.Ending.CurState = EndingManager.deathState.dByBleed;
            OnDie?.Invoke();
        }

        if (GameManager.Data.Neck == DataManager.State.Bited &&
            GameManager.Data.RArm == DataManager.State.Bited &&
            GameManager.Data.LArm == DataManager.State.Bited &&
            GameManager.Data.RLeg == DataManager.State.Bited &&
            GameManager.Data.LLeg == DataManager.State.Bited)
        {
            GameManager.Data.CurLife = 0;
            EndingManager endingManager = GameObject.FindWithTag("EndingManager").GetComponent<EndingManager>();
            endingManager.CurState = EndingManager.deathState.dByBleed;
            OnDie?.Invoke();
        }

    }



}
