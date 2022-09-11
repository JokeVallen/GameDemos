using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

[Assets]
public class DialogAsset : EasyAssets
{
    public DialogContext[] dialogList;
    [System.Serializable]
    public struct DialogContext
    {
        public string DialogName;
        public TextAsset dialogText;
    }
}

