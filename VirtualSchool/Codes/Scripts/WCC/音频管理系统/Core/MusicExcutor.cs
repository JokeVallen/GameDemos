using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class MusicExcutor : Excutor
{
    public void CallSourceToPlay(MusicInfo info)
    {
        GameManager.Get<EasyMessage>().Send("播放音乐", info);
    }

    public void CallSourceToPause(string fileName)
    {
        GameManager.Get<EasyMessage>().Send("暂停音乐", fileName);
    }

    public void CallSourceToContinue(string fileName)
    {
        GameManager.Get<EasyMessage>().Send("继续播放", fileName);
    }

    public void CallSourceToSetVolume(float volume)
    {
        GameManager.Get<EasyMessage>().Send("设置音量", volume);
    }

}

public struct MusicInfo
{
    public string sourceName;
    public AudioClip clip;
    public bool loop;
    public float volume;
    public MusicInfo(string sourceName, AudioClip clip, bool loop, float volume)
    {
        this.sourceName = sourceName;
        this.clip = clip;
        this.loop = loop;
        this.volume = volume;
    }
}

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    float curClipVolume;//clip的音量，非实际音量
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Get<EasyMessage>().AddListener("播放音乐", info => Play((MusicInfo)info));
        GameManager.Get<EasyMessage>().AddListener("暂停音乐", fileName => Pause((string)fileName));
        GameManager.Get<EasyMessage>().AddListener("继续播放", fileName => Continue((string)fileName));
        GameManager.Get<EasyMessage>().AddListener("设置音量", volume => SetVolume((float)volume));
    }

    void Play(MusicInfo info)
    {
        if (info.sourceName != this.name) return;

        curClipVolume = info.volume;
        audioSource.volume = info.volume * GameManager.Get<EasyMusic>().Volume;
        audioSource.clip = info.clip;
        audioSource.loop = info.loop;
        audioSource.Play();
    }

    void Pause(string fileName)
    {
        if (fileName == null) audioSource.Pause();
        if (this.name != fileName) return;
        audioSource.Pause();
    }

    void Continue(string fileName)
    {
        if (fileName == null) audioSource.Play();
        if (this.name != fileName) return;
        audioSource.Play();
    }

    void SetVolume(float volume)
    {
        audioSource.volume = curClipVolume * volume;
    }
}