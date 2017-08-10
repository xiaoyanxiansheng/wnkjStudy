using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IABScenceManager{

    IABManager aBManager;

    public IABScenceManager(string scenceName)
    {
        aBManager = new IABManager(scenceName);
    }

    private Dictionary<string, string> allAsset = new Dictionary<string, string>();

    public void LoadAsset(string bundleName, LoaderProgress progress, LoadAssetBundleCallBack callBack)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            string tmpValue = allAsset[bundleName];
            aBManager.LoadAssetBundle(tmpValue, progress, callBack);
        }
        else
        {

        }
    }

    public IEnumerator LoadAssetBundle(string bundleName)
    {
        yield return aBManager.LoadAssetBundle(bundleName);
    }

    public Object GetSingleResources(string bundleName, string resName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            return aBManager.GetSingleResource(allAsset[bundleName], resName);
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
            return aBManager.GetMultiResource(allAsset[bundleName], resName);
        }
        else
        {
            return null;
        }
    }

    public void DisposeBundleResObj(string bundleName,string res)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            aBManager.DisposeResObj(allAsset[bundleName], res);
        }
    }

    public void DisposeBundleRes(string bundleName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            aBManager.DisposeResObj(allAsset[bundleName]);
        }
    }

    public void DisposeAllObj()
    {
        aBManager.DisposeAllObj();
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
        allAsset.Clear();
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

    public bool IsLoadingFinish(string bundleName)
    {
        if (allAsset.ContainsKey(bundleName))
        {
            return aBManager.IsLoadFinish(bundleName);
        }
        return false;
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
