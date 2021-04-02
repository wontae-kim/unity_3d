﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerMove : MonoBehaviour
{
    //PlayerInput input;
    public Shoot Gun;
    float move;
    float rotate;
    bool reload;
    bool fireReady;
    bool fireStart;
    bool slidingDown; 

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

    void Attack()
    {
        if(fireReady)
        {
            //트리거도 구별해야 할듯 준비 하는 거랑 쏘는거 구분지어서 
            anim.SetBool("isShotReady", true);
            Gun.ShotReady(fireReady);
        }
       
        if(fireStart)
        {
            Gun.ShotReady(fireReady);
            anim.SetBool("isShotReady", false);
            anim.SetTrigger("doShot");
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //Gun = GetComponent<Shoot>();
        Gun.gameObject.SetActive(true);
    }

    //FixedUpdate에서 Update로 변경했다. 
    //달리는 도중에 점프나 슬라이딩 바로 안먹히는 문제가 있다.
    void Update()
    {
        if (!PV.IsMine) return;
        GetInput();
        Move();
        //Rotate();
        
        Jump();
        Aim();
        Sliding();
        Attack();
    }

    void GetInput()
    {
                
        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
        fireReady = Input.GetButton("Fire1");
        reload = Input.GetButtonDown("Reload");
        slidingDown = Input.GetButtonDown("Sliding");
        fireStart = Input.GetButtonUp("Fire1");
    }

    void Move()
    {
        MoveVec = new Vector3(rotate, 0, move).normalized;
        if (isSlidding)
        {
            MoveVec = DodgeVec;
        }
            
        transform.position += MoveVec * Time.deltaTime * move_speed;

        anim.SetBool("isMove", MoveVec != Vector3.zero);

    }

    void Rotate()
    {
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
        if (slidingDown && isJump == false)
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
