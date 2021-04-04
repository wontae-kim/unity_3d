using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    static public BulletManager instance;
    //static public BulletManager instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            GameObject obj = new GameObject("BulletManager");
    //            _instance = obj.AddComponent<BulletManager>();
    //        }
    //
    //        return _instance;
    //    }
    //}
    Queue<GameObject> bulletQueue = new Queue<GameObject>();
    public GameObject bulletPrefab;

    //public void CreateBullet()
    //{
    //    //넉넉하게 5개 잡자.
    //    //오브젝트 풀링 테스트 할때는 1000으로 해서 진행함.
    //    for (int i = 0; i < 10; i++)
    //    {
    //        //GameObject obj = Instantiate(bulletPrefab,
    //        //    Vector3.zero, Quaternion.identity);
    //        GameObject obj = Resources.Load<GameObject>("Prefabs/Bullet");
    //        bulletQueue.Enqueue(obj);
    //        obj.SetActive(false);
    //    }
    //}

    void Start()
    {
        //넉넉하게 5개 잡자.
        //오브젝트 풀링 테스트 할때는 1000으로 해서 진행함.

        instance = this;
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(bulletPrefab,
                Vector3.zero, Quaternion.identity);
            //GameObject obj = Resources.Load<GameObject>("Prefabs/Bullet");
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
        obj.SetActive(true);
        return obj;
    }
}
