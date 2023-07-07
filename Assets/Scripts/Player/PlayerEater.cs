using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEater : MonoBehaviour
{
    public bool isHoldingFood;
    Animator anim;
    PlayerMover mover;
    float time;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        mover = gameObject.GetComponent<PlayerMover>();
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
            mover.isDie = true;
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

        if (food.FoodData.isDrink)
            anim.SetTrigger("IsDrinking");
        else
            anim.SetTrigger("IsEating");
    }

}
