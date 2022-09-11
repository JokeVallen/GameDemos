namespace EasyGame.EditorTools
{
    public static class ScriptWriter
    {
        public static void Write(string scriptPath, string name, string templatePath, int[] index)
        {
            var path = scriptPath + "/" + name + ".cs";
            var className = name;

            int i = 1;
            while (System.IO.File.Exists(path))
            {
                path = scriptPath + "/" + name + i + ".cs";
                className = name + i;
                ++i;
            }

            var writer = System.IO.File.CreateText(path);
            System.IO.StreamReader reader = new System.IO.StreamReader(templatePath, false);
            string str;//临时字符串
            int count = 0;//行数
            int row = 0;
            while ((str = reader.ReadLine()) != null)
            {
                if (count == index[row] - 1)
                {
                    str = string.Format(@str, className);
                    if (row < index.Length - 1)
                    {
                        ++row;
                    }
                }
                writer.WriteLine(str);
                ++count;
            }
            reader.Close();
            writer.Close();

            UnityEditor.AssetDatabase.ImportAsset(path);
        }
    }

}