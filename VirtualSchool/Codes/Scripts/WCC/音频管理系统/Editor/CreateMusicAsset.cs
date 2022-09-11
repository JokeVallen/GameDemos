using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace EasyGame.EditorTools
{
    public class CreateMusicAsset : CreateScriptWizard<CreateMusicAsset>
    {
        public string Name;
        [MenuItem("Assets/EasyGame/创建音频列表", false, 7)]
        static void CreateWizard()
        {
            InitWizard();
        }

        private void OnWizardCreate()
        {
            var datas = ScriptableObject.CreateInstance("MusicAsset");
            var path = EditorExtra.GetCurrentAssetDirectory();
            var suffix = ".asset";
            path = EditorExtra.GetDifferentFileNames(path, Name, suffix);
            AssetDatabase.CreateAsset(datas, path);
        }

        private void OnWizardUpdate()
        {
            if (Name == null)
            {
                Name = "NewMusicList";
            }
        }
    }
}


