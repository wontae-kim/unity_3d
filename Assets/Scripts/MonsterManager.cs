using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //List를 private으로 사용하려면 동적할당해야 한다. 
    //find를 하는 첫번째 방법

    //여러 지역에서 몬스터 리스폰되도록 하는 방법.
    //private List<Transform> spawnList= new List<Transform>();

    //find를 하는 두번째 방법
    //태그를 이용하자. - 태그를 지정해야 한다. 
    private Transform spawnPoint;

    private GameObject grootPrefab;
    //private float time = 0.0f;
    public float respawnTime = 3.0f;
    
    //동적인 배열 사용할때는 C#에서는 list를 주로 사용한다. 
    void Awake()
    {
        grootPrefab = Resources.Load<GameObject>("Prefabs/Groot");
    }

    //반드시 Start 함수에서 찾아야 한다. 
    //Awake에서는 호출순서 때문에 못찾는다.
    void Start()
    {
        //find를 하는 첫번째 방법
        //GameObject monsterSpot = GameObject.Find("MonsterSpot");
        //for (int i = 0; i < monsterSpot.transform.childCount; i++)
        //{
        //    spawnList.Add(monsterSpot.transform.GetChild(i));
        //}

        //근데 이렇게 하면 한곳에서만 나온다. 일단 패스 
        //find를 하는 두번째 방법

        //한 곳에서 생성되게 하자.
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint").transform;

        StartCoroutine(MonsterSpawn());
    }
    void Update()
    {
        //time += Time.deltaTime;
        //
        //if (time > respawnTime)
        //{
        //    time = 0;
        //
        //    CreateMonster();
        //}
        
    }

    //코루틴을 이용해 위의 update함수를 대체하자.
    IEnumerator MonsterSpawn()
    {
        while (true)
        {
            CreateMonster();

            //yield return null;//다음 프래임까지 지연
            yield return new WaitForSeconds(respawnTime);
        }
    }

    private void CreateMonster()
    {
        //find를 하는 첫번째 방법
        //Transform spawnPoint = spawnList[Random.Range(0, spawnList.Count)];
        GameObject grootObj = Instantiate(
        grootPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

    
    

}
