using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //오브젝트 풀링 적용전
    //public GameObject bullet;
    //public Transform bulletPos;

    //private으로 바꾸자. 0404
    private Transform shotPos;


    private LineRenderer bulletLineRenderer;
    [SerializeField]
    private Transform user;


    private Vector3 targetDir;
    private Vector3 pointTolook;

    //0404 총구 파티클 
    //private ParticleSystem muzzleFlash;

    IEnumerator Shot()
    {
        //마우스업 할때는 시선을 마우스 포인트가 가리키는 방향으로 
        SoundManager.instance.Play(0);

        user.transform.LookAt(pointTolook);
        //muzzleFlash.Play();
        yield return null;

        //마우스 버튼 놓으면 발사하도록 바꾸자.
        //GameObject bulletIns = Instantiate(bullet, bulletPos.position, bulletPos.rotation);

        GameObject bulletIns = BulletManager.instance.GetQueue();
        bulletIns.transform.position = shotPos.position;
        bulletIns.transform.forward = shotPos.forward;

        bulletIns.SetActive(true);
        //트레일러 문제 때문에 일단은 주석처리함.
        //Rigidbody bulletRigid = bulletIns.GetComponent<Rigidbody>();
        //bulletRigid.transform.position = shotPos.position;
        //bulletRigid.velocity = shotPos.forward * 50.0f;
        //bulletRigid.transform.forward = shotPos.forward;


        //0402 추가함.
        yield return new WaitForSeconds(1f);
        //오브젝트 풀링 적용 전
        //Destroy(bulletIns, 1.0f);

        BulletManager.instance.InsertQueue(bulletIns);
        //bulletPool.InsertQueue(bulletIns);
    }

    public void Use()
    {
        StartCoroutine(Shot());
    }

    void Awake()
    {
        //muzzleFlash = GetComponentInChildren<ParticleSystem>();

        //bulletPool = GetComponent<BulletPooling>();       
        bulletLineRenderer = GetComponent<LineRenderer>();
        // 사용할 점을 두개로 변경
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;

        GameObject parent = transform.root.gameObject;
        //하위 계층 중에 Transform 가지고 있는놈을 모두 찾음
        Transform[] trans = parent.GetComponentsInChildren<Transform>();

        //0404 bulletPos를 private으로 사용하기 위해서 코드추가함.
        foreach (Transform tran in trans)
        {
            if(tran.gameObject.name == "shotPos")
            {
                shotPos = tran;
                break;
            }
        }

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
                pointTolook = ray.GetPoint(rayLength);                
                //player.transform.LookAt(pointTolook);
                bulletLineRenderer.SetPosition(0, user.position);
                //마우스 찍고 있는 방향으로 => 레이트레이싱을 해야한다. 
               
                //방향벡터를 계산하자
                targetDir = (pointTolook - user.position).normalized;

                //마우스 버튼up하면 해당 방향으로 발사 + 회전한다.
                bulletLineRenderer.SetPosition(1, targetDir * 5.0f
                    + user.transform.position);
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
