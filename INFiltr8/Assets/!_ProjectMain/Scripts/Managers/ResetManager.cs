using System;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor.StateMachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Managers
{
    public class ResetManager: MonoBehaviour
    {
        [SerializeField] private float resetTime = 2.25f;
        [SerializeField] private Image resetImage;
        [SerializeField] private UniversalRenderPipelineAsset renderPipeline;
        
        [SerializeField] private CinemachineFollow cinemachineFollow;
        [SerializeField] private float maxOffset = 20f;

        private float _defaultOffset;
        private void Start()
        {
            resetImage.fillAmount = 0f;
            _defaultOffset = cinemachineFollow.FollowOffset.y;
        }

        private float pressedStartTime = -1f;
        public void OnReset(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                pressedStartTime = Time.time;
            } else if (ctx.performed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // perform reset
            }
            else if (ctx.canceled)
            {
                pressedStartTime = -1f;
                resetImage.fillAmount = 0f;
                cinemachineFollow.FollowOffset.y = _defaultOffset;
            }
        }

        private void Update()
        {
            if (pressedStartTime != -1f)
            {
                float timePassedRatio = (Time.time - pressedStartTime)/resetTime;
                resetImage.fillAmount = timePassedRatio;
                cinemachineFollow.FollowOffset.y = _defaultOffset + (maxOffset - _defaultOffset) * timePassedRatio;
            }
        }
    }
}