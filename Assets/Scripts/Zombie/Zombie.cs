using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour, IDamageable
{
    private enum State { Idle, Walk, Follow, Attack, Dead}
    private State curState;

    [SerializeField] GameObject sound;

    [SerializeField] float detectRange;
    [SerializeField] float attackRange;
    [SerializeField] private int life;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] float range;
    [SerializeField] private bool isFreeze;
    
    public ZombieData zombieData;

    [SerializeField] GameObject player;
    CapsuleCollider capCol;
    SphereCollider sphereCol;
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

    OtherObject otherObject;
    float randomTime;

    Vector3 randomPos;
    float time;
    float soundTime;
    int num;

    public void Awake()
    {
        otherObject = GetComponent<OtherObject>();
    }

    public void Start()
    {

        otherObject.objName = "Ω√√º";

        player = GameObject.FindWithTag("Player");
        nav = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        capCol = gameObject.GetComponent<CapsuleCollider>();
        sphereCol = gameObject.GetComponent<SphereCollider>();

        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
        curState = State.Idle;

        life = zombieData.Hp;
        moveSpeed = zombieData.MoveSpeed;
        damage = zombieData.Damage;
        sphereCol.enabled = false;


        SetAnimSpeed();
        
        otherObject.canShowInventory = false;

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
            case State.Walk:
                UpdateWalk();
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


        if (time <= 0)
        {
            randomTime = Random.Range(1, 10);
        }

        if (time < randomTime)
        {
            time += Time.deltaTime;
        }

        if (time >= randomTime)
        {
            randomPos = Random.insideUnitSphere * 5f;
            randomPos = new Vector3(transform.position.x - randomPos.x, transform.position.y, transform.position.z - randomPos.z);

            time = 0;
            curState = State.Walk;
        }

        soundTime += Time.deltaTime;
        if (soundTime > 6f)
        {
            num = Random.Range(0, 2);
            if (num == 0)
                sound.transform.GetChild(6).GetComponent<AudioSource>().Play();
            else
                sound.transform.GetChild(7).GetComponent<AudioSource>().Play();

            soundTime = 0;
        }


    }

    private void UpdateWalk()
    {
        if (time <= 0)
        {
            randomTime = Random.Range(2, 10);
        }

        if (time < randomTime)
        {
            time += Time.deltaTime;
        }

        if (time >= randomTime)
        {
            time = 0;
            curState = State.Idle;
        }

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

        anim.applyRootMotion = false;
        anim.SetBool("IsWalk", true);

        nav.isStopped = false;
        nav.SetDestination(randomPos);
        nav.speed = moveSpeed;
        
        if (Vector3.Distance(transform.position, randomPos) < 0.1f)
            curState = State.Idle;

        soundTime += Time.deltaTime;
        if (soundTime > 0.5f)
        {
            num = Random.Range(0, 2);
            if (num == 0)
                sound.transform.GetChild(0).GetComponent<AudioSource>().Play();
            else
                sound.transform.GetChild(1).GetComponent<AudioSource>().Play();
            soundTime = 0;

        }
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

        soundTime += Time.deltaTime;
        if (soundTime > 0.5f)
        {
            num = Random.Range(0, 2);
            if (num == 0)
                sound.transform.GetChild(0).GetComponent<AudioSource>().Play();
            else
                sound.transform.GetChild(1).GetComponent<AudioSource>().Play();
            soundTime = 0;

        }
    }

    private void UpdateAttack()
    {
        Rotate();

        if (!isFreeze || !isDamaged)
        {

            anim.SetBool("IsWalk", false);

            nav.SetDestination(transform.position);
            nav.isStopped = true;

            attackTime += Time.deltaTime;

            if (!firstHit)
            {
                if (player.GetComponent<PlayerAttacker>().OnWeildIsInvoking)
                {
                    player.GetComponent<PlayerAttacker>().OnWeildIsInvoking = false;
                    return;
                }

                anim.applyRootMotion = true;
                anim.SetTrigger("IsAttack");

                num = Random.Range(0, 2);
                if (num == 0)
                    sound.transform.GetChild(2).GetComponent<AudioSource>().Play();
                else
                    sound.transform.GetChild(3).GetComponent<AudioSource>().Play();

                Freeze(3f);
                firstHit = true;
            }

            if (attackTime > 3f && firstHit)
            {
                if (player.GetComponent<PlayerAttacker>().OnWeildIsInvoking)
                {
                    player.GetComponent<PlayerAttacker>().OnWeildIsInvoking = false;
                    return;
                }

                anim.applyRootMotion = true;
                anim.SetTrigger("IsAttack");

                num = Random.Range(0, 2);
                if (num == 0)
                    sound.transform.GetChild(2).GetComponent<AudioSource>().Play();
                else
                    sound.transform.GetChild(3).GetComponent<AudioSource>().Play();


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

            num = Random.Range(0, 2);
            if (num == 0)
                sound.transform.GetChild(4).GetComponent<AudioSource>().Play();
            else
                sound.transform.GetChild(5).GetComponent<AudioSource>().Play();


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

            if (player.GetComponent<PlayerAttacker>().OnWeildIsInvoking == true)
            {
                player.GetComponent<PlayerAttacker>().OnWeildIsInvoking = false;
                return;
            }

            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.Damaged(damage);
        }
    }

    public void Die()
    {
        anim.applyRootMotion = true;
        anim.SetBool("IsDie", true);
        otherObject.canShowInventory = true;
        capCol.isTrigger = true;
        sphereCol.enabled = true;
    }


    private void AfterDie()
    {
        anim.enabled = false;
        this.enabled = false;
    }

    private void UpdateDead()
    {
        isDie = true;
        if (!anim.GetBool("IsDie"))
            Die();
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
        if (player.GetComponent<PlayerAttacker>().OnWeildIsInvoking == true)
        {
            player.GetComponent<PlayerAttacker>().OnWeildIsInvoking = false;
            Melt();
        }

        yield return new WaitForSeconds(freezeTime);
        Melt();
    }
}
