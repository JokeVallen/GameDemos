using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct DialogInfo
{
    public string Name { get; private set; }
    public string DialogName { get; private set; }
    public DialogueStyle Style { get; private set; }
    public Action OnComplete { get; private set; }
    public DialogInfo(string name, string dialogName, DialogueStyle style, Action onComplete = null)
    {
        Name = name;
        DialogName = dialogName;
        Style = style;
        OnComplete = onComplete;
    }


}

public enum DialogueStyle
{
    General, Tip
}