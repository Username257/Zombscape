using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour, IHideable, IDamageable
{
    private enum State { Idle, Follow, Attack, Dead}
    private State curState;

    [SerializeField] float detectRange;
    [SerializeField] float attackRange;
    [SerializeField] private int life;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] float range;
    [SerializeField] private bool isFreeze;
    
    private ZombieData zombieData;
    public ZombieData ZombieData { set { zombieData = value; } }

    GameObject player;
    GameObject followTarget;
    float disToTarget;
    Vector3 dir;
    NavMeshAgent nav;
    Animator anim;
    [SerializeField] float attackTime;
    private float cosResult;
    bool isDie;
    bool firstHit;
    bool isDamaged;


    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        nav = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();

        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
        curState = State.Idle;

        life = zombieData.Hp;
        moveSpeed = zombieData.MoveSpeed;
        damage = zombieData.Damage;

        SetAnimSpeed();
    }

    private void Update()
    {
        if (life < 0)
            curState = State.Dead;

        disToTarget = (player.transform.position - transform.position).magnitude;
        dir = player.transform.position - transform.position;

        

        switch (curState)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Follow:
                UpdateFollow();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Dead:
                UpdateDead();
                break;

        }
    }
    private void SetAnimSpeed()
    {
        if (moveSpeed == 3)
            anim.SetFloat("moveSpeed", 6f);
        else if (moveSpeed == 0.5)
            anim.SetFloat("moveSpeed", 2f);
        else
            anim.SetFloat("moveSpeed", 4f);
      
    }

    public void Rotate()
    {
        if (!isFreeze || !isDamaged)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }
    private void UpdateIdle()
    {
        anim.SetBool("IsWalk", false);

        nav.SetDestination(transform.position);

        if (disToTarget < detectRange && (GameManager.Data.CurLife > 0))
        {
            if (!isFreeze || !isDamaged)
            {
                followTarget = player;
                curState = State.Follow;
                return;
            }

        }
        if (life <= 0)
            curState = State.Dead;
    }

    private void UpdateFollow()
    {
        Rotate();

        if (!isFreeze || !isDamaged)
        {
            anim.applyRootMotion = false;
            anim.SetBool("IsWalk", true);

            nav.isStopped = false;
            nav.SetDestination(player.transform.position);
            nav.speed = moveSpeed;

            if (disToTarget < attackRange)
            {
                if (GameManager.Data.CurLife > 0)
                {
                    curState = State.Attack;
                    return;
                }
            }

            if (disToTarget >= detectRange)
            {
                curState = State.Idle;
                return;
            }
        }

        if (GameManager.Data.CurLife <= 0)
        {
            curState = State.Idle;
        }

        if (life <= 0)
            curState = State.Dead;
    }

    private void UpdateAttack()
    {
        Rotate();

        if(player.GetComponent<PlayerAttacker>().OnWeildIsInvoking == true)
        {
            player.GetComponent<PlayerAttacker>().OnWeildIsInvoking = false;
        }

        if (!isFreeze || !isDamaged || !player.GetComponent<PlayerAttacker>().OnWeildIsInvoking)
        {
            anim.SetBool("IsWalk", false);

            nav.SetDestination(transform.position);
            nav.isStopped = true;

            attackTime += Time.deltaTime;

            if (!firstHit)
            {
                anim.applyRootMotion = true;
                anim.SetTrigger("IsAttack");
                Freeze(3f);
                firstHit = true;
            }

            if (attackTime > 3f && firstHit)
            {
                anim.applyRootMotion = true;
                anim.SetTrigger("IsAttack");
                Freeze(3f);
                attackTime = 0;
            }

            if (disToTarget > attackRange)
            {
                curState = State.Follow;
                firstHit = false;
                return;
            }
        }

        if(GameManager.Data.CurLife <= 0)
        {
            curState = State.Idle;
        }

        if (life <= 0)
            curState = State.Dead;
    }


    public void Damaged(int damage)
    {
        if (!isDie)
        {
            Freeze(3f);
            anim.applyRootMotion = true;
            anim.SetTrigger("IsDamaged");
            Debug.Log("좀비가 데미지를 입었음");
            isDamaged = true;
            life -= damage;
        }
        
    }


    public void Hit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;

            if (!(collider.tag == "Player"))
                continue;

            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                continue;

            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.Damaged(damage);
        }
    }


    public void Die()
    {
        anim.applyRootMotion = true;
        anim.SetTrigger("IsDie");
        this.enabled = false;
    }

    private void UpdateDead()
    {
        isDie = true;
        Die();
    }

    public void Hide()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void Show()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    Coroutine meltRoutine;
    
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

    IEnumerator FreezeTime(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);
        Melt();
    }
}
