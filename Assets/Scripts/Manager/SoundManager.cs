using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //배경이랑 효과음 겹치게 하기 위해서 변수 2개 선언함.
    public AudioSource bgAudio;
    public AudioSource fxAudio;

    public AudioClip[] bgClips;
    public AudioClip[] fxClips;

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        PlayBgm(0);
    }

    public void Play(int clipNumber)
    {
        fxAudio.PlayOneShot(fxClips[clipNumber]);
    }

    public void PlayBgm(int clipNumber)
    {
        bgAudio.clip = bgClips[clipNumber];
        bgAudio.Play();
    }
}
