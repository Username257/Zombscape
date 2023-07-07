using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEater : MonoBehaviour
{
    [SerializeField] float hunger = 0;
    [SerializeField] float curHunger;
    public bool isHoldingFood;
    Animator anim;
    PlayerMover mover;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        mover = gameObject.GetComponent<PlayerMover>();
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

        mover.Freeze(2f);
    }

}
