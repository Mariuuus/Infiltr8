using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __ProjectMain.Scripts.Player
{
    public class LevelBuilderCamera : MonoBehaviour
    {
        private Vector3 _origin;
        private Vector3 _difference;
        
        private Camera _camera;
        
        private bool _isDragging;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void onDrag(InputAction.CallbackContext ctx)
        {
            if (ctx.started) _origin = GetMousePosition();
            _isDragging = ctx.performed || ctx.started;
        }

        private void LateUpdate()
        {
            if(!_isDragging) return;
            _difference = GetMousePosition() - transform.position;
            transform.position = _origin - _difference;
        }

        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}