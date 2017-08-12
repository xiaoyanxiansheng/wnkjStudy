using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IABScenceManager{

    IABManager aBManager;

    public IABScenceManager()
    {
        aBManager = new IABManager();
    }

    private Dictionary<string, string> allAsset = new Dictionary<string, string>();

    public void LoadAsset(string bundleName, LoadAssetBundleCallBack callBack)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            string tmpValue = allAsset[bundleName];
            aBManager.LoadBundle(tmpValue);
        }
        else
        {

        }
    }

    public IEnumerator LoadAssetBundle(string bundleName)
    {
        yield return aBManager.LoadBundle(bundleName);
    }

    public Object GetSingleResources(string bundleName, string resName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            return aBManager.GetBunldeRes(allAsset[bundleName], resName);
        }
        else
        {
            return null;
        }
    }

    public Object[] GetMultiResource(string bundleName, string resName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            return aBManager.GetBunldeMultiRes(allAsset[bundleName], resName);
        }
        else
        {
            return null;
        }
    }

    public void DisposeBundleResObj(string bundleName,Object resObj)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            aBManager.DisposeRes(allAsset[bundleName], resObj);
        }
    }

    public void DisposeBundleAllRes(string bundleName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            aBManager.DisposeBundleAllRes(allAsset[bundleName]);
        }
    }

    public void DisposeAllRes()
    {
        aBManager.DisposeAllRes();
    }

    public void DisposeBundle(string bundleName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            aBManager.DisposeBundle(bundleName);
        }
    }

    public void DisposeAllBundle()
    {
        aBManager.DisposeAllBundle();
    }

    public void DisposeAllBundleAndRes()
    {
        aBManager.DisposeAllBundleAndRes();
        allAsset.Clear();
    }

    public void DebugAllAsset()
    {
        List<string> keys = new List<string>();
        keys.AddRange(allAsset.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            aBManager.DebugAsset(allAsset[keys[i]]);
        }
    }

    public ELoadState BundleLoadState(string bundleName)
    {
        return aBManager.BundleLoadState(bundleName);
    }

    public bool IsLoadingAssetBundle(string bundleName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            return aBManager.IsLoadAssetBundle(bundleName);
        }
        return false;
    }
}
