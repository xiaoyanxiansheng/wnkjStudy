using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IABLoaderManager : MonoBehaviour {

    public static IABLoaderManager Instance;
    IABScenceManager iABScenceManager;
    void Awake()
    {
        Instance = this;

        // 加载manifest
        StartCoroutine(IABManifestLoader.Instance.LoadManifest());

        iABScenceManager = new IABScenceManager("testScence");
    }
    
    // TODO 这里没有理解
    public void LoadCallBack(string scenceName, string bundleName)
    {
        StartCoroutine(iABScenceManager.LoadAssetBundle(bundleName));
    }

    public void LoadAsset(string scenceName, string bundleName, LoaderProgress progress)
    {
        iABScenceManager.LoadAsset(bundleName,progress,LoadCallBack);
    }

    public Object GetSingleResources(string scenceName, string bundleName, string resName)
    {
        return iABScenceManager.GetSingleResources(bundleName,resName);
    }

    public Object[] GetMultiResources(string scenceName, string bundleName)
    {
        return iABScenceManager.GetMultiResource(bundleName, bundleName);
    }

    // 释放每一个资源
    public void UnLoadResObj(string scenceName, string bundleName, string res)
    {
        iABScenceManager.DisposeBundleResObj(bundleName, res);
    }
    // 释放整个包
    public void UnLoadBundleObj(string scenceName, string bundleName)
    {
        iABScenceManager.DisposeBundleRes(bundleName);
    }
    // 释放所有资源
    public void UnLoadAllResObjs(string scenceName)
    {
        iABScenceManager.DisposeAllObj();
    }
    // 释放bundle包
    public void UnLoadAssetBundle(string scenceName, string bundleName)
    {
        iABScenceManager.DisposeBundle(bundleName);
    }

    public void UnLoadAllAssetBundle(string scenceName, string bundleName)
    {
        iABScenceManager.DisposeAllBundle();
    }

    public void UnLoadAllAssetBundleAndRes(string scenceName, string bundleName)
    {
        iABScenceManager.DisposeAllBundleAndRes();
    }

    void OnDestroy()
    {
        System.GC.Collect();
    }

    public void DebugAllAssetBundle(string scenceName)
    {
        iABScenceManager.DebugAllAsset();
    }

    public bool IsLoadingFinish(string scenceName, string bundleName)
    {
        return iABScenceManager.IsLoadingFinish(bundleName);
    }

    public bool IsLoadingAssetBundle(string scenceName, string bundleName)
    {
        return iABScenceManager.IsLoadingAssetBundle(bundleName);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
