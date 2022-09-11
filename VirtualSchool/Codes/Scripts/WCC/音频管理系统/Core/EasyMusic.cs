using System.Collections;
using System.Collections.Generic;
using EasyGame;
using UnityEngine;

[System(typeof(MusicExcutor))]
public class EasyMusic : AbstractSystem
{
    MusicMod musicModel;
    MusicExcutor musicExcutor;
    public float Volume { get; private set; }
    protected override void Init()
    {
        Volume = 1;//初始化音量

        musicExcutor = GetExcutor<MusicExcutor>();
        InitAudioSoruce();
    }

    void InitAudioSoruce()
    {
        musicModel = GameManager.Get<MusicMod>();
        foreach (var item in musicModel.musicDir)
        {
            GameObject obj = new GameObject(item.Key);
            GameObject.DontDestroyOnLoad(obj);
            obj.transform.SetParent(musicExcutor.transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            obj.AddComponent<MusicPlayer>();
        }
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="fileName">文件名称</param>
    /// <param name="clipName">音频名称</param>
    /// <param name="loop">是否循环</param>
    public void Play(string fileName, string clipName, bool loop = false)
    {
        if (!musicModel.musicDir.ContainsKey(fileName)) return;

        foreach (var item in musicModel.musicDir[fileName].clipList)
        {
            if (item.name == clipName)
            {
                musicExcutor.CallSourceToPlay(new MusicInfo(fileName, item.audioClip, loop, item.volume));
                return;
            }
        }
    }

    /// <summary>
    /// 暂停播放
    /// </summary>
    /// <param name="fileName">文件名</param>
    public void Pause(string fileName)
    {
        musicExcutor.CallSourceToPause(fileName);
    }

    /// <summary>
    /// 暂停所有音乐
    /// </summary>
    public void PauseAll()
    {
        musicExcutor.CallSourceToPause(null);
    }

    /// <summary>
    /// 继续播放
    /// </summary>
    /// <param name="fileName">文件名</param>
    public void Continue(string fileName)
    {
        musicExcutor.CallSourceToContinue(fileName);
    }

    /// <summary>
    /// 继续所有音乐
    /// </summary>
    public void ContinueAll()
    {
        musicExcutor.CallSourceToContinue(null);
    }

    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="volume">音量大小(0-1)</param>
    public void SetVolume(float volume)
    {
        Volume = volume;
        musicExcutor.CallSourceToSetVolume(volume);
    }
}
