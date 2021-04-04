using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustHpBar : MonoBehaviour
{
    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    //생명 게이지의 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 Canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillAmount 속성을 변경할 Image
    [HideInInspector]
    public Image hpBarImage;

    //테스트
    public void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        //UI Canvas 하위로 생명 게이지를 생성
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        //fillAmount 속성을 변경할 Image를 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        //생명 게이지가 따라가야 할 대상과 오프셋 값 설정
        var _hpBar = hpBar.GetComponent<HpBar>();

        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    
    public void Destroy()
    {
        hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
    }
}
