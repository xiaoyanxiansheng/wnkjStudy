using UnityEngine;
using System.Collections;

public class MsgCenter : MonoBase {

    public static MsgCenter Instance = null;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}

    public void SendToMsg(MsgBase msg)
    {
        AnasysisMsg(msg);
    }

    public void AnasysisMsg(MsgBase msg)
    {
        ManagerID managerID = msg.GetManager();
        switch (managerID)
        {
            case ManagerID.UIManager:

                break;
            case ManagerID.NPCManager:

                break;

            default:
                Debug.Log("aaaaaaaaaaaaaaaa");
                break;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
