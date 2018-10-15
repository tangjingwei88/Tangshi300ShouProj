using UnityEngine;
using System.Collections;
using chanyu;

public class NetSender {

    private static NetSender instance;
    public static NetSender Instance {
        get
        {
            if (instance == null)
            {
                instance = new NetSender();
            }
            return instance;
        }
    }


    NetSender() { }


    #region 发送协议

    public void SEND_LOGIN(string sessionID,string userID,string userType,int serverID,int thirdPartyID,string extraStr,string systemInfo,string versionString)
    {
        Client.Instance.Send(Build_LoginProto(sessionID,userID,userType,serverID,thirdPartyID,extraStr,systemInfo,versionString));
    }

    #endregion




    #region 协议生成

    private ProtoMessage Build_LoginProto(string sessionID,string userID,string userType,int serverID,int thirdPartyID,string extraStr,
                                        string systeminfo,string versionString)
    {
        LoginProto proto = new LoginProto
        {
            loginsession = sessionID,
            userid = userID,
            usertype = userType,
            serverid = serverID,
            thirdpartyid = thirdPartyID,
            stringValue1 = extraStr,
            systemInfo = systeminfo,
            clientVersion = versionString
        };

        return Client.Instance.SerializeMsg<LoginProto>(ProtoMsgDefine.C2S_LOGIN,proto);
    }

    #endregion
}
