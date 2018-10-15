using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour {

    private static LoadingManager instance;
    public LoadingScenePanel theLoadingScencePanel;
    public static LoadingManager Instance {
        get {
            return instance;
        }
    }


   void Awake()
    {
        instance = this;
    }


    void Start()
    {
        DontDestroyOnLoad(theLoadingScencePanel);
    }


    public void StartLoadScence(string targetScenceName)
    {
        //StartCoroutine(WaitLoadingTargetScences(targetScenceName));
        theLoadingScencePanel.WaitingLoadingScence(targetScenceName);
    }


    private IEnumerator WaitLoadingTargetScences(string scencesName)
    {
        AsyncOperation async = Application.LoadLevelAsync(scencesName);
        Debug.LogError(async.progress);

        yield return async;
    }


    public static GameObject NewUI(string name)
    {
        Object obj = Resources.Load(name);
        if (obj == null)
        {
            Debug.LogError(name + "is not exist");
            return null;
        }

        return (GameObject)Object.Instantiate(obj);
    }
}
