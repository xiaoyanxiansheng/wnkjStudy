using UnityEngine;
using System.Collections;

public class MsgBase{
    public ushort msgId;

    public ManagerID GetManager()                                      
    {
        return (ManagerID)(msgId - msgId % FrameTool.MagSpan);
    }

    public MsgBase(ushort temMsg)
    {
        msgId = temMsg;
    }
}
