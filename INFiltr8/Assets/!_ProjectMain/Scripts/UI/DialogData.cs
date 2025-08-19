using UnityEngine;
using System.Collections.Generic;

public enum DialoqType
{
    AREA, NPC
}

//[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
[System.Serializable]
public class DialogData
{
    public string dialogName;
    public DialoqType trigger;
    public string character = "";
    public List<string> lines = new List<string>();
}

// [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/DialogData")]
public class CharacterData
{
    public string characterName;
    public Sprite characterSprite;
}