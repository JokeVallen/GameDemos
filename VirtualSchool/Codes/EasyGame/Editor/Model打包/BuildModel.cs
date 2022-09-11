using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using EasyGame;
using System.IO;
using System.Text;

public class BuildModel
{
    [MenuItem("EasyGame/Model打包", false, 7)]
    static void OnClick()
    {
        var pathDic = GetAllPath();

        var result = CheckPath(pathDic);
        if (!result) return;

        CreateConfigure(pathDic);
    }

    //获取所有model
    static Dictionary<string, string> GetAllPath()
    {
        Assembly assembly = null;
        Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var item in assemblys)
        {
            if (item.GetName().Name == "Assembly-CSharp")
            {
                assembly = item;
            }
        }
        Type[] types = assembly.GetTypes();
        Dictionary<string, string> pathDic = new Dictionary<string, string>();
        foreach (var item in types)
        {
            var ati = item.GetCustomAttribute(typeof(ModelAttribute));
            if (ati == null) continue;

            if ((ati as ModelAttribute).Path != null)
            {
                pathDic.Add(item.Name, (ati as ModelAttribute).Path);
            }
        }
        return pathDic;
    }

    //检查路径是否正确
    static bool CheckPath(Dictionary<string, string> pathDic)
    {
        foreach (var path in pathDic.Values)
        {
            if (!Directory.Exists("Assets/Resources/" + path))
            {
                Debug.LogError("打包失败,没有" + path + "这个文件夹");
                return false;
            }
        }
        return true;
    }


    static void CreateConfigure(Dictionary<string, string> pathDic)
    {
        foreach (var item in pathDic)
        {
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo("Assets/Resources/" + item.Value);
            System.IO.FileInfo[] fileInfos = directoryInfo.GetFiles("*.asset", System.IO.SearchOption.AllDirectories);
            string result = "[";

            for (int i = 0; i < fileInfos.Length; i++)
            {
                var name = fileInfos[i].FullName.Split(new string[] { "Resources\\" + item.Value + "\\", ".asset" }, StringSplitOptions.RemoveEmptyEntries)[1];
                name = name.Replace('\\', '/');
                result += "\"" + name + "\"";

                if (i == fileInfos.Length - 1) break;

                result += ",";
            }
            result += "]";

            byte[] databyte = Encoding.UTF8.GetBytes(result);
            FileStream jsonFileStream = File.Create("Assets/Resources/" + item.Value + "/" + item.Key + ".json");
            jsonFileStream.Write(databyte, 0, databyte.Length);
            jsonFileStream.Close();
            AssetDatabase.ImportAsset("Assets/Resources/" + item.Value + "/" + item.Key + ".json");
        }
    }
}

