using UnityEngine;
using System.Collections;
using chanyu;

public class ClientClass{

    #region C2S

    public static MailSendProto C2S_Build_MailSendRequest(int targetId,string title,string content)
    {
        MailSendProto proto = new MailSendProto();
        proto.targetId = targetId;
        proto.title = title;
        proto.content = content;

        return proto;
    }


    #endregion



    #region S2C

    #region EnterGame

    public static C_PlayerInfo S2C_Build_EnterGame(EnterGameProto proto)
    {
        C_PlayerInfo msg = new C_PlayerInfo();
        msg.id = proto.cid;
        msg.name = proto.name;
        msg.isMale = proto.isMale;
        msg.gold = proto.gold;
        msg.crystal = proto.crystal;
        msg.diamond = proto.diamond;
        msg.stamina = proto.stamina;
        msg.lv = proto.lv;
        msg.exp = proto.exp;
        msg.icon = proto.icon;
        msg.vipLevel = proto.viplevel;

        return msg;
    }

    #endregion




    #endregion
}


public class C_PlayerInfo
{
    public int id;              //id
    public string name;         //名字
    public bool isMale;         //性别
    public double gold;         //金币
    public int crystal;         //水晶
    public int diamond;         //钻石
    public int stamina;         //体力
    public int lv;              //等级
    public double exp;          //经验值
    public int icon;            //头像Id
    public int vipLevel;        //VIP等级

}