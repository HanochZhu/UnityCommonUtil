using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class AssetBundleEditor
{
    [MenuItem("MyTool/AssetBundle/CreatAllAssetBundle")]
    public static void creatAllAssetBundle()
    {

        string outputPath = Application.streamingAssetsPath + "/Window/AB/";

        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        AssetBundleManifest abm =  BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/Window/AB/", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }
}
