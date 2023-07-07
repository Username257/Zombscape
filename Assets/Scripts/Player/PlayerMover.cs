using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    PlayerEater eater;
    private CharacterController controller;
    private Vector3 moveDir;
    private Animator anim;
    private float ySpeed;
    [SerializeField] private bool isFreeze;
    public bool isDie;
    public bool isDamaged;


    private void Start()
    {
        eater = gameObject.GetComponent<PlayerEater>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        moveSpeed = 3f;
    }

    private void Update()
    {
        if (!isFreeze && !isDie && !isDamaged)
            Move();
        Fall();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            return;
        }

        if (!isDamaged && !isFreeze)
        {
            Vector3 moveVec = new Vector3(moveDir.x, 0, moveDir.y);
            controller.Move(moveVec * Time.deltaTime * moveSpeed);
            Quaternion lookRotation = Quaternion.LookRotation(moveVec);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
        }

        anim.SetFloat("MoveSpeed", moveSpeed, 0.1f, Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        if (!isDie)
        {
            anim.applyRootMotion = false;
            moveDir = value.Get<Vector2>();
            anim.SetBool("IsMove", moveDir.sqrMagnitude > 0);
            Move();
        }
    }

    private void Sneak()
    {
        moveSpeed = 1f;
    }

    private void OnSneak(InputValue value)
    {
        anim.applyRootMotion = false;

        if (value.isPressed)
            Sneak();
        else
            moveSpeed = 3f;
    }

    private void Run()
    {
        moveSpeed = 5f;
    }

    private void OnRun(InputValue value)
    {
        anim.applyRootMotion = false;
        if (value.isPressed)
            Run();
        else
            moveSpeed = 3f;
    }
    private void Fall()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (GroundCheck() && ySpeed < 0)
        {

            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    public void Freeze(float freezeTime)
    {
        if (isFreeze)
        {
            StopCoroutine(meltRoutine);
        }
        isFreeze = true;
        meltRoutine = StartCoroutine(FreezeTime(freezeTime));
    }

    public void Melt()
    {
        isFreeze = false;
        isDamaged = false;
    }

    Coroutine meltRoutine;
    IEnumerator FreezeTime(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);
        Melt();
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1f, 0.5f, Vector3.down, out hit, 0.6f);
    }

    
}
