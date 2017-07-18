using UnityEngine;
using System.Collections;

public enum ManagerID
{
    GameManager = 0,
    UIManager = FrameTool.MagSpan,
    AudioManager = FrameTool.MagSpan * 2,
    NPCManager = FrameTool.MagSpan * 3
}
public class FrameTool{
    public const int MagSpan = 3000;
}
