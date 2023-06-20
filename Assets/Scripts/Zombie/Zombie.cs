using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    }

    private void Update()
    {
        if (life < 0)
            curState = State.Dead;

        disToTarget = (player.transform.position - transform.position).magnitude;
        dir = player.transform.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

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

    private void UpdateIdle()
    {
        anim.SetBool("IsWalk", false);

        if (disToTarget < detectRange)
        {
            followTarget = player;
            curState = State.Follow;
            return;
        }
    }

    private void UpdateFollow()
    {
        if (!isFreeze)
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
    }

    private void UpdateAttack()
    {
        if (!isFreeze || !isDamaged)
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
    }


    public void Damaged(int damage)
    {
        if (!isDie)
        {
            Freeze(3f);
            anim.applyRootMotion = true;
            anim.SetTrigger("IsDamaged");
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

    public void Freeze(float time)
    {
        float freezeTime = +time;
        isFreeze = true;
        Invoke("Melt", freezeTime);
    }

    public void Melt()
    {
        isFreeze = false;
        isDamaged = false;
    }


}
