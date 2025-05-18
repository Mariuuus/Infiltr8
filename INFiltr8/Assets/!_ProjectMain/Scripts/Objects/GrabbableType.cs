using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

public class grabbableType : MonoBehaviour
{
    [SerializeField] private HackStatus hackColor;

    public HackStatus getHackColor()
    {
        return hackColor;
    }
}
