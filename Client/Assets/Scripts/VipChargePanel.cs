using UnityEngine;
using System.Collections;

public class VipChargePanel : MonoBehaviour {

    public void Apply()
    {

    }

    public void OnBackBtnClick()
    {
        UiMain.Instance.ReturnToLastUIState();
    }
}
