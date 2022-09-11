using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

namespace EasyGame
{
    [Assets]
    public class MusicAsset : EasyAssets
    {
        public List<ClipInfo> clipList = new List<ClipInfo>();
    }

    [System.Serializable]
    public struct ClipInfo
    {
        public string name;
        public AudioClip audioClip;
        [Range(0, 1)] public float volume;
    }
}

