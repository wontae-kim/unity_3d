using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    Rigidbody rigid = null;
    private GameObject expParticle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        expParticle = Resources.Load<GameObject>("Prefabs/Particle/SmallExplosion");
    }
    private void OnEnable()
    {
        rigid.velocity = Vector3.zero;       
    }

    //bullet에 몬스터 충돌할 경우 파티클 나오게 하자.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Vector3 hitPos = other.ClosestPoint(transform.position);
            Instantiate(expParticle, hitPos, Quaternion.identity);

            //ParticleSystem particle = 
        }
    }
}
