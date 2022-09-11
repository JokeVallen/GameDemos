using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

public class MainCanvas : ViewController
{
    public static Transform Transform { get; private set; }
    private void Awake()
    {
        Transform = transform;
    }
}

