using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy의 동작상태만 
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        IDLE,
        ATTACK,
        DIE
    }

    private readonly int hashFind = Animator.StringToHash("isFind");
    private readonly int hashWalk = Animator.StringToHash("isWalk");

    public State state = State.IDLE;
    Transform playerTrans;
   
    private bool _isDie;
    public bool isDie
    {
        get { return _isDie; }
        set { _isDie = value; }
    }
    private Animator animator;
    private WaitForSeconds ws;

    //플레이어와의 거리를 판단후 행동을 취하게 하기 위해서 
    //거리를 나타낼 변수를 선언하자. 
    public float attackDist = 5.0f;
    public float traceDist = 20.0f;

    private MoveAgent moveAgent;
    private EnemyAttack enemyAttack;


    private void Awake()
    {
        moveAgent = GetComponent<MoveAgent>();
        animator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Start()
    {
        //모든 게임오브젝트들이 생성된 후에 집어넣자.
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        ws = new WaitForSeconds(0.3f);

        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    //죽으면 중지.
    //플레이어와의 거리를 판단후에 거리가 가까우면 추적 아니면 일단은 순찰
    IEnumerator CheckState()
    {
        while(!_isDie)
        {
            if (state == State.DIE)
                yield break;

            float dist = Vector3.Distance(playerTrans.position, transform.position);

            if(dist < attackDist)
            {
                state = State.ATTACK;
            }
            else if(dist < traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }
        
    }

    //상태를 보고 행동을 변화하게 하는 코루틴을 추가하자.
    IEnumerator Action()
    {
        while(!_isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.ATTACK :
                    moveAgent.Stop();
                    animator.SetBool(hashFind, true);
                    
                    enemyAttack.isAttack = true;
                    break;
                case State.DIE:

                    break;
                case State.IDLE:

                    break;
                case State.PATROL:
                    animator.SetBool(hashWalk, false);
                    moveAgent.patrolling = true;
                    break;
                case State.TRACE:
                    animator.SetBool(hashWalk, true);

                    moveAgent.traceTarget = playerTrans.position;
                    break;

            }
            
        }
    }

    //void Attack()
    //{       
    //    Quaternion rot = Quaternion.LookRotation(playerTrans.position - transform.position);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * damping);
    //}
}
