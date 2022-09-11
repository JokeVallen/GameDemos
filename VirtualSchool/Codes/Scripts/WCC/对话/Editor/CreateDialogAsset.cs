using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EasyGame.EditorTools;

public class CreateDialogAsset : CreateScriptWizard<CreateDialogAsset>
{
    public string Name;
    [MenuItem("Assets/EasyGame/创建对话列表", false, 8)]
    static void CreateWizard()
    {
        InitWizard();
    }

    private void OnWizardCreate()
    {
        var datas = ScriptableObject.CreateInstance("DialogAsset");
        var path = EditorExtra.GetCurrentAssetDirectory();
        var suffix = ".asset";
        path = EditorExtra.GetDifferentFileNames(path, Name, suffix);
        AssetDatabase.CreateAsset(datas, path);
    }

    private void OnWizardUpdate()
    {
        if (Name == null)
        {
            Name = "NewDialog";
        }
    }
}

