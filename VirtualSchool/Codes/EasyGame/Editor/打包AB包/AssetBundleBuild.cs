using UnityEngine;
using UnityEditor;
using System.IO;

namespace EasyGame.EditorTools
{
    public class AssetBundleBuild
    {
        [UnityEditor.MenuItem("EasyGame/Build AssetBundle", false, 41)]
        static void MenuItem()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
            BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
        }
    }
}
