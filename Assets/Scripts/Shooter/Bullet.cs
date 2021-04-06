using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    Rigidbody rigid = null;
    private GameObject expParticle;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rigid = GetComponent<Rigidbody>();
        expParticle = Resources.Load<GameObject>("Prefabs/Particle/SmallExplosion");
    }
    private void OnEnable()
    {
        trailRenderer.Clear();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        rigid.velocity = transform.forward * 50.0f;
        //rigid.AddForce(transform.forward * 1000.0f);
    }

    private void OnDisable()
    {
        //trailRenderer.enabled = false;
        //trailRenderer.Clear();
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
