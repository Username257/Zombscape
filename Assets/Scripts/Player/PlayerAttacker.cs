using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class PlayerAttacker : MonoBehaviour, IHitable, IDamageable
{
    Animator anim;
    [SerializeField] PlayerMover playerMover;
    [SerializeField] int damage;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] float range;
    [SerializeField] int life;
    public int randNum = 0;
    private float cosResult;
    public UnityEvent OnDamaged;
    public bool isDie;
    CharacterController controller;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMover = GetComponent<PlayerMover>();
        controller = GetComponent<CharacterController>();
        
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }
    private void Update()
    {
        if (life < 0)
            Die();
    }
    public void Hit()
    {
        if (!isDie)
        {
            playerMover.Freeze();

            randNum = Random.Range(0, 2);

            if (randNum == 0)
                anim.SetTrigger("IsPunching");
            else
                anim.SetTrigger("IsPunching1");

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
            playerMover.Freeze();
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
        anim.SetTrigger("IsDie");
        controller.enabled = false;
        playerMover.isDie = true;
        playerMover.enabled = false;
        this.enabled = false;
    }
}
