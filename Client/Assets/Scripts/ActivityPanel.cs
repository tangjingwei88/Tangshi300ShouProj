using UnityEngine;
using System.Collections;

public class ActivityPanel : MonoBehaviour {


    public void Apply()
    {


    }


    public void OnFriendBtnClick()
    {
        UiMain.Instance.FadeToUIState(UIState.FriendState);
    }

    public void OnBackBtnClick()
    {
        UiMain.Instance.ReturnToLastUIState();
    }
}
