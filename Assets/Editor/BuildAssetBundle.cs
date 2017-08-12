using UnityEngine;
using System.Collections;
using UnityEditor;
public class BuildAssetBundle : MonoBehaviour {

    [MenuItem("Custom Editor/Create AssetBundles Main")]
    static void CreateAssetbundlesMain()
    {
        string path = IABTools.GetBaseBundlePath();
        BuildPipeline.BuildAssetBundles(path);
        AssetDatabase.Refresh();
    }
}
