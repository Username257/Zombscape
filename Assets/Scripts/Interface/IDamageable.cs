using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damaged(int damage);
    void Die();
}
