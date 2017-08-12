using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IABLoaderManager : MonoBehaviour {

    public static IABLoaderManager Instance;
    IABManager iABManager;
    void Awake()
    {
        Instance = this;

        // 加载manifest
        StartCoroutine(IABManifestLoader.Instance.LoadManifest());

        iABManager = new IABManager();
    }


    public IEnumerator LoadBundle(string bundleName,LoadAssetBundleCallBack call)
    {
        yield return iABManager.LoadBundle(bundleName, call);
    }

    public Object GetBunldeRes(string bundleName, string resName)
    {
        return iABManager.GetBunldeRes(bundleName, resName);
    }

    public Object[] GetMultiResources(string bundleName,string resName)
    {
        return iABManager.GetBunldeMultiRes(bundleName, resName);
    }

    // 释放每一个资源
    public void UnLoadResObj(string bundleName, Object resObj)
    {
        iABManager.DisposeRes(bundleName, resObj);
    }
    // 释放整个包
    public void UnLoadBundle(string bundleName)
    {
        iABManager.DisposeBundle(bundleName);
    }
    // 释放所有资源
    public void UnLoadAllResObjs()
    {
        iABManager.DisposeAllRes();
    }
    // 释放bundle包
    public void UnLoadAssetBundle(string bundleName)
    {
        iABManager.DisposeBundle(bundleName);
    }

    public void UnLoadAllAssetBundle(string bundleName)
    {
        iABManager.DisposeAllBundle();
    }

    public void UnLoadAllAssetBundleAndRes(string bundleName)
    {
        iABManager.DisposeAllBundleAndRes();
    }

    void OnDestroy()
    {
        System.GC.Collect();
    }

    public void DebugAllAssetBundle()
    {
        iABManager.DebugAllAsset();
    }

    public ELoadState IsLoadingFinish(string bundleName)
    {
        return iABManager.BundleLoadState(bundleName);
    }

    public bool IsLoadingAssetBundle(string bundleName)
    {
        return iABManager.IsLoadAssetBundle(bundleName);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
