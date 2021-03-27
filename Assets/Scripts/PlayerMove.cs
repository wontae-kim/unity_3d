using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //PlayerInput input;

    float move;
    float rotate;
    bool reload;
    bool fire;
    bool slidingDown; 

    Rigidbody rigid;
    Animator anim;

    float move_speed = 5.0f;
    float rot_speed = 180f;

    private bool isJump;
    Vector3 MoveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //input = GetComponent<PlayerInput>();
    }

    //FixedUpdate에서 Update로 변경했다. 
    //달리는 도중에 점프나 슬라이딩 바로 안먹히는 문제가 있다.
    void Update()
    {
        GetInput();
        Move();
        Rotate();
        
        Jump();
        Aim();
        Sliding();
    }

    void GetInput()
    {
    
        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
        fire = Input.GetButtonDown("Fire1");
        reload = Input.GetButtonDown("Reload");
        slidingDown = Input.GetButtonDown("Sliding");
    }

    void Move()
    {
        MoveVec = new Vector3(rotate, 0, move).normalized;
        transform.position += MoveVec * Time.deltaTime * move_speed;

        //anim.SetFloat("Blend", input.move);
       
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

    void JumpEnd()
    {
        isJump = false;
        move_speed = 5.0f;
    }

    void Sliding()
    {
        if (/*Input.GetButtonDown("Sliding")*/slidingDown && isJump == false)
        {
            isJump = true;
            anim.SetTrigger("Sliding");
            move_speed *= 2.0f;
        }
    }

    //0326 일단 이렇게 했고, 총 착용했을때만 aim자세 취하게 하기 위해서 불변수 유무로 링크하도록 하자.
    void Aim()
    {
        bool isAim = Input.GetKeyDown(KeyCode.K);
        anim.SetBool("isAim", isAim);
    }
}
