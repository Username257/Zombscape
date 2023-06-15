using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAttacker : MonoBehaviour
{
    Animator anim;
    [SerializeField] PlayerMover playerMover;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMover = GetComponent<PlayerMover>();
    }
    public void Attack()
    {
        playerMover.Freeze();
        anim.applyRootMotion = true;
        anim.SetTrigger("IsPunching");
    }

}
