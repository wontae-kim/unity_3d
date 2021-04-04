using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 파티클을 여기에 모아놓자.
//일단을 파티클 하나부터
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    Queue<GameObject> effectQueue = new Queue<GameObject>();
    public GameObject effectPrefab;

    //private Dictionary<string, Queue<GameObject>> totalEffect =
    //new Dictionary<string, Queue<GameObject>>();

    public void CreateEffect()
    {
        //AddEffect(30, "EnergyExplosion");
        //AddEffect(30, "SmallExplosion");
        
    }

    private void AddEffect(int poolCount, string name)
    {
        GameObject prefab =
            Resources.Load<GameObject>("Prefabs/Particles/" + name);

        Queue<GameObject> effects = new Queue<GameObject>();
        for (int i = 0; i < poolCount; i++)
        {
            GameObject effect = Instantiate(prefab, transform);
            effect.SetActive(false);
            effect.name = name + i;

            effects.Enqueue(effect);
        }

        //totalEffect.Add(name, effects);
    }

    void Start()
    {
        instance = this;

        for(int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(effectPrefab,
                Vector3.zero, Quaternion.identity);
            //GameObject obj = Resources.Load<GameObject>("Prefabs/Bullet");
            effectQueue.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    public void InsertQueue(GameObject obj)
    {
        obj.SetActive(false);
        effectQueue.Enqueue(obj);
    }

    public GameObject GetQueue()
    {
        GameObject obj = effectQueue.Dequeue();
        obj.SetActive(true);
        return obj;
    }
}
