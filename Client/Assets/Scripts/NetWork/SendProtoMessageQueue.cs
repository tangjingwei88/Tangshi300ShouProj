using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class SendProtoMessageQueue {

    Queue<ProtoMessage> q = new Queue<ProtoMessage>();

    public ProtoMessage Dequeue()
    {
        lock (this)
        {
            while (q.Count == 0)
            {
                Monitor.Wait(this);
            }

            return q.Dequeue();
        }
    }


    public void Enqueue(ProtoMessage msg)
    {
        lock (this)
        {
            q.Enqueue(msg);
            Monitor.Pulse(this);
        }
    }


    public void Clear()
    {
        lock (this)
        {
            q.Clear();
        }
    }
}
