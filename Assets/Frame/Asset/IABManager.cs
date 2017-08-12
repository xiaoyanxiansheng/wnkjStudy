using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void LoadAssetBundleCallBack(string bundleName);

public delegate void LoadAssetBundleObjCallBack(Object resObj);

public class IABManager{

    // 所有的bundle
    Dictionary<string, IABLoader> aBRelationList = new Dictionary<string, IABLoader>();

    public IABManager()
    {

    }

    // bundle的加载状态
    public ELoadState BundleLoadState(string bundleName)
    {
        ELoadState state = ELoadState.notLoad;
        if (aBRelationList.ContainsKey(bundleName) != null)
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            state = iABLoader.LoadState;
        }
        return state;
    }

    // 资源是否加载
    public bool IsLoadAssetBundle(string bundleName) 
    {
        if (aBRelationList.ContainsKey(bundleName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Object GetBunldeRes(string bundleName, string resName)
    {
        Object asset = null;
        if (aBRelationList.ContainsKey(bundleName))
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            asset = iABLoader.GetBundleResAndNotLoad(resName);
        }
        else
        {
            // TODO 加载bundle 这里无法开启协程
        }

        return asset;
    }

    public Object[] GetBunldeMultiRes(string bundleName, string resName)
    {
        Object[] asset = null;
        if (aBRelationList.ContainsKey(bundleName))
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            asset = iABLoader.GetBundleMultiRes(resName);
        }
        else
        {
            // TODO 加载bundle 这里无法开启协程
        }

        return asset;
    }

    /// <summary>
    /// 释放bundle 加载出来的obj
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="resObj"></param>
    public void DisposeRes(string bundleName,Object resObj)
    {
        if (aBRelationList.ContainsKey(bundleName))
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            iABLoader.UnLoadRes(resObj);
        }
    }
    /// <summary>
    /// 释放bundle加载出来的所有obj
    /// </summary>
    /// <param name="bundleName"></param>
    public void DisposeBundleAllRes(string bundleName)
    {
        if (aBRelationList.ContainsKey(bundleName))
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            iABLoader.UnLoadAllRes();
        }
    }

    public void DisposeAllRes()
    {
        List<string> keys = new List<string>();
        keys.AddRange(aBRelationList.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            DisposeBundleAllRes(aBRelationList[keys[i]].BundleName);
        }
    }

    public string[] GetDepences(string bundleName)
    {
        return IABManifestLoader.Instance.GetDepences(bundleName);
    }

    public IEnumerator LoadAssetBundleDepences(string bundleName, string refrenceName)
    {
        if (!aBRelationList.ContainsKey(bundleName))
        {
            IABLoader iABLoader = new IABLoader(bundleName);

            iABLoader.AddRefrence(refrenceName);

            aBRelationList.Add(bundleName, iABLoader);

            yield return LoadBundle(bundleName);
        }
        else
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            iABLoader.AddRefrence(refrenceName);
        }
    }

    public IEnumerator LoadBundle(string bundleName)
    {
        IABLoader iABLoader;
        if (aBRelationList.ContainsKey(bundleName))
        {
            iABLoader = aBRelationList[bundleName];
        }
        else
        {
            iABLoader = new IABLoader(bundleName);
            aBRelationList.Add(bundleName, iABLoader);
        }
        // 先加载依赖
        string[] depences = GetDepences(bundleName);

        iABLoader.SetDepence(depences);

        for (int i = 0; i < depences.Length; i++)
        {
            yield return LoadAssetBundleDepences(depences[i], bundleName);
        }
        Debug.Log("LoadBundle depence " + iABLoader.BundleName);
        yield return iABLoader.LoadBundle();
    }

    public void DisposeBundle(string bundleName)
    {
        if (aBRelationList.ContainsKey(bundleName))
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            List<string> refrences = iABLoader.GetDepence();
            for (int i = 0; i < refrences.Count; i++)
            {
                if (aBRelationList.ContainsKey(refrences[i]))
                {
                    IABLoader refrence = aBRelationList[refrences[i]];
                    if (refrence.RemoveRefrence(bundleName))
                    {
                        DisposeBundle(refrence.BundleName);
                    }
                }
            }

            if (iABLoader.GetRefrence().Count == 0)
            {
                iABLoader.Dispose();
                aBRelationList.Remove(bundleName);
            }
        }
    }

    public void DisposeAllBundleAndRes()
    {
        List<string> keys = new List<string>();

        keys.AddRange(aBRelationList.Keys);

        for (int i = 0; i < aBRelationList.Count; i++)
        {
            IABLoader iABLoader = aBRelationList[keys[i]];
            iABLoader.UnLoadBundleAndRes();
        }
        aBRelationList.Clear();
    }

    public void DisposeAllBundle()
    {

    }

    public void DebugAsset(string bundleName)
    {
        if (aBRelationList[bundleName] != null)
        {
            IABLoader iABLoader = aBRelationList[bundleName];
            iABLoader.DebugAsset();
        }
    }

    public void DebugAllAsset()
    {
        foreach (string bundleName in aBRelationList.Keys)
        {
            DebugAsset(bundleName);
        }
    }
}
