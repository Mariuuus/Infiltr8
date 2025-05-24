using System;
using __ProjectMain.Scripts.LevelEditor.Types;
using UnityEngine;

public class grabbableType : MonoBehaviour
{
    [SerializeField] private HackStatus hackColor;

    [SerializeField] private Material redMat;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material yellowMat;

    private void Start()
    {
       changeMaterial(hackColor);
    }
    public HackStatus getHackColor()
    {
        return hackColor;
    }
    
    public void changeMaterial(HackStatus Color) {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = null;
    
        switch (Color) {
            case HackStatus.BlueHacked:
                material = blueMat;
                break;
            case HackStatus.RedHacked:
                material = redMat;
                break;
            case HackStatus.GreenHacked:
                material = greenMat;
                break;
            case HackStatus.YellowHacked:
                material = yellowMat;
                break;
        }
        
        renderer.material = material;
    }
}
