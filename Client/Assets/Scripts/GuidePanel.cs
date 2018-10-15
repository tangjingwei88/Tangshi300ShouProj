using UnityEngine;
using System.Collections;

public class GuidePanel : MonoBehaviour {

    public void Apply()
    {

    }


    public void OnActivityBtnClick()
    {
        UiMain.Instance.FadeToUIState(UIState.ActivityState);
    }

    public void OnVipChargeBtnClick()
    {
        UiMain.Instance.FadeToUIState(UIState.VipRechargeState);
    }

    public void OnBackBtnClick()
    {
        UiMain.Instance.ReturnToLastUIState();
    }
}
