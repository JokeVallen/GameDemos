using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasyGame.EditorTools
{
    public class ExcutorCreate : CreateScriptWizard<ExcutorCreate>
    {
        public string Name = "NewExcutor";
        private string scriptName = "NewExcutor";
        [MenuItem("Assets/EasyGame/创建执行器", false, 4)]
        static void CreateWizard()
        {
            InitWizard();
        }

        private void OnWizardCreate()
        {
            Create(scriptName, "Assets/EasyGame/模板/Excutor.txt", new int[] { 6 });
        }

        private void OnWizardUpdate()
        {
            scriptName = Name;
        }
    }

}
