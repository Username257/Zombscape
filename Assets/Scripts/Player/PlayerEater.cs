using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEater : MonoBehaviour
{
    public bool isHoldingFood;
    Animator anim;
    PlayerAttacker attacker;
    float time;
    public int randNum = 0;
    bool isHealed;
    public UnityAction<int> OnHealed;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        attacker = gameObject.GetComponent<PlayerAttacker>();
    }
    public void Update()
    {
        time += Time.deltaTime;

        if (time > 5)
        {
            GameManager.Data.CurHunger ++;
            time = 0;
        }

        if (GameManager.Data.CurHunger > 100)
        {
            attacker.Die();
            GameManager.Data.CurHunger = 100;
            this.enabled = false;
        }
    }

    public void IsHoldingFood(Food food)
    {
        if (isHoldingFood)
            Eat(food);
    }

    Coroutine eatting;
    public void Eat(Food food)
    {
        GameManager.Data.CurHunger -= food.FoodData.satisfy;
        if (GameManager.Data.CurHunger < 0)
            GameManager.Data.CurHunger = 0;

        if (food.FoodData.itemName == "¾Ë¾à")
        {
            if (GameManager.Data.Neck != DataManager.State.None || 
                GameManager.Data.RArm != DataManager.State.None ||
                GameManager.Data.LArm != DataManager.State.None ||
                GameManager.Data.RLeg != DataManager.State.None ||
                GameManager.Data.LLeg != DataManager.State.None)
            {
                isHealed = false;
                while (!isHealed)
                    TakePhills();
                
            }
        }
            

        if (food.FoodData.isDrink)
            anim.SetTrigger("IsDrinking");
        else
            anim.SetTrigger("IsEating");
    }

    private void TakePhills()
    {
        randNum = Random.Range(0, 5);

        switch (randNum)
        {
            case 0:
                if (GameManager.Data.Neck == DataManager.State.None)
                    break;
                else
                {
                    GameManager.Data.Neck = DataManager.State.None;
                    OnHealed?.Invoke(0);
                    isHealed = true;
                    break;
                }
            case 1:
                if (GameManager.Data.RArm == DataManager.State.None)
                    break;
                else
                {
                    GameManager.Data.RArm = DataManager.State.None;
                    OnHealed?.Invoke(1);
                    isHealed = true;
                    break;
                }
            case 2:
                if (GameManager.Data.LArm == DataManager.State.None)
                    break;
                else
                {
                    GameManager.Data.LArm = DataManager.State.None;
                    OnHealed?.Invoke(2);
                    isHealed = true;
                    break;
                }
            case 3:
                if (GameManager.Data.RLeg == DataManager.State.None)
                    break;
                else
                {
                    GameManager.Data.RLeg = DataManager.State.None;
                    OnHealed?.Invoke(3);
                    isHealed = true;
                    break;
                }
            case 4:
                if (GameManager.Data.LLeg == DataManager.State.None)
                    break;
                else
                {
                    GameManager.Data.LLeg = DataManager.State.None;
                    OnHealed?.Invoke(4);
                    isHealed = true;
                    break;
                }

        }
    }
}
