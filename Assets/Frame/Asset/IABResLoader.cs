using System;
using UnityEngine;
using System.Collections;
public class IABResLoader:IDisposable{
    
    private AssetBundle ABRes;

    public IABResLoader(AssetBundle tmpBundle)
    {
        ABRes = tmpBundle;
    }

    // 加载单个assetbundle
    public UnityEngine.Object this[string resName]
    {
        get
        {
            UnityEngine.Object asset = null;
            if (ABRes != null & ABRes.Contains(resName))
            {
                asset = ABRes.LoadAsset(resName);
            }
            else
            {
                Debug.Log("res not contain " + resName);
            }
            return asset;
        }
    }

    public UnityEngine.Object[] LoadResources(string resName)
    {
        UnityEngine.Object[] asset = null;
        if (ABRes != null & ABRes.Contains(resName))
        {
            asset = ABRes.LoadAssetWithSubAssets(resName);
        }
        else
        {
            Debug.Log("res not contain " + resName);
        }
        return asset;
    }

    // 释放已经加载出来的资源
    public void UnLoadRes(UnityEngine.Object resObj)
    {
        Resources.UnloadAsset(resObj);
    }

    // 卸载assetbundle(如果忘记释放) TODO 这里并没有使用true
    public void Dispose()
    {
        ABRes.Unload(false);
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
