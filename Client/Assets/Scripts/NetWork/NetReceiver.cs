using UnityEngine;
using System.Collections;
using chanyu;

public class NetReceiver {

    private static NetReceiver instance;
    public static NetReceiver Instance {
        get {
            if (instance == null) {
                instance = new NetReceiver();
            }
            return instance;
        }
    }

    public NetReceiver() { }

    public void Update()
    {
        DisposeProtoMsg();
    }


    #region 消息接收处理
    public void DisposeProtoMsg()
    {
        while (true)
        {
            ProtoMessage protoMsg = Client.Instance.Receive();
            if (protoMsg == null)
                break;

            switch (protoMsg.Type)
            {
                case ProtoMsgDefine.S2C_ENTERGAME:
                    {
                        Debug.LogError("S2C_ENTERGAME");
                        EnterGameProto msg = Client.Instance.DeserializeMsg<EnterGameProto>(protoMsg);
                        NetReceiverHandler.Instance.Handle_EnterGame(ClientClass.S2C_Build_EnterGame(msg));
                    }
                    break;
            }
        }
    }

    #endregion

}
