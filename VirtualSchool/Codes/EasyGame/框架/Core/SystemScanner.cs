using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using EasyGame;
using EasyGame.Core;
using UnityEngine;

public class SystemScanner
{
    public static Dictionary<Type, object> Scanning(Assembly assembly)
    {
        Dictionary<Type, object> result = new Dictionary<Type, object>();
        Type[] types = assembly.GetTypes();
        foreach (var item in types)
        {
            var attribute = item.GetCustomAttribute(typeof(SystemAttribute));
            if (attribute?.GetType() == typeof(SystemAttribute))
            {
                object obj = assembly.CreateInstance(item.Name);
                Type type = (attribute as SystemAttribute).Type;

                //创建执行器
                CreateExcutor(item, obj as ISystem, attribute);

                if (type == null)
                {
                    result.Add(item, obj);
                }
                else
                {
                    result.Add(type, obj);
                }
            }
        }
        return result;
    }


    static void CreateExcutor(Type type, ISystem system, Attribute attribute)
    {
        var ari = (attribute as SystemAttribute);

        if (ari.Excutor == null && ari.ExcutorPath == null)//什么都没设置
        {
            GameObject excutor = new GameObject(type.Name);
            GameObject.DontDestroyOnLoad(excutor);
            var ex = excutor.AddComponent<Excutor>();
            (ex as IExcutor).System = system;
            system.Excutor = ex as IExcutor;
        }
        else if (ari.Excutor != null && ari.ExcutorPath == null)//设置了执行器
        {
            GameObject excutor = new GameObject(type.Name);
            GameObject.DontDestroyOnLoad(excutor);
            var ex = excutor.AddComponent((attribute as SystemAttribute).Excutor);
            (ex as IExcutor).System = system;
            system.Excutor = ex as IExcutor;
        }
        else//设置了路径
        {
            var obj = Resources.Load(ari.ExcutorPath);
            if (!(obj is GameObject) || obj == null)
            {
                Debug.LogError("执行器路径或类型错误,请检查" + type.Name + "中的路径或者执行器类型是否填写正确");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
            var excutor = GameObject.Instantiate(obj);
            excutor.name = type.Name;
            GameObject.DontDestroyOnLoad(excutor);
            Component ex = null;
            (excutor as GameObject).TryGetComponent((attribute as SystemAttribute).Excutor, out ex);
            if (ex == null)
            {
                Debug.LogError("没有找到执行器,请检查" + type.Name + "中指向的物体是否含有执行器组件");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }

            (ex as IExcutor).System = system;
            system.Excutor = ex as IExcutor;
        }
    }
}

