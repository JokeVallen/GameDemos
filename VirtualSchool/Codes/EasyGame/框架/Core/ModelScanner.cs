using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using EasyGame.Core;
using UnityEngine;

public class ModelScanner
{
    public static Dictionary<Type, object> Scanning(Assembly assembly)
    {
        Dictionary<Type, object> result = new Dictionary<Type, object>();
        Type[] types = assembly.GetTypes();
        foreach (var item in types)
        {
            var attribute = item.GetCustomAttribute(typeof(ModelAttribute));
            if (attribute?.GetType() == typeof(ModelAttribute))
            {
                object obj = assembly.CreateInstance(item.Name);
                Type type = (attribute as ModelAttribute).Type;

                //将Asset装配进model
                LoadingAssets(item, obj as IModel, attribute);

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

    static void LoadingAssets(Type type, IModel model, Attribute attribute)
    {
        var ari = (attribute as ModelAttribute);
        if (ari.Path == null) return;

        var path = "Assets/Resources/" + ari.Path;

        List<string> fileList = null;
        var isDevelop = false;
#if UNITY_EDITOR
        isDevelop = UnityEditor.EditorApplication.isPlaying;
#endif
        if (isDevelop)
        {
            fileList = FileTool.GetAllFileNamesInAFolder(path, type.Name);//开发环境
        }
        else
        {
            fileList = FileTool.GetAllFilenamesByConfigure(ari.Path + "/" + type.Name);//构建完成
        }

        //反射设置属性
        PropertyInfo[] pti = type.GetProperties();
        PropertyInfo target = null;
        foreach (var item in pti)
        {
            var ati = item.GetCustomAttribute(typeof(AutoWritedAttribute));
            if (ati != null)
            {
                target = item;
                break;
            }
        }
        if (target == null) return;

        var dic = Activator.CreateInstance
        (
            typeof(Dictionary<,>).MakeGenericType
            (
                new Type[]
                {
                    typeof(string), target.GetCustomAttribute<AutoWritedAttribute>().AssetType
                }
            )
        );
        var addMethod = dic.GetType().GetMethod("Add");
        foreach (var item in fileList)
        {
            var asset = Resources.Load(ari.Path + "/" + item, target.GetCustomAttribute<AutoWritedAttribute>().AssetType);
            if (asset == null) continue;
            addMethod.Invoke(dic, new object[] { item, asset });
        }

        target.SetValue(model, dic);
    }
}

