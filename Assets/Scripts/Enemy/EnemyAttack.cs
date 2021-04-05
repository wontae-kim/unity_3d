using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector]
    public bool isAttack;

    //플레이어 방향을 향해 밀고 들어옴.
    private Transform playerTr;
    
    private void Awake()
    {
       
    }

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if(isAttack)
        {
            Vector3 dir = (playerTr.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime * 2.0f;
        }
    }

    
}
