using UnityEngine;
using System.Collections;
using System.IO;
public class IABTools{

    public static string GetPlatformFoldName(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "IOS";
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                return "Windows";
            default:
                return null;
        }
    }

    public static string GetAppFilePath(){
        string tmpPath = "";
        if(Application.platform == RuntimePlatform.WindowsEditor | Application.platform == RuntimePlatform.OSXEditor)
        {
            tmpPath = Application.streamingAssetsPath;
        }
        else
        {
            tmpPath = Application.persistentDataPath;
        }
        return tmpPath;
    }

    public static string GetAssetBundlePath(string bundleName){
        string allPath = GetAppFilePath();
        allPath = Path.Combine(allPath, bundleName);
        allPath = ToFilePath(PathTanslate(allPath));
        return allPath;
    }

    public static string GetBaseBundlePath() 
    {
        return GetAppFilePath();
    }

    public static string GetManifestBundlePath() 
    {
        return GetAssetBundlePath("StreamingAssets");
    }

    public static string ToFilePath(string path)
    {
        return "file://" + IABTools.PathTanslate(path);
    }

    public static string PathTanslate(string path)
    {
        return path.Replace("\\","/");
    }
}
