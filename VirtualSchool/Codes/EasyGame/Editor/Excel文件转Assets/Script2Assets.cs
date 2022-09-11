using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;
using System.Reflection;
using System;

namespace EasyGame.EditorTools
{
    public class Script2Assets
    {
        [MenuItem("EasyGame/生成资源文件", false, 22)]
        static void Create()
        {
            var path = EditorExtra.GetNameAndPath()[0];
            var fileName = EditorExtra.GetNameAndPath()[1];

            if (fileName == "")
            {
                Debug.LogError("没有选中文件");
                return;
            }

            //加载程序集,返回类型是一个Assembly
            Assembly assembly = null;
            Assembly[] assembly2 = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in assembly2)
            {
                if (item.GetName().Name == "Assembly-CSharp")
                {
                    assembly = item;
                }
            }

            var datas = ScriptableObject.CreateInstance(assembly.GetType("Datas" + fileName));

            Type curType = assembly.GetType(fileName);


            //实例化方法类
            ExcelTool excelTool = new ExcelTool();
            MethodInfo method = typeof(ExcelTool).GetMethod("CreateAsset");
            MethodInfo generic = method.MakeGenericMethod(curType);
            object[] p = { path, fileName };

            Type type = assembly.GetType("Datas" + fileName);
            FieldInfo[] flist = type.GetFields();
            foreach (var item in flist)
            {
                item.SetValue(datas, generic.Invoke(excelTool, p));
            }

            AssetDatabase.CreateAsset(datas, path + "Item" + ".asset");
        }
    }


    public class ExcelTool
    {
        public List<T> CreateAsset<T>(string path, string fileName)
        {
            //创建文件流操作对象
            FileStream fs = new FileStream(path + ".xlsx", FileMode.Open, FileAccess.Read);
            //创建一个Excel读取类
            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet result = reader.AsDataSet();

            int rows = result.Tables[0].Rows.Count;//获取Excel行
            int Columns = result.Tables[0].Columns.Count;//获取Excel列

            ArrayList mname = new ArrayList();//变量名数组
            ArrayList mtype = new ArrayList();//变量类型数组
            List<T> list = new List<T>();//返回的数组

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (i == 0)
                    {
                        string type = result.Tables[0].Rows[i][j].ToString();
                        mtype.Add(type);
                    }
                    else
                    {
                        string name = result.Tables[0].Rows[i][j].ToString();
                        mname.Add(name);
                    }
                }
            }

            for (int i = 2; i < rows; i++)
            {
                //反射设置属性
                Assembly assembly = null;
                Assembly[] assembly2 = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var item in assembly2)
                {
                    if (item.GetName().Name == "Assembly-CSharp")
                    {
                        assembly = item;
                    }
                }//加载程序集,返回类型是一个Assembly
                Type type = assembly.GetType(fileName);
                object obj = Activator.CreateInstance(type);
                FieldInfo[] flist = type.GetFields();

                int j = 0;
                foreach (var item in flist)
                {
                    string s = mtype[j].ToString();
                    if (s == "string")
                    {
                        item.SetValue(obj, result.Tables[0].Rows[i][j].ToString());
                    }
                    else if (s == "int")
                    {
                        item.SetValue(obj, Int32.Parse(result.Tables[0].Rows[i][j].ToString()));
                    }
                    else if (s == "float")
                    {
                        item.SetValue(obj, float.Parse(result.Tables[0].Rows[i][j].ToString()));
                    }
                    else
                    {

                    }
                    j++;
                }
                j = 0;
                list.Add((T)obj);
            }
            fs.Close();
            return list;
        }
    }
}
