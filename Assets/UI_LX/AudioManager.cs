using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    AudioSource BGM, SFX;

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
        base.Awake();
        BGM = transform.Find("BGM").GetComponent<AudioSource>();
        SFX = transform.Find("SFX").GetComponent<AudioSource>();

        //初始化数据
        BGM.volume = PlayerPrefs.GetFloat("BGMVolume", 0);

        SFX.volume = PlayerPrefs.GetFloat("SFXVolume",0);
    }

    public void ChangeBGMVolume(float value)
    {
        BGM.volume = value;
    }
    public void ChangeSFXVolume(float value)
    {
        SFX.volume = value;
    }

    public void PlayBGM(string path)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        PlayBGM(audioClip);
    }

    public void PlayBGM(AudioClip audioClip)
    {

        if (audioClip == null)
        {
            return;
        }

        BGM.clip = audioClip;
        BGM.loop = true;
        BGM.Play();
    }
    public void PlaySFX(string path)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        PlaySFX(audioClip);


    }
    public void PlaySFX(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            return;
        }

        SFX.PlayOneShot(audioClip);

    }
}
