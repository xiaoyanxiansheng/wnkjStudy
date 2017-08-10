using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IABRelation{

    // 依赖关系
    private List<string> depenceBundleList;
    // 引用关系
    private List<string> refrenceBundleList;

    IABLoader iABLoader;

    private string theBundleName;

    private LoaderProgress progress;

    public string GetBundleName()
    {
        return theBundleName;
    }

    public List<string> GetDepence()
    {
        return depenceBundleList;
    }

    public IABRelation()
    {
        depenceBundleList = new List<string>();
        refrenceBundleList = new List<string>();
    }

    public void AddRefrence(string bundleName)
    {
        refrenceBundleList.Add(bundleName);
    }

    public List<string> GetRefrenceBundleList()
    {
        return refrenceBundleList;
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

    public void SetDepence(string[] depence)
    {
        depenceBundleList.AddRange(depence);
    }

    public bool RemoveDepence(string bundleName)
    {
        for (int i = 0; i < depenceBundleList.Count; i++)
        {
            if (depenceBundleList[i].Equals(bundleName))
            {
                depenceBundleList.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    private bool isLoadFinish = false;
    public void BundleLoadFinish(string bundleName)
    {
        isLoadFinish = true;
    }

    public string getBundleName()
    {
        return theBundleName;
    }

    public void Inital(string bundleName,LoaderProgress tmpProgress)
    {
        progress = tmpProgress;
        isLoadFinish = false;
        theBundleName = bundleName;
        iABLoader = new IABLoader(tmpProgress, BundleLoadFinish);
    }

    public LoaderProgress GetProgress()
    {
        return progress;
    }

    #region  TODO 这里不应该处理bundle的 卸载资源的获取
    public void Dispose()
    {
        iABLoader.Dispose();
    }

    public IEnumerator LoadAssetBundle()
    {
        yield return iABLoader.CommonLoad();
    }

    public Object GetResource(string bundleName)
    {
        return iABLoader.GetResource(bundleName);
    }

    public Object[] GetMutiResource(string bundleName)
    {
        return iABLoader.GetMutiRersource(bundleName);
    }

    public void DebugAsset()
    {
        iABLoader.DebugLoader();
    }
    #endregion

    public bool IsloadFinish 
    {
        get
        {
            return isLoadFinish;
        }
    }
}
