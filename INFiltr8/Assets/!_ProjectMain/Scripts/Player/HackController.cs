using __ProjectMain.Scripts.LevelEditor.Types;
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
            if (!context.started) return;
            if (GetComponent<GrabController>().IsGrabbing) return;
            if (!GetComponent<PlayerController>().ClosestObject) return;
            switch (context.control.name)
            {
                case "1":
                case "up":
                    GetComponent<PlayerController>().ClosestObject.GetComponent<GrabbableObject>()
                        .changeMaterial(HackStatus.BlueHacked);
                    break;
                case "2":
                case "right":
                    GetComponent<PlayerController>().ClosestObject.GetComponent<GrabbableObject>()
                        .changeMaterial(HackStatus.RedHacked);
                    break;
                case "3":
                case "down":
                    GetComponent<PlayerController>().ClosestObject.GetComponent<GrabbableObject>()
                        .changeMaterial(HackStatus.GreenHacked);
                    break;
                case "4":
                case "left":
                    GetComponent<PlayerController>().ClosestObject.GetComponent<GrabbableObject>()
                        .changeMaterial(HackStatus.YellowHacked);
                    break;
            }
            GetComponent<PlayerController>().UpdateInteractionUI();
        }
    }
}