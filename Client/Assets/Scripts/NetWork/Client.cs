using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;
using ProtoBuf;

public class Client {

    private static readonly Client instance = new Client();
    public static  Client Instance {
        get {
            return instance;
        }
    }

    #region 变量
    //接收消息队列
    private ReceiveProtoMessageQueue recvMsgQueue = new ReceiveProtoMessageQueue();
    //发送消息队列
    private SendProtoMessageQueue sendMsgQueue = new SendProtoMessageQueue();
    //与服务器连接通道
    private TcpClient tcpClient = null;
    //与服务器连接的数据流
    private Stream stream = null;
    //接收消息线程
    private Thread recvMsgThread = null;
    //发送消息线程
    private Thread sendMsgThread = null;

    //断线编号
    private int SERVER_DISCONNECTED = -1;

    private int CLIENT_ERROR = -100;

    #endregion


    static Client()
    {
    }

    Client() { }


    #region 系统对外接口

    public bool Connect(string host, int port)
    {
        if (tcpClient == null)
        {
            try
            {
                tcpClient = new TcpClient(host, port);
                tcpClient.NoDelay = true;
                stream = tcpClient.GetStream();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Connect error!");
                Debug.LogError(ex.ToString());
                Debug.LogError(ex.StackTrace);
                return false;
            }

            //启动收发线程
            recvMsgThread = new Thread(new ThreadStart(RecvMsgLoop));
            sendMsgThread = new Thread(new ThreadStart(SendMsgLoop));

            recvMsgThread.Start();
            sendMsgThread.Start();

            return true;
        }
        else {
            return false;
        }
    }


    /// <summary>
    /// 序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">T的类型</param>
    /// <param name="proto"></param>
    /// <returns></returns>
    public ProtoMessage SerializeMsg<T>(int type, T proto)
    {
        //构造要发出的消息数据
        MemoryStream ms = new MemoryStream();
        Serializer.Serialize<T>(ms,proto);
        return new ProtoMessage(type,ms);
    }


    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T">T的类型</typeparam>
    /// <param name="msg"></param>
    /// <returns></returns>
    public T DeserializeMsg<T>(ProtoMessage msg)
    {
        return Serializer.Deserialize<T>(msg.Data);
    }



    //异步发送数据
    public void Send(ProtoMessage msg)
    {
        sendMsgQueue.Enqueue(msg);
    }



    public ProtoMessage Receive()
    {
        return recvMsgQueue.Dequeue();
    }


    #endregion

    /// <summary>
    /// 循环接收消息
    /// </summary>
    private void RecvMsgLoop()
    {
        while (true)
        {
            try
            {
                //接收到的一条消息
                ProtoMessage message = RecvOneMessage(stream);

                if (message == null)
                {
                    recvMsgQueue.Enqueue(new ProtoMessage(SERVER_DISCONNECTED, null));
                    Console.WriteLine("RecvThread finished.");
                    return;
                }

                recvMsgQueue.Enqueue(message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("RecvThread error!!!");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);

                recvMsgQueue.Enqueue(new ProtoMessage(CLIENT_ERROR,null));
                Console.WriteLine("RecvThread finished.");
                return;
            }
        }
    }

    /// <summary>
    /// 循环发送消息
    /// </summary>
    private void SendMsgLoop()
    {
        while (true) {
            ProtoMessage msg = null;
            try
            {
                msg = sendMsgQueue.Dequeue();

                if (msg != null)
                {
                    if (msg.Type == -1)
                    {
                        Console.WriteLine("SendThread finished.");
                        return;
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine("send msg:" + msg.Type);
                            //发送消息
                            SendOneMessage(stream, msg);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine("SendThread error!!!");
                            Console.WriteLine(ex.ToString());
                            Console.WriteLine(ex.StackTrace);
                            Console.WriteLine("SendThread finished.");
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("SendThread error! dequeue empty message!");
                }
            }
            catch (System.Exception ex) {
                Console.WriteLine("SendMsgLoop error!!");
                Console.WriteLine(ex.StackTrace);
                recvMsgQueue.Enqueue(new ProtoMessage(CLIENT_ERROR,null));
                Console.WriteLine("SendThread finished.");
                return;
            }
        }
    }


    protected ProtoMessage RecvOneMessage(Stream s)
    {
        //读出消息头部6字节（2字节类型+4字节长度）
        byte[] head = new byte[6];
        int remainLen = 6;
        while (remainLen > 0) {
            int recvnum = s.Read(head,6 - remainLen,remainLen);

            if (recvnum <= 0) {
                return null;
            }

            remainLen -= recvnum;
        }

        int msgType = (head[0] << 8) + head[1];
        int msgLen = (head[2] << 24) + (head[3] << 16) + (head[4] << 8) + head[5];

        MemoryStream protoData = null;
        if (msgLen > 0)
        {
            byte[] data = new byte[msgLen];
            //剩余待接收的消息长度
            remainLen = msgLen;
            while (remainLen > 0)
            {
                //继续接收剩余消息，并写入data
                int recvnum = s.Read(data, msgLen - remainLen, remainLen);

                if (recvnum <= 0)
                {
                    return null;
                }

                remainLen -= recvnum;
            }
            protoData = new MemoryStream(data, false);
        }
        else {
            protoData = new MemoryStream();
        }

        return new ProtoMessage(msgType,protoData);
    }


    protected void SendOneMessage(Stream s, ProtoMessage msg)
    {
        //构造要发出的消息数据
        MemoryStream ms = msg.Data;

        int dataLength = (int)ms.Length;
        byte[] buf = new byte[4 + dataLength];

        //设置头部
        buf[0] = (byte)((msg.Type >> 8) & 0xFF);
        buf[1] = (byte)(msg.Type & 0xFF);
        buf[2] = (byte)((dataLength >> 8) & 0xFF);
        buf[3] = (byte)(dataLength & 0xFF);

        ms.Position = 0;
        int remainLen = dataLength;
        while (remainLen > 0) {
            remainLen -= ms.Read(buf,4 + dataLength - remainLen,remainLen);
        }

        s.Write(buf,0,4 + dataLength);
    }


    public void Close()
    {
        if (sendMsgThread != null) {
            sendMsgQueue.Enqueue(new ProtoMessage(-1,null));
            sendMsgThread.Join();
            sendMsgThread = null;
        }

        if(tcpClient != null){
            tcpClient.Close();
            tcpClient = null;
        }

        if (recvMsgThread != null) {
            recvMsgThread.Join();
            recvMsgThread = null;
        }

        stream = null;

        sendMsgQueue.Clear();
        recvMsgQueue.Clear();
    }
}
