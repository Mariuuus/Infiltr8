using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Objects
{
    public enum DistroType
    {
        Arch,
        CentOS,
        Fedora,
        Kali,
        Kubuntu,
        Manjaro,
        Mint,
        NixOS,
        Parrot,
        PopOS,
        RaspiOS,
        Ubuntu,
    }
    
    [CreateAssetMenu(fileName = "Distro", menuName = "Infiltr8/Distro", order = 1)]
    public class Distro : ScriptableObject
    {
        [Header("Distro Settings")]
        public Sprite distroSprite;
        [Tooltip("Distro Name (will be used in Enum")]
        public DistroType distroName;
    }
}