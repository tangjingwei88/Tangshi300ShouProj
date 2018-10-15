using UnityEngine;
using System.Collections;

public class FriendPanel : MonoBehaviour {

    public void Apply()
    {

    }


    public void OnGuidBtnClick()
    {
        UiMain.Instance.FadeToUIState(UIState.GuideState);
    }


    public void OnBackBtnClick()
    {
        UiMain.Instance.ReturnToLastUIState();
    }
}
