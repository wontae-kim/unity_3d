using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigid;
    [SerializeField]
    private float speed;

    float vInput;
    float hInput;
    private Vector3 MoveVec;

    private bool isRot;
    private float offset;
    private Transform target;
    private bool isJump;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

    }

    void GetInput()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        MoveVec = new Vector3(hInput, 0, vInput).normalized;
        //transform.position += MoveVec * Time.deltaTime * speed;
        //anim.SetBool("speed", MoveVec != Vector3.zero);
        anim.SetFloat("speed", vInput);
    }

    void Rotate()
    {
        transform.LookAt(MoveVec + transform.position);

        if (isRot)
        {
            transform.RotateAround(target.position, Vector3.up, offset * Time.deltaTime);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;
            anim.SetTrigger("jump");
        }
    }

    void JumpEnd()
    {
        isJump = false;
    }

    void Sliding()
    {
        if(Input.GetButtonDown("Sliding") && isJump == false)
        {
            isJump = true;
            anim.SetTrigger("Sliding");
        }
    }

    void Update()
    {
        GetInput();
        Move();
        Rotate();
        Jump();
        Aim();
        Sliding();
    }

    //0326 일단 이렇게 했고, 총 착용했을때만 aim자세 취하게 하기 위해서 불변수 유무로 링크하도록 하자.
    void Aim()
    {
        bool isAim = Input.GetKeyDown(KeyCode.K);
        anim.SetBool("isAim", isAim);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            isRot = true;
            RotationFloor rot_obs = collision.gameObject.GetComponentInChildren<RotationFloor>();

            if(rot_obs.dir == RotationFloor.DIR.CW)
            {
                offset = -rot_obs.offset;
            }
            else
            {
                offset = rot_obs.offset;
            }
            target = rot_obs.transform;
            //offset = collision.gameObject.GetComponent<RotationFloor>().offset;
        }
        else
        {
            isRot = false;
        }
    }
}
