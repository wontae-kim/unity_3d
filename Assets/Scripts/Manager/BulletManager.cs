using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다른 플레이어와 공유한다.
//적이 쏘는 bullet도 여기서 만들자.
public class BulletManager : MonoBehaviour
{
    static public BulletManager instance;
    
    Queue<GameObject> bulletQueue = new Queue<GameObject>();
    private GameObject bulletPrefab;

    //큐로 해서 만들었고, 다른방식으로는 적군이 쏘는걸로 만들자.


    void Start()
    {
        instance = this;
        //넉넉하게 5개 잡자.
        //오브젝트 풀링 테스트 할때는 1000으로 해서 진행함.
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
      
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(bulletPrefab,
                Vector3.zero, Quaternion.identity);
            
            bulletQueue.Enqueue(obj);
            obj.SetActive(false);
        }

    }

    public void InsertQueue(GameObject obj)
    {
        obj.SetActive(false);
        bulletQueue.Enqueue(obj);
    }

    public GameObject GetQueue()
    {
        GameObject obj = bulletQueue.Dequeue();

        //먼저 활성화를 하게 되면 트레일 렌더러 겹친다.
        //트레일 렌더러 때문에 다른데서 활성화하자.
        //obj.SetActive(true);
        return obj;
    }
}
