using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAssetBundleAsync(Application.streamingAssetsPath + "/Window/AB/newquad.asset"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadAssetBundle(string path)
    {

    }

    public IEnumerator LoadAssetBundleAsync(string path)
    {
        //manifest of face

        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(path);

        // why yield
        yield return abcr;

        if (abcr.isDone)
        {
            AssetBundle ab = abcr.assetBundle;
            string[] names = ab.GetAllAssetNames();

            foreach (var name in names)
            {
                AssetBundleRequest abr = ab.LoadAllAssetsAsync();

                yield return abr;

                Object[] assets = abr.allAssets;

                foreach (var item in assets)
                {
                    GameObject o = GameObject.Instantiate<GameObject>(item as GameObject);
                }
            }
        }

    }
}
