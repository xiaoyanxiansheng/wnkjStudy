using UnityEngine;
using System.Collections;

public class WWWTest : MonoBehaviour {

    private string url = "file://D:/unityWork/WNKJ/Assets/StreamingAssets/cubetest1";

    public string bundleName = "cubetest1";

    private WWW www = null;

    public void BundleCall(string bundleName)
    {
        Debug.Log("aaaaaaaaaaaaa");
    }

    void Start()
    {
        //StartCoroutine(StartWWW());

        IABLoaderManager.Instance.LoadBundle(bundleName, BundleCall);
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
