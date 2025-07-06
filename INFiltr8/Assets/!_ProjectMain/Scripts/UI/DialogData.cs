using UnityEngine;
using System.Collections.Generic;

public enum DialoqType
{
    AREA, NPC
}

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{
    public string npc_name;
    public DialoqType trigger;
    public Sprite avatar;
    public List<string> msg;
}
