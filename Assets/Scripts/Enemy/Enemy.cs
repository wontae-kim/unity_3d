using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hp;
    Material mat;

    private AdjustHpBar hpBarPrefab;
    public int damage;


    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        hpBarPrefab = GetComponent<AdjustHpBar>();
        hpBarPrefab.SetHpBar();

    }


    private void OnTriggerEnter(Collider other)
    {
        //피격시마다 파티클 나오게 만들자. 
        if(other.tag == "bullet")
        {
            hp -= other.GetComponent<Bullet>().damage;
            Debug.Log("적의 hp는 " + hp);
    
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            StartCoroutine(OnDamage());
            hpBarPrefab.hpBarImage.fillAmount = hp / 100f;
    
            //Vector3 hitPos = other.ClosestPoint(transform.position);
           
            //파티클 매니저는 추후에 만들고 일단은 단일작업부터 하자.
            //GameObject exp = EffectManager.instance.GetQueue();
            //exp.transform.position = other.transform.position;
            
        }
    }

   

    //private void OnCollisionEnter(Collision collision)
    //{
    //    ContactPoint contact = collision.contacts[0];
    //    Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
    //    GameObject exp = Instantiate(smallExp, contact.point, collision.transform.rotation);
    //}

    

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

            gameObject.SetActive(false);
            //지우지는 말고 fps를 위해서 안보이게 하자.
            //Destroy(gameObject, 3f);
            //hpBar도 삭제 또는 안보이게 하자.
            //Destroy(hpBarPrefab);
            hpBarPrefab.Destroy();
        }
    }
}
