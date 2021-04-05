using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //이동 회전 장전 발사 
    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool reload { get; private set; }
    public bool fire { get; private set; }
    public bool slidingDown { get; private set; }
    public bool fireReady { get; private set; }
    public bool fireStart { get; private set; }
    //입력값 수시로 확인해야 한다. 
    public void GetInput()
    {
        //if(GameMana)

        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
        fireReady = Input.GetButton("Fire1");
        reload = Input.GetButtonDown("Reload");
        slidingDown = Input.GetButtonDown("Sliding");
        fireStart = Input.GetButtonUp("Fire1");
    }

    void Update()
    {
        GetInput();
    }
}
