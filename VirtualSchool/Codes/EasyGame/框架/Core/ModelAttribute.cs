using System;

/// <summary>
/// 数据层特性，添加此特性后该数据会自动添加进容器
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ModelAttribute : Attribute
{
    public string Path { get; set; }
    public Type Type { get; set; }

    public ModelAttribute() { }
    public ModelAttribute(string path)
    {
        Path = path;
    }
    public ModelAttribute(Type type)
    {
        Type = type;
    }
    public ModelAttribute(Type type, string path)
    {
        Type = type;
        Path = path;
    }
}

