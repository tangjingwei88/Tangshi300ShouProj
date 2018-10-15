using UnityEngine;
using System.Collections;

public class LoginGame : MonoBehaviour {

    public LoadingScenePanel theLoadingScenePanel;


    public void OnLoginBtnClick()
    {
        LoadingManager.Instance.StartLoadScence("MainScence");
        //theBlackScreenPanel.WaitingLoadingScence("MainScence");

        Client.Instance.Connect("127.0.0.1",65535);
        NetSender.Instance.SEND_LOGIN("firt","soft","aa",1,1,"hello","cc","ss");
    }




}
