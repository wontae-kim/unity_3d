using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//한 지역에서 그루트 생성되게 한 다음에 하위 오브젝트 스팟순으로
//그루트 돌아다니게 만들자. 
public class Groot : MonoBehaviour
{
    public float speed = 2.0f;

    private List<Transform> spawnList = new List<Transform>();

    private int nextPoint = 0;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        //테스트용
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.CrossFade("Hit On The Back", 0.2f);
        //}

    }

    void Start()
    {
        GameObject monsterSpot = GameObject.Find("MonsterSpot");       
        for (int i = 0; i < monsterSpot.transform.childCount; i++)
        {
            spawnList.Add(monsterSpot.transform.GetChild(i));
        }

        //이동해야할 지점과의 거리를 비교해 
        MoveNextPoint();
        StartCoroutine(Move());
    }

    //매 프레임마다 거리 체크하는 것이 아니라 0.1초마다 확인하자.
    private IEnumerator Move()
    {
        while (true)
        {
            float dist = Vector3.Distance(transform.position,
                spawnList[nextPoint].position);

            if (dist <= 0.5f)
                MoveNextPoint();

            yield return new WaitForSeconds(0.1f);
        }


    }

    private void MoveNextPoint()
    {
        nextPoint = ++nextPoint % spawnList.Count;
        agent.destination = spawnList[nextPoint].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Punch")
        {
            //내비 멈추자.
            agent.isStopped = true;
            animator.CrossFade("Hit", 0.2f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Punch")
        {
            agent.isStopped = true;
            animator.CrossFade("Hit", 0.2f);
        }
    }

    private void HitEnd()
   {
       //내비게이션을 활성화.
       agent.isStopped = false;
       //애님 동작 원복
       animator.CrossFade("Running", 0.2f);
   }

    //public void Damage()
    //{
    //    agent.isStopped = false;
    //    animator.CrossFade("Hit On The Back", 0.2f);
    //}

}
