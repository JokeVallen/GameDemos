using UnityEditor;
using UnityEngine;

namespace EasyGame.EditorTools
{
    public class CreateScriptWizard<T> : ScriptableWizard where T : CreateScriptWizard<T>
    {
        protected static void InitWizard()
        {
            T wizad = ScriptableWizard.DisplayWizard<T>("生成脚本", "生成");
            wizad.maxSize = new Vector2(280, 200);
            wizad.minSize = new Vector2(280, 200);
        }

        protected static void InitWizard(string titleName, string btnName, float width, float height)
        {
            T wizad = ScriptableWizard.DisplayWizard<T>(titleName, btnName);
            wizad.maxSize = new Vector2(width, height);
            wizad.minSize = new Vector2(width, height);
        }


        protected void Create(string scriptName, string templatePath, int[] index)
        {
            string path = EditorExtra.GetCurrentAssetDirectory();
            ScriptWriter.Write(path, scriptName, templatePath, index);
        }
    }
}

