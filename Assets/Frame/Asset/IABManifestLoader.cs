using UnityEngine;
using System.Collections;

public class IABManifestLoader{

    public AssetBundleManifest assetBundleManifest;

    public string manifestPath;

    private bool isLoadFinish;

    public AssetBundle manifestLoader;

    public IABManifestLoader()
    {
        assetBundleManifest = null;
        manifestPath = null;
        isLoadFinish = false;
        manifestLoader = null;
    }

    public IEnumerator LoadManifest()
    {
        WWW manifestWWW = new WWW(manifestPath);
        yield return manifestWWW;

        manifestLoader = manifestWWW.assetBundle;

        assetBundleManifest = manifestLoader.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        isLoadFinish = true;
    }

    public void SetManifestPath(string path)
    {
        manifestPath = path;
    }

    private static IABManifestLoader instance = null;

    public static IABManifestLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new IABManifestLoader();
            }
            return instance;
        }
    }

    public bool IsLoadFinish()
    {
        return isLoadFinish;
    }

    public string[] GetDepences(string bundleName)
    {
        return assetBundleManifest.GetAllDependencies(bundleName);
    }

    public void UnLoadManifest()
    {
        // TODO 为何要true
        manifestLoader.Unload(true);
    }
}
