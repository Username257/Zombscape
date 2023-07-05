using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEater : MonoBehaviour
{
    [SerializeField] float hunger = 0;
    [SerializeField] float curHunger;
    public bool isHoldingFood;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void IsHoldingFood(Food food)
    {
        if (isHoldingFood)
            Eat(food);
    }

    Coroutine eatting;
    public void Eat(Food food)
    {
        if (food.FoodData.isDrink)
            anim.SetTrigger("IsDrinking");
        else
            anim.SetTrigger("IsEating");

    }

}
