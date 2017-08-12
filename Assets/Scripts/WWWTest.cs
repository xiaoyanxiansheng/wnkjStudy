using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WWWTest : MonoBehaviour {

    private string url = "file://D:/unityWork/WNKJ/Assets/StreamingAssets/cubetest1";

    private string bundleName = "";

    private WWW www = null;

    public void BundleCall(string bundleName)
    {
        Debug.Log("LoadBundle 4 " + bundleName);
    }

    public void BundleAssetCall(Object resObj)
    {
        GameObject obj = Instantiate(resObj) as GameObject;
        obj.transform.parent = transform;
    }

    void Start()
    {
        //StartCoroutine(StartWWW());

        LoadBundleTest();

        //StartCoroutine(test3());
    }

    /// <summary>
    /// TODO 未完善
    /// 其实可以记录每个资源所在的bundle包中，这样加载就不需要知道bundleName了
    /// </summary>
    public void LoadBundleTest()
    {
        string[,] a = new string[2, 2]{
            {"cubetest1","Cube"},
            {"cubetest2","Cube 1"}
        };

        for (int i = 0; i < a.GetLength(0); i++)
        {
            string bundleName = a[i, 0];
            string resName = a[i, 1];
            Debug.Log("LoadBundle start 0 " + bundleName);
            IABLoaderManager.Instance.LoadBundleAsset(bundleName, resName, BundleAssetCall);
        }
    }

    IEnumerator test1()
    {
        Debug.Log("aaaaaaaaaaaaaaa1");
        yield return 1;
        Debug.Log("aaaaaaaaaaaaaaa11");
        yield return 1;
        Debug.Log("aaaaaaaaaaaaaaa111");
    }

    IEnumerator test2()
    {
        Debug.Log("aaaaaaaaaaaaaaa2");
        yield return test1();
        Debug.Log("aaaaaaaaaaaaaaa22");
        yield return 10000;
        Debug.Log("aaaaaaaaaaaaaaa222");
    }

    IEnumerator test3()
    {
        Debug.Log("aaaaaaaaaaaaaaa3");
        yield return test2();
        Debug.Log("aaaaaaaaaaaaaaa33");
    }

    IEnumerator StartWWW()
    {
        www = new WWW(url);
        while (www.progress != 1.0) 
        {
            Debug.Log(Time.deltaTime + "aaaaaaaaa " + www.progress);
        }
        yield return www;
        Debug.Log(Time.deltaTime + "aaaaaaaaa " + www.progress);
        //GameObject go = Instantiate();
        AssetBundle assetBundle = www.assetBundle;
        Object go = assetBundle.LoadAsset("Cube");
        //GameObject obj = Instantiate(go) as GameObject;
        //obj.transform.parent = transform;
        assetBundle.Unload(true);
        www.Dispose();
    }

    

    void Update()
    {

    }
}
