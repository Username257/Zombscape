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

        //none���� scar, scar���� bleeding�� ��
        //bleeding�� bodySelect���� ���ܵ�

        
        randNum = Random.Range(0, 101);

        //5%�� Ȯ���� ����
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
        //��ġ�� ������ ��, ������, ����, �����ٸ�, �޴ٸ�
        //������, ���� ��ĥ Ȯ�� 70, �����ٸ� �޴ٸ� 20, �� 10
        //�׷��� �̹� �� �긮�� �ִ� ������ ���̻� ��ġ�� ����
        randNum = Random.Range(0, 101);

        if (randNum < 70)
        {
            //�������� �� ��ĥ Ȯ�� ����
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
        //���� ���ſ� �Ǹ� �긮�� ���������� ����
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
