using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// 每帧回调
public delegate void LoaderProgress(string bundleName,float progress);
// 加载完成回调
public delegate void LoadFinish(string bundle);

/// <summary>
/// bundle的加载状态
/// </summary>
public enum ELoadState
{
    notLoad,
    loading,
    loadFinish
}

public class IABLoader:IDisposable{

    private string bundleName;

    private AssetBundle assetBundle;
    
    private LoadFinish loadFinishCall;

    private IABResLoader iABResLoader;

    private ELoadState loadState;

    private float bundleLoadProgress;

    // 依赖关系(其实并不需要存储依赖关系)
    private List<string> depenceBundleList;
    // 引用关系
    private List<string> refrenceBundleList;

    public IABLoader(string tmpBundleName, LoadFinish tmpLoadFinishCall = null)
    {
        bundleLoadProgress = 0;
        bundleName = tmpBundleName;
        loadFinishCall = tmpLoadFinishCall;

        depenceBundleList = new List<string>();
        refrenceBundleList = new List<string>();
    }

    public IEnumerator LoadBundle()
    {
        string bundleNamePath = IABTools.GetAssetBundlePath(bundleName);
        WWW loader = new WWW(bundleNamePath);
        loadState = ELoadState.loading;
        yield return loader;

        assetBundle = loader.assetBundle;

        bundleLoadProgress = loader.progress;
        loadState = ELoadState.loadFinish;

        if (loadFinishCall != null)
        {
            loadFinishCall(bundleName);
        }

        // 资源加载器
        iABResLoader = new IABResLoader(this);

        loader.Dispose();
        loader = null;
    }

    /// <summary>
    /// 获取bundle加载资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object GetBundleRes(string resName)
    {
        return iABResLoader.GetBundleRes(resName);
    }
    /// <summary>
    /// 获取bundle加载资源 不存在则加载
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object GetBundleResAndNotLoad(string resName)
    {
        UnityEngine.Object res = iABResLoader.GetBundleRes(resName);
        if (res == null)
        {
            iABResLoader.LoadBundleRes(resName);
            return iABResLoader.GetBundleRes(resName);
        }
        else
        {
            return res;
        }
    }

    public UnityEngine.Object[] GetBundleMultiRes(string resName)
    {
        return iABResLoader.LoadAssetWithSubAssets(resName);
    }

    public void AddRefrence(string bundleName)
    {
        refrenceBundleList.Add(bundleName);
    }

    public bool RemoveRefrence(string bundleName)
    {
        bool isRemove = false;
        for (int i = 0; i < refrenceBundleList.Count; i++)
        {
            if (refrenceBundleList[i].Equals(bundleName))
            {
                isRemove = true;
                refrenceBundleList.RemoveAt(i);
                break;
            }
        }
        if (refrenceBundleList.Count == 0)
            Dispose();

        return isRemove;
    }

    public void BundleLoadFinish(string bundleName)
    {
        loadState = ELoadState.loadFinish;
    }

    public void DebugLoader()
    {
        iABResLoader.DebugAllRes(); 
    }

    public void Dispose()
    {
        // 这里默认不卸载加载出来的资源
        UnLoadBundle();
    }

    // 卸载bundle
    public void UnLoadBundle(bool unloadAllLoadedObjects = false)
    {
        if (assetBundle!=null)
        {
            assetBundle.Unload(unloadAllLoadedObjects);
            assetBundle = null;
        }
    }

    // 卸载assetbundle load出来的资源
    public void UnLoadRes(UnityEngine.Object tmpObj)
    {
        iABResLoader.UnLoadRes(tmpObj);
    }

    public void UnLoadAllRes()
    {
        iABResLoader.UnLoadAllRes();
    }

    public void UnLoadBundleAndRes()
    {
        UnLoadAllRes();
        UnLoadBundle();
    }

    public float BundleLoadProgress
    {
        get
        {
            return bundleLoadProgress;
        }
    }

    public string BundleName
    {
        get
        {
            return bundleName;
        }
    }

    public AssetBundle _AssetBundle
    {
        get
        {
            return assetBundle;
        }
    }

    public List<string> GetDepences()
    {
        return depenceBundleList;
    }

    public void SetDepence(string[] depence)
    {
        depenceBundleList.AddRange(depence);
    }

    public ELoadState LoadState
    {
        get
        {
            return loadState;
        }
    }

    public List<string> GetDepence()
    {
        return depenceBundleList;
    }

    public List<string> GetRefrence()
    {
        return refrenceBundleList;
    }

    public void DebugAsset()
    {
        iABResLoader.DebugAllRes();
    }
}
