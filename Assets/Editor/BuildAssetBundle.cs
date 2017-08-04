using UnityEngine;
using System.Collections;
using UnityEditor;
public class BuildAssetBundle : MonoBehaviour {

    [MenuItem("Custom Editor/Create AssetBundles Main")]
    static void CreateAssetbundlesMain()
    {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }
}
