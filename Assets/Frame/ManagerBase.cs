using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EvenNode
{
    public MonoBase data;
    public EvenNode next;

    public EvenNode(MonoBase mono)
    {
        this.data = mono;
        this.next = null;
    }
}

public class ManagerBase:MonoBase {

    public Dictionary<ushort, EvenNode> eventTree = new Dictionary<ushort, EvenNode>();

    public void Register(MonoBase mono,params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length;i++ )
        {
            EvenNode node = new EvenNode(mono);
            RegisterMsg(msgs[i], node);
        }
    }

    public void RegisterMsg(ushort id, EvenNode node)
    {
        if (!eventTree.ContainsKey(id))
        {
            eventTree.Add(id, node);
        }
        else
        {
            EvenNode preNode = eventTree[id];
            while (preNode.next!=null)
            {
                preNode = preNode.next;
            }
            preNode.next = node;
        }
    }
    
    public void UnRegister(MonoBase mono,params ushort[] msgs)
    {
        for(int i = 0;i<msgs.Length;i++)
        {
            UnRegisterMsg(msgs[i], mono);
        }
    }

    public void UnRegisterMsg(ushort id,MonoBase mono)
    {
        if (!eventTree.ContainsKey(id))
        {
            Debug.Log("eventTree not id " + id);
            return;
        }
        EvenNode node = eventTree[id];
        if (node.data == mono)
        {
            if (node.next == null)
            {
                eventTree.Remove(id);
            }
            else
            {
                node.data = mono;
                node.next = node.next.next;
            }
        }
        else
        {
            while (node.next != null && node.next.data != mono)
            {
                node = node.next;
            }
            if (node.next.next != null)
            {
                node.next = node.next.next;
            }
            else
            {
                node.next = null;
            }
        }
    }

    public override void ProcessEvent(MsgBase tmpMsg)
    {
        if (!eventTree.ContainsKey(tmpMsg.msgId))
        {
            Debug.Log("ProcessEvent is error manager msgId " + tmpMsg.GetManager() + "===" + tmpMsg.msgId);
        }
        else
        {
            EvenNode node = eventTree[tmpMsg.msgId];
            do
            {
                node.data.ProcessEvent(tmpMsg);
                node = node.next;
            }
            while (node != null);
        }
    }
}
