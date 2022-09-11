using EasyGame;
using UnityEditor;

namespace EasyGame.EditorTools
{
    public class ICommandCreate : CreateScriptWizard<ICommandCreate>
    {
        public string Name = "NewCommand";
        private string scriptName = "NewCommand";
        [MenuItem("Assets/EasyGame/创建命令脚本", false, 3)]
        static void CreateWizard()
        {
            InitWizard();
        }

        private void OnWizardCreate()
        {
            Create(scriptName, "Assets/EasyGame/模板/ICommand.txt", new int[] { 4 });
        }

        private void OnWizardUpdate()
        {
            scriptName = Name;
        }
    }
}


