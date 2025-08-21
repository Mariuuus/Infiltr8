using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

namespace __ProjectMain.Scripts.UI.Controls
{
    public class VirtualMouseUI : MonoBehaviour
    {
        private VirtualMouseInput _virtualMouseInput;

        private void Awake()
        {
            _virtualMouseInput = GetComponent<VirtualMouseInput>();
        }

        private void LateUpdate()
        {
            Vector2 position = _virtualMouseInput.virtualMouse.position.value;
            position.x = Math.Clamp(position.x, 0, Screen.width);
            position.y = Math.Clamp(position.y, 0, Screen.height);
            InputState.Change(_virtualMouseInput.virtualMouse.position, position);
        }
    }
}