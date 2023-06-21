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

        //none���� scar, scar���� bleeding�� ��
        //bleeding�� bodySelect���� ���ܵ�

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
        //���� ���ſ� �Ǹ� �긮�� ���������� ����
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
