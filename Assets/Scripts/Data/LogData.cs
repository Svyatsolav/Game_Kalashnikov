using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LogItem", menuName = "Log/Item")]

public class LogData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    
    [TextArea(1, 5)]
    public string description;
}
