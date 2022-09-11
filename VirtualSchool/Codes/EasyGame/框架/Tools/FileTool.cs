using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTool
{
    public static List<string> GetAllFileNamesInAFolder(string path, string source = "")
    {
        if (!System.IO.Directory.Exists(path))
        {
            Debug.LogError(source + "-" + "获取资源失败,请检查路径是否填写正确");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return null;
        }
        var direName = path.Split('/')[2];

        System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
        System.IO.FileInfo[] fileInfos = directoryInfo.GetFiles("*.asset", System.IO.SearchOption.AllDirectories);
        List<string> fileList = new List<string>();
        foreach (var item in fileInfos)
        {
            var name = item.FullName.Split(new string[] { "Resources\\" + direName + "\\", ".asset" }, StringSplitOptions.RemoveEmptyEntries)[1];
            name = name.Replace('\\', '/');
            fileList.Add(name);
        }
        return fileList;
    }

    public static List<string> GetAllFilenamesByConfigure(string configureName)
    {
        var txt = Resources.Load<TextAsset>(configureName);
        var str = txt.text.Replace("[", "");
        str = str.Replace("]", "");
        str = str.Replace("\"", "");
        string[] fileArry = str.Split(',');
        List<string> fileList = new List<string>(fileArry);
        return fileList;
    }
}

