using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    GameObject m_goPrefab = null;
    List<Transform> objectList = new List<Transform>();
    List<GameObject> hpBarList = new List<GameObject>();

    Camera cam = null;

    void Start()
    {
        cam = Camera.main;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < objs.Length; i++)
        {
            objectList.Add(objs[i].transform);
            GameObject t_hpBar = Instantiate(m_goPrefab, objs[i].transform.position,
                Quaternion.identity, transform);
            hpBarList.Add(t_hpBar);

        }
    }

    void Update()
    {
        for(int i = 0; i < objectList.Count; i++)
        {          
            hpBarList[i].transform.position = cam.WorldToScreenPoint(objectList[i].position + new Vector3(0,2.0f, 0));
        }
    }
}
