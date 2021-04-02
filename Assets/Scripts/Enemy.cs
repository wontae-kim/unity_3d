using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hp;
    Material mat;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            hp -= other.GetComponent<Bullet>().damage;
            Debug.Log("적의 hp는 " + hp);
            Destroy(other.gameObject);
            StartCoroutine(OnDamage());
        }
    }

    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(hp > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.grey;
            gameObject.layer = 12;
            Destroy(gameObject, 3f);
        }
    }
}
