using System;
using System.Collections;
using System.Collections.Generic;
using EasyGame;
using UnityEngine;

[Model("音频资源")]
public class MusicMod : AbstractModel
{
    [AutoWrited(typeof(MusicAsset))]
    public Dictionary<string, MusicAsset> musicDir { get; private set; }
    protected override void Init()
    {

    }

}
