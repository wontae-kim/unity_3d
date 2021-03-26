using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Caracter : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private bool isAttack = false;
    //0323 펀치 충돌체 활성화 비활성화 
    private Collider punchCollider;

    //펀치 이펙트 관련
    //private GameObject punchEffectPrefab;
    //private Transform punchTransform;
    //private GameObject punchEffectObj;
    public GameObject punchEffect;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        //모든 콜라이더 찾자.
        //Collider[] colliders = GetComponentsInChildren<Collider>();
        //
        //foreach (Collider coll in colliders)
        //{
        //    if(coll.tag == "Punch")
        //    {
        //        punchCollider = coll;
        //    }
        //}
        ////맨처음에는 펀치 콜라이더는 끄자
        //punchCollider.enabled = false;

        //punchEffectPrefab = Resources.Load<GameObject>("Prefabs/PunchEffect");
        //Transform[] transforms = GetComponentsInChildren<Transform>();
        //
        //foreach (Transform tran in transforms)
        //{
        //    if (tran.name == "EffectPos")
        //    {
        //        punchTransform = tran;
        //    }
        //}
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //메인 카메라 태그가 달린 카메라에 접근해서 Camera 컴포넌트에 접근할수 있다.
            //카메라에서 마우스 커서가 피킹한 지점을 가져옴.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //반환용도로 사용되는 hit 
            RaycastHit hit;         
            //물리 클래스 사용함.
            //out 명령어를 사용함.
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                agent.destination = hit.point;
                //Debug.Log(hit.point);
                //print(hit.point);
            }
        }

        //해시테이블 형태로 구성되어 있다.
        animator.SetFloat("speed", agent.velocity.magnitude);

        if (!isAttack && Input.GetKeyDown(KeyCode.Space))
        {
            //미끄러짐 방지 
            agent.velocity = Vector3.zero;
            //이동 중 공격시 멈춤.
            agent.isStopped = true;
            //파라미터 없이 바로 애니메이션을 호출한다.
            animator.CrossFade("Hook Punch", 0.2f);
            //연속동작을 방지한다.
            isAttack = true;

        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("Roll");
            animator.applyRootMotion = true;
            agent.isStopped = true;
        }

    }

    private void EndAttack()
    {
        agent.isStopped = false;
        isAttack = false;
    }

    private void EndRoll()
    {
        animator.applyRootMotion = false;
        agent.isStopped = false;
    }

    private void PunchHitStart()
    {
        punchEffect.SetActive(true);
        //punchCollider.enabled = true;
        //punchEffectObj = Instantiate(punchEffectPrefab,
        //    punchTransform.position, punchTransform.rotation);
    }
    
    private void PunchHitEnd()
    {
        punchEffect.SetActive(false);

        //Destroy(punchEffectObj);
        //punchCollider.enabled = false;
    }
}
