using UnityEditor;

namespace EasyGame.EditorTools
{
    public class IModleCreate : CreateScriptWizard<IModleCreate>
    {
        public ModelOrSys 脚本类型 = ModelOrSys.系统;
        public string Name = "NewScript";
        private string scriptName = "NewScript";
        private string templatePath;
        [MenuItem("Assets/EasyGame/创建Modle|System层脚本", false, 2)]
        static void CreateWizard()
        {
            InitWizard();
        }

        private void OnWizardCreate()
        {
            Create(scriptName, templatePath, new int[] { 7 });
        }

        private void OnWizardUpdate()
        {
            scriptName = Name;
            if (脚本类型 == ModelOrSys.系统)
            {
                templatePath = "Assets/EasyGame/模板/ISystem.txt";
            }
            else if (脚本类型 == ModelOrSys.数据)
            {
                templatePath = "Assets/EasyGame/模板/IModel.txt";
            }
        }

        public enum ModelOrSys
        {
            系统, 数据
        }
    }
}