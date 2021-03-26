using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFloor : MonoBehaviour
{
    public enum DIR
    {
        CW,
        CCW    
    }

    public DIR dir;
    public float offset;

    private void OnBecameInvisible()
    {
        Debug.Log("안보임");
        //gameObject.SetActive(false);
       
        enabled = false;
    }

    private void OnBecameVisible()
    {
        Debug.Log("보임");
        //gameObject.SetActive(true);

        enabled  = true;
    }

    void Awake()
    {
        enabled = false;
        //rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (!enabled) return;
        //rigid.AddTorque(Vector3.up * 10 * Time.deltaTime);
        transform.Rotate(Vector3.up * offset * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        //if(collision.gameObject.tag == "obstacle")
        //    transform.rotation = collision.collider.transform.rotation;
    }
}
