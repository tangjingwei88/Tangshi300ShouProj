using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiMain : MonoBehaviour {

    #region 引用

    public ActivityPanel theActivityPanel;
    public FriendPanel theFriendPanel;
    public GuidePanel theGuidePanel;
    public VipChargePanel theVipChargePanel;

    public GameObject theContentPanel;

    #endregion


    #region 变量

    Stack<UIState> uiStateStack = new Stack<UIState>();

    private UIState recentUIState = UIState.MainState;
    public UIState RecentUIState {
        get { return recentUIState; }
        set { recentUIState = value; }
    }

    #endregion

    private static UiMain instance;
    public static UiMain Instance {
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
        LoadingManager.Instance.theLoadingScencePanel.HideLoadingScreenPanel();
    }


    public void Apply()
    {
       
        theContentPanel.gameObject.SetActive(true);
    }

    public class UIPanelHelper
    {
        public UIState targetState;
        public string PrefabPath;
        public string typeName;

        public UIPanelHelper(UIState inputState,string inputPrefabPath,string inputTypeName)
        {
            targetState = inputState;
            PrefabPath = inputPrefabPath;
            typeName = inputTypeName;
        }
    }

    public List<UIPanelHelper> uiPanelList = new List<UIPanelHelper>() {
        new UIPanelHelper(UIState.ActivityState,"UIPrefab/UI/ActivityPanel","ActivityPanel"),
        new UIPanelHelper(UIState.FriendState,"UIPrefab/UI/FriendPanel","FriendPanel"),
        new UIPanelHelper(UIState.GuideState,"UIPrefab/UI/GuidePanel","GuidePanel"),
        new UIPanelHelper(UIState.VipRechargeState,"UIPrefab/UI/VipChargePanel","VipChargePanel"),
    };


    public GameObject GetHelp(UIState inputUIState,Component inputComponet)
    {
        if (inputComponet == null)
        {
            for (int i = 0; i < uiPanelList.Count; i++)
            {
                if (uiPanelList[i].targetState == inputUIState)
                {
                    GameObject obj = LoadingManager.NewUI(uiPanelList[i].PrefabPath);
                    GameObject targetGo = obj.GetComponent<UIMainLoadedPanel>().targetPanel;

                    targetGo.transform.parent = transform.GetChild(0);
                    targetGo.transform.localPosition = Vector3.zero;
                    targetGo.transform.localScale = Vector3.one;

                    Destroy(obj);
                    return targetGo;
                }
            }
        }

       return inputComponet.gameObject;
    }



    public void OnActivityBtnClick()
    {
        FadeToUIState(UIState.ActivityState);
    }


    public void FadeToUIState(UIState inputState)
    {
        if (inputState == UIState.MainState)
            uiStateStack.Clear();
        else if (inputState != recentUIState)
            uiStateStack.Push(inputState);

        FadeToUIStateWithOutPushToUIStateStack(inputState);
    }


    public void FadeToUIStateWithOutPushToUIStateStack(UIState targetState)
    {
        LeaveUIState(recentUIState);
        recentUIState = targetState;
        EnterUIState(targetState);
    }


    private void EnterUIState(UIState inputState)
    {
        if (inputState == UIState.MainState)
        {
            theContentPanel.gameObject.SetActive(true);
        }
        else if (inputState == UIState.ActivityState)
        {
            theActivityPanel = GetHelp(inputState, theActivityPanel).GetComponent<ActivityPanel>();
            theActivityPanel.gameObject.SetActive(true);
        }
        else if (inputState == UIState.GuideState)
        {
            theGuidePanel = GetHelp(inputState, theGuidePanel).GetComponent<GuidePanel>();
            theGuidePanel.gameObject.SetActive(true);
        }
        else if (inputState == UIState.FriendState)
        {
            theFriendPanel = GetHelp(inputState, theFriendPanel).GetComponent<FriendPanel>();
            theFriendPanel.gameObject.SetActive(true);
        }
        else if (inputState == UIState.VipRechargeState)
        {
            theVipChargePanel = GetHelp(inputState, theVipChargePanel).GetComponent<VipChargePanel>();
            theVipChargePanel.gameObject.SetActive(true);
        }
    }


    public void LeaveUIState(UIState targetState)
    {
        if (targetState == UIState.MainState)
        {
            theContentPanel.gameObject.SetActive(false);
        }
        else if (targetState == UIState.ActivityState)
        {
            theActivityPanel.gameObject.SetActive(false);
        }
        else if (targetState == UIState.FriendState)
        {
            theFriendPanel.gameObject.SetActive(false);
        }
        else if (targetState == UIState.GuideState)
        {
            theGuidePanel.gameObject.SetActive(false);
        }
        else if (targetState == UIState.VipRechargeState)
        {
            theVipChargePanel.gameObject.SetActive(false);
        }
    }

    public void ReturnToLastUIState()
    {
        LeaveUIState(recentUIState);
        //        uiStateStack.Pop();

        if (uiStateStack.Count > 0)
        {
            //栈顶的uistate和recentState一样需要pop掉，不然会进入两个一样的state
            if (recentUIState == uiStateStack.Peek())
            {
                uiStateStack.Pop();
            }

            if (uiStateStack.Count > 0)
            {
                recentUIState = uiStateStack.Pop();
                EnterUIState(recentUIState);

                if (recentUIState == UIState.ActivityState)
                {
                    UiMain.instance.theActivityPanel.Apply();
                }
                else if (recentUIState == UIState.FriendState)
                {
                    UiMain.instance.theFriendPanel.Apply();
                }
                else if (recentUIState == UIState.GuideState)
                {
                    UiMain.instance.theGuidePanel.Apply();
                }
                else if (recentUIState == UIState.VipRechargeState)
                {
                    UiMain.instance.theVipChargePanel.Apply();
                }


            }
            else
            {
                recentUIState = UIState.MainState;

                EnterUIState(recentUIState);
            }
        }
        else {
            recentUIState = UIState.MainState;
            
            EnterUIState(recentUIState);
        }
    }
}

public enum UIState
{
    MainState,
    VipRechargeState,
    GuideState,
    ActivityState,
    FriendState,
}
