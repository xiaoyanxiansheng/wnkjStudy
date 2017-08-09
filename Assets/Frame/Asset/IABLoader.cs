using UnityEngine;
using System.Collections;
using System;

// 每帧回调
public delegate void LoaderProgress(string bundleName,float progress);
// 加载完成回调
public delegate void LoadFinish(string bundle);

public class IABLoader:IDisposable{

    private string bundleName;

    private string commonBundlePath;

    private WWW commonLoader;

    private float commonResLoaderProgress;

    private LoaderProgress loaderProgress;

    private LoadFinish loadFinish;

    private IABResLoader iABResLoaser;

    public IABLoader(LoaderProgress tmpLoaderProgress, LoadFinish tmpLoadFinish)
    {
        commonBundlePath = "";
        bundleName = "";
        commonResLoaderProgress = 0;
        loaderProgress = tmpLoaderProgress;
        loadFinish = tmpLoadFinish;
    }

    public void SetBundleName(string TmpBundleName)
    {
        bundleName = TmpBundleName;
    }

    public void LoadResources(string path)
    {
        commonBundlePath = path;
    }

    public IEnumerator CommonLoad()
    {
        commonLoader = new WWW(commonBundlePath);

        while (!commonLoader.isDone)
        {
            commonResLoaderProgress = commonLoader.progress;
            loaderProgress(bundleName, commonResLoaderProgress);
        }
        yield return commonLoader;
        
        loaderProgress(bundleName, commonResLoaderProgress);
        loadFinish(bundleName);

        iABResLoaser = new IABResLoader(commonLoader.assetBundle);

        commonLoader.Dispose();
    }

    public UnityEngine.Object GetResource(string name)
    {
        return iABResLoaser[name];
    }

    public UnityEngine.Object[] GetMutiRersource(string name)
    {
        return iABResLoaser.LoadResources(name);
    }

    public void DebugLoader()
    {
        iABResLoaser.DebugAllRes(); 
    }

    // 卸载assetbundle
    public void Dispose()
    {
        if (iABResLoaser!=null)
        {
            iABResLoaser.Dispose();
            iABResLoaser = null;
        }
    }

    // 卸载assetbundle load出来的资源
    public void UnLoadAssetRes(UnityEngine.Object tmpObj)
    {
        if (iABResLoaser != null)
            iABResLoaser.UnLoadRes(tmpObj);
    }
}
