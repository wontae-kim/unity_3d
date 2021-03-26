using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyBelt : MonoBehaviour
{
    public Transform EndPos;
    [SerializeField]
    private float speed;
    public Renderer rend;
    //private float scrollSpeed = 0.5F;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    private void OnTriggerStay(Collider other)
    {
        Transform moveObj = other.gameObject.transform;
        moveObj.position = Vector3.MoveTowards(moveObj.position, EndPos.position, speed * Time.deltaTime);
    }

    private void Update()
    {
        float offset = Time.time * speed * 0.2f;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }

}
