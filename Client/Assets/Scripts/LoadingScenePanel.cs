using UnityEngine;
using System.Collections;

public class LoadingScenePanel : MonoBehaviour {

    public UISlider LoadingSlider;
    private AsyncOperation async;
    public UILabel LoadingText;
    private float targetValue;
    private float loadingSpeed = 1;

    public void WaitingLoadingScence(string sceneName)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(WaitLoadingTargetScences(sceneName));
    }



    private IEnumerator WaitLoadingTargetScences(string scencesName)
    {
        async = Application.LoadLevelAsync(scencesName);
        Debug.LogError(async.progress);
        //阻止当前加载完成自动切换
        async.allowSceneActivation = false;

        yield return async;
    }

    void Update()
    {
        targetValue = async.progress;

        if (targetValue >= 0.9f)
        {
            targetValue = 1.0f;
        }

        if (targetValue != LoadingSlider.value)
        {
            LoadingSlider.value = Mathf.Lerp(LoadingSlider.value,targetValue,Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(LoadingSlider.value - targetValue) < 0.01f)
            {
                LoadingSlider.value = targetValue;
            }
        }

        LoadingText.text = ((int)(LoadingSlider.value * 100)).ToString() + "%";
        if ((int)(LoadingSlider.value * 100) == 100)
        {
            //允许异步加载完成后自动切换场景
            async.allowSceneActivation = true;
            
        }
    }


    public void HideLoadingScreenPanel()
    {
        this.gameObject.SetActive(false);
    }
}
