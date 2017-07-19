using UnityEngine;
using System.Collections;

public class UIManager : ManagerBase {

    public static UIManager Instance = null;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}

    public void SendMsg(MsgBase msg)
    {
        if (msg.GetManager() == ManagerID.UIManager)
        {
            ProcessEvent(msg);
        }
        else
        {
            MsgCenter.Instance.SendToMsg(msg);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
