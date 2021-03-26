using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Transform target;
    private bool isRot;
    private float offset;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            isRot = true;
            offset = collision.gameObject.GetComponent<RotationFloor>().offset;
        }
        else
        {
            isRot = false;
        }
    }

    private void OnWillRenderObject()
    {
        
    }

    //private void OnBecameInvisible()
    //{
    //    Debug.Log("안보임");
    //    enabled = false;
    //}
    //
    //private void OnBecameVisible()
    //{
    //    Debug.Log("보임");
    //
    //}

    void Start()
    {
        
    }

    void Update()
    {
        if(isRot)
        {
            transform.RotateAround(target.position, Vector3.up, offset * Time.deltaTime);
        }
    }
}
