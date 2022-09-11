using UnityEditor;

namespace EasyGame.EditorTools
{
    public class IViewCreate : CreateScriptWizard<IViewCreate>
    {
        public string Name = "NewView";
        private string scriptName = "NewView";
        private string templatePath;
        int[] index;
        [MenuItem("Assets/EasyGame/创建View层脚本", false, 1)]
        static void CreateWizard()
        {
            InitWizard();
        }

        private void OnWizardCreate()
        {
            Create(scriptName, templatePath, index);
        }

        private void OnWizardUpdate()
        {
            scriptName = Name;
            templatePath = "Assets/EasyGame/模板/IView.txt";
            index = new int[] { 6 };
        }
    }
}


