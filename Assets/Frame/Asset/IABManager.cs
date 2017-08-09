using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void LoadAssetBundleCallBack(string scenceName,string bundleName);

// 加载的资源
public class AssetObj
{
    public List<Object> objs;

    public AssetObj(params Object[] tmpObj)
    {
        objs = new List<Object>();

        objs.AddRange(tmpObj);
    }

    // TODO 释放接口偏多
    public void ReleaseObj()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            Resources.UnloadAsset(objs[i]);
        }
    }
}

// 一个bundle中的资源
public class AssetResObj
{
    public Dictionary<string, AssetObj> resObjs;

    public AssetResObj(string name, AssetObj tmp)
    {
        resObjs = new Dictionary<string, AssetObj>();
        resObjs.Add(name,tmp);
    }

    public void AddResObj(string name, AssetObj tmpObj)
    {
        resObjs.Add(name, tmpObj);
    }

    public void ReleaseAllResObj()
    {
        List<string> keys = new List<string>();

        keys.AddRange(resObjs.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            ReleaseResObj(keys[i]);
        }
    }

    public void ReleaseResObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetObj tmp = resObjs[name];
            tmp.ReleaseObj();
        }
        else
        {
            Debug.Log("resObjs not key name " + name);
        }
    }

    public List<Object> GetResObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            return resObjs[name].objs;
        }
        else
        {
            return null;
        }
    }
}

public class IABManager{

    // 所有的bundle
    Dictionary<string, IABRelation> aBRelationList = new Dictionary<string, IABRelation>();

    // bundle和资源的对应关系
    Dictionary<string, AssetResObj> Bundle2ResObjList = new Dictionary<string, AssetResObj>();

    public void DebugAsset(string bundleName)
    {
        if (aBRelationList[bundleName] != null)
        {
            IABRelation iABRelation = aBRelationList[bundleName];
            iABRelation.DebugAsset();
        }
    }

    private string scenceName;
    public IABManager(string tmpName)
    {
        scenceName = tmpName;
    }

    // 资源是否加载完成
    public bool IsLoadFinish(string bundleName)
    {
        if (aBRelationList[bundleName] != null)
        {
            IABRelation iABRelation = aBRelationList[bundleName];
            return iABRelation.IsloadFinish;
        }
        Debug.Log("aBRelationList not " + bundleName);
        return false;
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

    // TODO 这里逻辑写的有点混乱 获取某个加载资源
    public Object GetSingleResource(string bundleName, string resName)
    {
        // 
        if (Bundle2ResObjList.ContainsKey(bundleName))
        {
            AssetResObj tmpRes = Bundle2ResObjList[bundleName];

            List<Object> tmpObj = tmpRes.GetResObj(resName);

            if (tmpObj!=null) 
            {
                // TODO 不理解
                return tmpObj[0];
            }
        }

        if (aBRelationList.ContainsKey(bundleName))
        {
            IABRelation iABRelation = aBRelationList[bundleName];

            Object tmpObj = iABRelation.GetResource(resName);

            AssetObj tmpAssetObj = new AssetObj(tmpObj);

            if (Bundle2ResObjList.ContainsKey(bundleName))
            {
                AssetResObj tmpRes = Bundle2ResObjList[bundleName];

                tmpRes.AddResObj(resName, tmpAssetObj);
            }
            else
            {
                AssetResObj tmpRes = new AssetResObj(resName, tmpAssetObj);

                Bundle2ResObjList.Add(bundleName, tmpRes);
            }

            return tmpObj;
        }
        else
        {
            return null;
        }
    }

    public Object[] GetMultiResource(string bundleName, string resName)
    {
        // 
        if (Bundle2ResObjList.ContainsKey(bundleName))
        {
            AssetResObj tmpRes = Bundle2ResObjList[bundleName];

            List<Object> tmpObj = tmpRes.GetResObj(resName);

            if (tmpObj != null)
            {
                // TODO 不理解
                return tmpObj.ToArray();
            }
        }

        if (aBRelationList.ContainsKey(bundleName))
        {
            IABRelation iABRelation = aBRelationList[bundleName];

            Object[] tmpObj = iABRelation.GetMutiResource(resName);

            AssetObj tmpAssetObj = new AssetObj(tmpObj);

            if (Bundle2ResObjList.ContainsKey(bundleName))
            {
                AssetResObj tmpRes = Bundle2ResObjList[bundleName];

                tmpRes.AddResObj(resName, tmpAssetObj);
            }
            else
            {
                AssetResObj tmpRes = new AssetResObj(resName, tmpAssetObj);

                Bundle2ResObjList.Add(bundleName, tmpRes);
            }

            return tmpObj;
        }
        else
        {
            return null;
        }
    }

    public void DisposeResObj(string bundleName,string resName)
    {
        if (Bundle2ResObjList.ContainsKey(bundleName))
        {
            AssetResObj tmpObj = Bundle2ResObjList[bundleName];
            tmpObj.ReleaseResObj(resName);
        }
    }

    public void DisposeResObj(string bundleName)
    {
        if (Bundle2ResObjList.ContainsKey(bundleName))
        {
            AssetResObj tmpObj = Bundle2ResObjList[bundleName];
            tmpObj.ReleaseAllResObj();
        }
    }

    public void DisposeAllObj()
    {
        List<string> keys = new List<string>();
        keys.AddRange(Bundle2ResObjList.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            DisposeResObj(keys[i]);
        }
        Bundle2ResObjList.Clear();
    }


    // TODO 并没有地方引用
    public void LoadAssetBundle(string bundleName, LoaderProgress progress, LoadAssetBundleCallBack callback)
    {
        if (!aBRelationList.ContainsKey(bundleName))
        {
            IABRelation iABRelation = new IABRelation();
            iABRelation.Inital(bundleName, progress);

            aBRelationList.Add(bundleName, iABRelation);

            callback(scenceName, bundleName);
        }
    }

    string[] GetDepences(string bundleName)
    {
        return IABManifestLoader.Instance.GetDepences(bundleName);
    }

    public IEnumerator LoadAssetBundleDepences(string bundleName, string refrenceName, LoaderProgress progress)
    {
        if (!aBRelationList.ContainsKey(bundleName))
        {
            IABRelation loader = new IABRelation();
            loader.Inital(bundleName,progress);

            loader.AddRefrence(refrenceName);

            aBRelationList.Add(bundleName, loader);

            yield return LoadAssetBundle(bundleName);
        }
        else
        {
            IABRelation loader = aBRelationList[bundleName];
            loader.AddRefrence(refrenceName);
        }
    }

    public IEnumerator LoadAssetBundle(string bundleName)
    {
        while (!IABManifestLoader.Instance.IsLoadFinish())
        {
            yield return null;
        }

        // 需要加载的bundle
        IABRelation loader = aBRelationList[bundleName];
        // 先加载依赖
        string[] depences = GetDepences(bundleName);

        loader.SetDepence(depences);

        for (int i = 0; i < depences.Length; i++)
        {
            yield return LoadAssetBundleDepences(depences[i], bundleName, loader.GetProgress());
        }

        yield return loader.LoadAssetBundle();
    }
}
