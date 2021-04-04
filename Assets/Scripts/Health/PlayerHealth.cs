using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    //hpBar를 가지고 오자.
    private AdjustHpBar hpBarPrefab;
    private PlayerMove playerMove;
    //슈터 비활성화는 추후에 어떻게 할지 보자...
    //private Shoot 
    
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //health -= other.GetComponent<Enemy>().damage;
            float damage = other.GetComponent<Enemy>().damage;

            //일단 이렇게 해서 테스트하자.
            OnDamage(damage, Vector3.zero, Vector3.zero);
            Debug.Log("플레이어의 hp는 " + health);
            hpBarPrefab.hpBarImage.fillAmount = health / 100f;
        }


    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpBarPrefab = GetComponent<AdjustHpBar>();
        hpBarPrefab.SetHpBar();
        playerMove.enabled = true;
    }

    protected override void Die()
    {
        base.Die();

        hpBarPrefab.Destroy();
        playerMove.enabled = false;
    }
}
