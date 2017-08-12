using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IABResLoader:IDisposable{
    
    private List<UnityEngine.Object> loadResObjs;

    private IABLoader iABLoader;

    private AssetBundle ABRes;

    public IABResLoader(IABLoader tmpIABLoader)
    {
        iABLoader = tmpIABLoader;
        ABRes = iABLoader._AssetBundle;
    }

    // 加载单个assetbundle
    public UnityEngine.Object GetBundleRes(string resName)
    {
        for (int i = 0; i < loadResObjs.Count; i++)
        {
            UnityEngine.Object res = loadResObjs[i];
            if (resName == loadResObjs[i].name)
            {
                return res;
            }
        }
        return null;
    }

    public void LoadBundleRes(string resName)
    {
        bool isIn = false;
        for (int i = 0; i < loadResObjs.Count; i++)
        {
            if (resName == loadResObjs[i].name)
            {
                isIn = true;
                break;
            }
        }
        if (!isIn)
        {
            loadResObjs.Add(ABRes.LoadAsset(resName));
        }
    }

    public UnityEngine.Object[] LoadAssetWithSubAssets(string resName)
    {
        UnityEngine.Object[] asset = null;
        if (ABRes != null & ABRes.Contains(resName))
        {
            asset = ABRes.LoadAssetWithSubAssets(resName);
            loadResObjs.AddRange(asset);
        }
        else
        {
            Debug.Log("res not contain " + resName);
        }
        return asset;
    }

    public List<UnityEngine.Object> GetLoadResObjs()
    {
        return loadResObjs;
    }

    public void UnLoadAllRes()
    {
        for (int i = 0; i < loadResObjs.Count; i++)
        {
            UnLoadRes(loadResObjs[i]);
        }
        loadResObjs = null;
    }

    // 释放已经加载出来的资源
    public void UnLoadRes(UnityEngine.Object resObj)
    {
        if (loadResObjs.Contains(resObj))
        {
            loadResObjs.Remove(resObj);
            Resources.UnloadAsset(resObj);
        }
        else
        {
            Debug.Log(iABLoader.BundleName + " not resObj " + resObj.name);
        }
    }

    public void Dispose()
    {
        Debug.Log("IABResLoader Dispose");
    }

    public void DebugAllRes()
    {
        string[] tmpAssetNames = ABRes.GetAllAssetNames();
        for (int i = 0; i < tmpAssetNames.Length; i++)
        {
            Debug.Log("ABRes Contain Asset Name = " + tmpAssetNames[i]);
        }
    }
}
