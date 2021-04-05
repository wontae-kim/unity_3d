using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    PlayerInput input;
    public Shoot Gun;
    

    Rigidbody rigid;
    Animator anim;

    float move_speed = 5.0f;
    float rot_speed = 180f;

    private bool isSlidding;
    private bool isJump;
    Vector3 MoveVec;
    Vector3 DodgeVec;
    //포톤 추가
    public PhotonView PV;
    public bool isLook;



    


    void Attack()
    {
        if(input.fireReady)
        {
            //트리거도 구별해야 할듯 준비 하는 거랑 쏘는거 구분지어서 
            anim.SetBool("isShotReady", true);
            Gun.ShotReady(input.fireReady);
        }
       
        if(input.fireStart)
        {
            //isLook = true;
            StartCoroutine(ForwardDir());
            Gun.ShotReady(input.fireReady);
            anim.SetBool("isShotReady", false);
            anim.SetTrigger("doShot");
        }
    }

    //마우스 업하면 키보드 마우스 조작으로 인한 플레이어 전방방향 설정은 무시
    IEnumerator ForwardDir()
    {
        isLook = false;
        yield return new WaitForSeconds(0.3f);
        isLook = true;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //Gun = GetComponent<Shoot>();
        Gun.gameObject.SetActive(true);
        input = GetComponent<PlayerInput>();
        
    }

    //FixedUpdate에서 Update로 변경했다. 
    //달리는 도중에 점프나 슬라이딩 바로 안먹히는 문제가 있다.
    void Update()
    {
        if (!PV.IsMine) return;
        input.GetInput();
        Move();

        //마우스 업할때는 동작되지 않도록하자. 
        Rotate();
        
        Jump();
        Aim();
        Sliding();
        Attack();
    }



    void Move()
    {
        MoveVec = new Vector3(input.rotate, 0, input.move).normalized;
        if (isSlidding)
        {
            MoveVec = DodgeVec;
        }
            
        transform.position += MoveVec * Time.deltaTime * move_speed;

        anim.SetBool("isMove", MoveVec != Vector3.zero);

    }

    void Rotate()
    {
        if(isLook)
            transform.LookAt(MoveVec + transform.position);       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            rigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
            isJump = true;
            anim.SetTrigger("doJump");
            anim.SetBool("isJump", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
            anim.SetBool("isJump", false);   
        }

        

    }

    

    void SliddingEnd()
    {
        isSlidding = false;
        move_speed = 5.0f;
    }

    void Sliding()
    {
        if (input.slidingDown && isJump == false)
        {
            isJump = true;
            anim.SetTrigger("Sliding");
            move_speed *= 2.0f;
            DodgeVec = MoveVec;
            isSlidding = true;
        }
    }

    //0326 일단 이렇게 했고, 총 착용했을때만 aim자세 취하게 하기 위해서 불변수 유무로 링크하도록 하자.
    void Aim()
    {
        bool isAim = Input.GetKeyDown(KeyCode.K);
        anim.SetBool("isAim", isAim);
    }
}
