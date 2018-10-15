using UnityEngine;
using System.Collections;
using System.IO;

public class ProtoMessage {
    readonly int type;
    readonly MemoryStream data;

    public int Type
    {
        get
        {
            return type;
        }
    }

    public MemoryStream Data
    {
        get {
            return data;
        }
    }


    public ProtoMessage(int type,MemoryStream data)
    {
        this.type = type;
        this.data = data;
    }
	
}
