using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float health { get; private set; }
    
    [SerializeField]
    private float startHealth = 100f;
    public bool isDead { get; private set; }
    public event Action onDeath;

    //사망
    //리스폰시 hp 초기화 
    //데미지
    protected virtual void OnEnable()
    {
        health = startHealth;
        isDead = false;
    }

    //인터페이스로 인해 접근제어지시자 변경해줌.
    public virtual void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        health -= damage;
        
        if(health < 0 && !isDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        
        if(onDeath != null)
        {
            onDeath();
        }
    }
}
