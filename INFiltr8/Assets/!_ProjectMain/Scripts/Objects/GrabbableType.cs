using System;
using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

public class grabbableType : MonoBehaviour
{
    [SerializeField] private HackStatus hackColor;

    private void Start()
    {
        
    }
    public HackStatus getHackColor()
    {
        return hackColor;
    }
}
