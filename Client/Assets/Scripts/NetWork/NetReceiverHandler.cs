using UnityEngine;
using System.Collections;

public class NetReceiverHandler {

    private static NetReceiverHandler instance;
    public static NetReceiverHandler Instance {
        get
        {
            if (instance == null)
            {
                instance = new NetReceiverHandler();
            }
            return instance;
        }
    }

    private NetReceiverHandler() { }


    #region 消息处理
    public void Handle_EnterGame(C_PlayerInfo playerInfo)
    {
        GameData.Instance.playerInfo = playerInfo;
    }


    #endregion
}
