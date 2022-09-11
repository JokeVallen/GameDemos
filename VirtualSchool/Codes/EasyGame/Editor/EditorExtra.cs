using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorExtra
{
    public static string GetCurrentAssetDirectory()
    {
        foreach (var obj in Selection.GetFiltered<Object>(SelectionMode.Assets))
        {
            var path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path))
                continue;
            if (System.IO.Directory.Exists(path))
            {
                return path;
            }
            else if (System.IO.File.Exists(path))
            {
                return System.IO.Path.GetDirectoryName(path);
            }
        }
        return "Assets";
    }

    public static List<string> GetNameAndPath()
    {
        List<string> result = new List<string>();
        EditorApplication.ExecuteMenuItem("Assets/Copy Path");
        string str = GUIUtility.systemCopyBuffer;

        result.Add(str.Split('.')[0]);

        if (str.Split('.').Length <= 1)
        {
            result.Add("");
            return result;
        }

        string[] list = str.Split('/');
        str = list[list.Length - 1];
        list = str.Split('.');
        str = list[0];
        result.Add(str);
        return result;
    }

    /// <summary>
    /// 获取不同的文件名或文件绝对路径
    /// </summary>
    /// <param name="path">不带文件名的路径</param>
    /// <param name="fileName">文件名</param>
    /// <param name="suffix">后缀</param>
    /// <param name="mod">返回模式</param>
    /// <returns></returns>
    public static string GetDifferentFileNames(string path, string fileName, string suffix, DifferentFileNamesMod mod = DifferentFileNamesMod.Path)
    {
        int i = 1;
        var fullPath = path + "/" + fileName + suffix;
        var resultFileName = fileName;
        while (System.IO.File.Exists(fullPath))
        {
            fullPath = path + "/" + fileName + i + suffix;
            resultFileName = fileName + i;
            ++i;
        }
        if (mod == DifferentFileNamesMod.Path)
        {
            return fullPath;
        }
        else
        {
            return resultFileName;
        }
    }

    public enum DifferentFileNamesMod
    {
        Path, FileName
    }
}

