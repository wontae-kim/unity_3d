using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar_cady : MonoBehaviour
{
    [SerializeField]
    GameObject m_goPrefab = null;
    List<Transform> objectList = new List<Transform>();
    List<GameObject> hpBarList = new List<GameObject>();

    //부모 RectTransform 컴포넌트
    private RectTransform rectParent;
    //자신 RectTransform 컴포넌트
    private RectTransform rectHp;
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

    void LateUpdate()
    {
        for(int i = 0; i < objectList.Count; i++)
        {          
            hpBarList[i].transform.position = cam.WorldToScreenPoint(objectList[i].position + new Vector3(0,2.0f, 0));
        }
    }
}
