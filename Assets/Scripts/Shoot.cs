using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //오브젝트 풀링 적용전
    //public GameObject bullet;
    public Transform bulletPos;
    public Animator anim;
    public Transform weaponPivot;
    public Transform rightHandMount;

    private LineRenderer bulletLineRenderer;
    [SerializeField]
    private Transform player;

    //오브젝트 풀링 적용
    private BulletPooling bulletPool;

    IEnumerator Shot()
    {
        
        yield return null;

        //마우스 버튼 놓으면 발사하도록 바꾸자.
        //GameObject bulletIns = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        GameObject bulletIns = bulletPool.GetQueue();
        Rigidbody bulletRigid = bulletIns.GetComponent<Rigidbody>();
        bulletRigid.transform.position = bulletPos.position;
        bulletRigid.velocity = bulletPos.forward * 50.0f;

        //0402 추가함.
        yield return new WaitForSeconds(1f);
        //오브젝트 풀링 적용 전
        //Destroy(bulletIns, 1.0f);
        
        bulletPool.InsertQueue(bulletIns);
    }

    public void Use()
    {
        StartCoroutine(Shot());
    }

    void Awake()
    {
        bulletPool = GetComponent<BulletPooling>();
        anim = GetComponent<Animator>();
        bulletLineRenderer = GetComponent<LineRenderer>();
        // 사용할 점을 두개로 변경
        bulletLineRenderer.positionCount = 2;
        // 라인 렌더러를 비활성화
        bulletLineRenderer.enabled = false;
    }

    //발사 구간 처리 및 시선 처리.
    public void ShotReady(bool isFire)
    {
        if(isFire)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //Physics.Raycast(ray, out hit, Mathf.Infinity);

            Plane groupPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groupPlane.Raycast(ray, out rayLength))
            {
                Vector3 pointTolook = ray.GetPoint(rayLength);                
                player.transform.LookAt(pointTolook);
                bulletLineRenderer.SetPosition(0, player.position);
                //마우스 찍고 있는 방향으로 => 레이트레이싱을 해야한다. 
                bulletLineRenderer.SetPosition(1, player.transform.forward * 10.0f
                    + player.transform.position);
                bulletLineRenderer.enabled = true;
            }

            //오브젝트 풀링 테스트용
            //테스트 할때는 밑에 else 주석하고 밑의 Use 함수 호출 주석 풀자
            //누르고 있는 중에 계속 발사하게 만들기 
            //Use();
        }
        else
        {
            bulletLineRenderer.enabled = false;
            Use();
        }
    }



}
