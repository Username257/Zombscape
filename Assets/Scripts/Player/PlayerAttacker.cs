using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEditor;

public class PlayerAttacker : MonoBehaviour, IHitable, IDamageable
{
    
    [SerializeField] public int damage;
    [SerializeField, Range(0, 360)] public float angle;
    [SerializeField] public float range;
    [SerializeField] int life;
    public int randNum = 0;
    private float cosResult;
    public UnityEvent OnDamaged;
    public bool isDie;
    CharacterController controller;
    Animator anim;
    PlayerMover playerMover;
    public bool isHoldingWeapon;
    public float weaponSpeed;
    public float legSpeed;
    public float freezeTime;
    public bool debug;
    public UnityAction OnWeild;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        playerMover = gameObject.GetComponent<PlayerMover>();
        controller = gameObject.GetComponent<CharacterController>();


        anim.SetLayerWeight(1, 0);
        anim.SetFloat("MeleeLeg", 1f);

        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Start()
    {
        damage = 1;
        freezeTime = 1;
        legSpeed = 1;
        range = 1;
        angle = 45;
    }

    private void Update()
    {
        if (life < 0)
            Die();
    }

    public void HoldingWeapon()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetFloat("MeleeSpeed", weaponSpeed);
        anim.SetFloat("MeleeLeg", legSpeed);
    }

    public void NotHoldingWeapon()
    {
        anim.SetLayerWeight(1, 0);
        damage = 1;
        freezeTime = 1;
        legSpeed = 1;
        range = 1;
        angle = 45;
    }

    public void Hit()
    {

        if (!isDie)
        {
            anim.applyRootMotion = true;

            if (isHoldingWeapon)
            {
                OnWeild.Invoke();
                playerMover.Freeze(freezeTime);
                anim.SetTrigger("IsMelee");
                anim.SetTrigger("IsPunching1");
                return;
            }
            else
            {
                randNum = Random.Range(0, 2);

                playerMover.Freeze(1f);

                if (randNum == 0)
                    anim.SetTrigger("IsPunching");
                else
                    anim.SetTrigger("IsPunching1");

                return;
            }

        }
    }

    public void ApplyDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;

            if (!(collider.tag == "Zombie"))
                continue;

            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                continue;

            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.Damaged(damage);
            StartCoroutine(ApplyDamageWaitTime());

        }
    }

    IEnumerator ApplyDamageWaitTime()
    {
        yield return new WaitForSeconds(1f);
    }

    public void Damaged(int damage)
    {
        if (!isDie)
        {
            playerMover.isDamaged = true;
            playerMover.Freeze(1f);
            anim.applyRootMotion = true;
            anim.SetTrigger("IsDamaged");
            life -= damage;
            GameManager.Data.CurLife = life;
            OnDamaged?.Invoke();
        }
    }


    public void Die()
    {
        GameManager.Data.CurLife = 0;
        isDie = true;
        anim.applyRootMotion = true;
        anim.SetTrigger("IsDie");
        controller.enabled = false;
        playerMover.isDie = true;
        playerMover.enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

        Handles.color = Color.cyan;
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, -angle, range);
        Handles.DrawSolidArc(transform.position, transform.up, transform.forward, angle, range);
    }

}
