using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadAssetBundle : MonoBehaviour
{
    AssetBundle bundle;


    public void LoadGame()
    {
        LoadAssetBundleFunc();
    }
    void LoadAssetBundleFunc()
    {
        bundle = AssetBundle.LoadFromFile(@"C:\Users\raksh\Desktop\AssetBundles\scenes");

        string[] paths = bundle.GetAllScenePaths();
        SceneManager.LoadScene(paths[0]);
    }
}
