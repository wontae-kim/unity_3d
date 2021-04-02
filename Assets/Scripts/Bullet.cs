using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    Rigidbody rigid = null;
    //public Transform GunPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rigid.velocity = Vector3.zero;
        //rigid.transform.position = GunPos.position;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        Destroy(gameObject, 3.0f);
    //    }
    //       
    //}
}
