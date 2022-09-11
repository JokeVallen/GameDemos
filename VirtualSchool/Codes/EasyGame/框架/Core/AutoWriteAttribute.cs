using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public class AutoWritedAttribute : Attribute
{
    public Type AssetType { get; set; }
    public AutoWritedAttribute(Type type)
    {
        AssetType = type;
    }
}

