using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//wayPoint 지점으로 자동으로 이동하는 코드
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    private int nextIndex;
    private NavMeshAgent agent;

    //순찰과 추적하는 스피드를 구분짓자.
    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;

    //회전을 위해서 damping 값을 추가함.
    private float damping = 1.0f;

    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }

    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        //목적지 가까워지면 속도 감소함.
        agent.autoBraking = false;
    }

    void Start()
    {
        GameObject group = GameObject.Find("WayPointGroup");
        //매개변수는 배열 또는 List로 받는다. //그루트강의 참고하자.
        group.GetComponentsInChildren<Transform>(wayPoints);
        //확인해보니 들어가짐
        //부모도 값이 들어가는데 부모값 제거하자.
        wayPoints.RemoveAt(0);

        //MoveWayPoint();
    }

    
    void Update()
    {
        //적의 회전 속도를 조절함.
        if (!agent.isStopped)
        {
            //가야할 방향인 desiredVelocity를 매개변수로 사용함.
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //Lerp 시킴.
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * damping);
        }
        
        //magnituge : 루드제곱벡터의 크기 sqrMagnitude : 제곱된값 : 차이점 찾아보자.  
        if (agent.velocity.sqrMagnitude >= 0.1f && agent.remainingDistance <= 0.5f)
        {
            //순환시키려고 나머지 연산자로 계산함.
            nextIndex = ++nextIndex % wayPoints.Count;
            MoveWayPoint();
        }
    }

    //설치해놓은 way지점으로 반복적으로 돌아다니게 하는 함수.
    private void MoveWayPoint()
    {
        if (agent.isPathStale)
            return;

        agent.destination = wayPoints[nextIndex].position;
        agent.isStopped = false;
    }

    //
    private void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;

        agent.destination = pos;
        agent.isStopped = false;
    }

    //EnemyAI에서 호출할 것이므로 public으로 함.
    public void Stop()
    {
        agent.isStopped = true;
        //속도제어 안하면 미끄러진다.
        agent.velocity = Vector3.zero;
        patrolling = false;
    }
}
