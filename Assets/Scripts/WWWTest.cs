using UnityEngine;
using System.Collections;

public class WWWTest : MonoBehaviour {

    public string url = "file://D:/unityWork/WNKJ/Assets/StreamingAssets/cubetest1";
    private WWW www = null;
    void Start()
    {
        StartCoroutine(StartWWW());
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
