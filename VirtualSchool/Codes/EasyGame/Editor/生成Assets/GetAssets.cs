using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

public class GetAssets
{
    [MenuItem("Assets/EasyGame/生成Assets文件 %G", false, 5)]
    static void Create()
    {
        List<string> list = EditorExtra.GetNameAndPath();
        if (list[1] == "")
        {
            Debug.LogError("生成失败,请检查是否选中文件");
            return;
        }

        Assembly assembly = null;
        Assembly[] assembly2 = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var item in assembly2)
        {
            if (item.GetName().Name == "Assembly-CSharp")
            {
                assembly = item;
            }
        }

        Type[] types = assembly.GetTypes();
        string str = list[1] + '$';
        Regex regex = new Regex(@str);
        Type type = null;
        foreach (var item in types)
        {
            if (regex.IsMatch(item.FullName))
            {
                type = item;
            }
        }

        if (type == null)
        {
            Debug.LogError("未在程序集中找到该类型");
        }
        if (type.IsDefined(typeof(AssetsAttribute), false))
        {
            AssetsAttribute testAttribute = type.GetCustomAttribute<AssetsAttribute>();
            var obj = ScriptableObject.CreateInstance(type.Name);
            AssetDatabase.CreateAsset(obj, list[0] + ".asset");
        }
        else
        {
            Debug.LogError("生成失败,请检查该脚本是否使用了AssetsAttribute特性");
            return;
        }

    }
}