using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers.Ingame;
using __ProjectMain.Scripts.Managers.Level;
using __ProjectMain.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Player
{
    public class HackController : MonoBehaviour
    {
        public void OnHack(InputAction.CallbackContext context)
        {
            if (gameObject.GetComponent<PlayerController>().inSlowdown) return;
            if(IngameManager.Instance.Paused) return;
            if (!context.started) return;
            if (GetComponent<GrabController>().IsGrabbing) return;
            if (!GetComponent<PlayerController>().ClosestObject) return;
            var hackableDevice = GetComponent<PlayerController>().ClosestObject.GetComponent<HackableDevice>();
            switch (context.control.name)
            {
                case "1":
                case "up":
                    if(hackableDevice.Component.possibleHacks.Contains(HackStatus.BlueHacked))
                        hackableDevice.ChangeMaterial(HackStatus.BlueHacked);
                    break;
                case "2":
                case "right":
                    if(hackableDevice.Component.possibleHacks.Contains(HackStatus.RedHacked))
                        hackableDevice.ChangeMaterial(HackStatus.RedHacked);
                    break;
                case "3":
                case "down":
                    if(hackableDevice.Component.possibleHacks.Contains(HackStatus.GreenHacked))
                        hackableDevice.ChangeMaterial(HackStatus.GreenHacked);
                    break;
                case "4":
                case "left":
                    if(hackableDevice.Component.possibleHacks.Contains(HackStatus.YellowHacked))
                        hackableDevice.ChangeMaterial(HackStatus.YellowHacked);
                    break;
            }
            GetComponent<PlayerController>().UpdateInteractionUI();
        }
    }
}