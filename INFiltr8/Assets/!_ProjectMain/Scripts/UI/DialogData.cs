using UnityEngine;
using System.Collections.Generic;

public enum DialoqType
{
    AREA, NPC
}

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{
    public string dialogName;
    public DialoqType trigger;
    public CharacterData character;
    public List<string> lines;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/DialogData")]
public class CharacterData: ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
}