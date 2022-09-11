using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;
using UnityEditor.Callbacks;

namespace EasyGame.EditorTools
{
    public class Excel2Script : CreateScriptWizard<Excel2Script>
    {
        [MenuItem("EasyGame/生成物体脚本", false, 21)]
        static void Create()
        {
            EditorPrefs.SetBool("是否生成", true);
            CreateScript();
        }

        static void CreateScript()
        {
            var path = EditorExtra.GetNameAndPath()[0];
            var fileName = EditorExtra.GetNameAndPath()[1];

            if (fileName == "")
            {
                Debug.LogError("没有选中文件");
                return;
            }

            //创建文件流操作对象
            FileStream fs = new FileStream(path + ".xlsx", FileMode.Open, FileAccess.Read);
            //创建一个Excel读取类
            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet result = reader.AsDataSet();

            int rows = result.Tables[0].Rows.Count;//获取Excel行
            int Columns = result.Tables[0].Columns.Count;//获取Excel列

            ArrayList mName = new ArrayList();//变量名数组
            ArrayList mType = new ArrayList();//变量类型数组
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (i == 0)
                    {
                        string type = result.Tables[0].Rows[i][j].ToString();
                        mType.Add(type);
                    }
                    else
                    {
                        string name = result.Tables[0].Rows[i][j].ToString();
                        mName.Add(name);
                    }
                }
            }
            fs.Close();

            if (!Directory.Exists("Assets/Datas/" + fileName))
            {
                Directory.CreateDirectory("Assets/Datas/" + fileName);
            }

            string filePath = "Assets/Datas/" + fileName + "/" + fileName + ".cs";
            string datasFilePath = "Assets/Datas/" + fileName + "/" + "Datas" + fileName + ".cs";

            //创建文件
            FileStream fs2 = new FileStream(filePath, FileMode.Create);
            FileStream fs3 = new FileStream(datasFilePath, FileMode.Create);
            fs2.Close();
            fs3.Close();
            //写入文件
            StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.Default);
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("[System.Serializable]");
            writer.Write("public class {0}", fileName);
            writer.WriteLine("{");

            //字段内容
            for (int i = 0; i < mName.Count; i++)
            {
                string s = "    public ";
                s += mType[i] + " " + mName[i] + ";";
                writer.WriteLine(s);
            }

            writer.WriteLine("}");
            writer.Close();

            //集合类
            StreamWriter writer2 = new StreamWriter(datasFilePath, false, System.Text.Encoding.Default);
            writer2.WriteLine("using System.Collections.Generic;");
            writer2.WriteLine("using EasyGame;");
            writer2.WriteLine("public class Datas{0} : EasyAssets", fileName);
            writer2.WriteLine("{");
            writer2.WriteLine("    public List<{0}> dataArray = new List<{1}>();", fileName, fileName);
            writer2.WriteLine("}");
            writer2.Close();
            AssetDatabase.ImportAsset(filePath);
            AssetDatabase.ImportAsset(datasFilePath);
        }

        [DidReloadScripts]
        static void OK()
        {
            if (EditorPrefs.GetBool("是否生成"))
            {
                Debug.Log("编译完成");
                EditorPrefs.SetBool("是否生成", false);
            }
        }
    }
}
