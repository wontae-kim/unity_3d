using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetPos;
    public Vector3 offsetRot;

    private void Update()
    {
        //transform.position = target.position + offsetPos;
        //transform.LookAt(target);
        //transform.rotation = Quaternion.Euler(offsetRot);
    }

}
