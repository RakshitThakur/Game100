using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleBuilder
{
    [MenuItem("Assets/BuildAssetBundles")]
    public static void BuildAsset()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\raksh\Desktop\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
    }
}
