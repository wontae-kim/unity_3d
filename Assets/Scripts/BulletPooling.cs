using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어들의 불릿 미리 만들어 놓음
public class BulletPooling : MonoBehaviour
{
    //public static BulletPooling instance;
    //멀티플레이니까 플레이어마다 각자 관리하는게 좋지않을까 생각한다.

    Queue<GameObject> bulletQueue = new Queue<GameObject>();
    public GameObject bulletPrefab;
    void Start()
    {
        //넉넉하게 5개 잡자.
        //오브젝트 풀링 테스트 할때는 1000으로 해서 진행함.
        for(int i = 0; i < 10; i++)
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
        obj.SetActive(true);
        return obj;
    }

}
