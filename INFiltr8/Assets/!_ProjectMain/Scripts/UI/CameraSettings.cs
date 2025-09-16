using System.Globalization;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class CameraSettings : MonoBehaviour
    {
        private CameraComponent _cameraComponent;
        [SerializeField] private TMP_InputField rotationSpeedInput;
        [SerializeField] private TMP_InputField turnaroundTimeInput;
        [SerializeField] private Slider rotationSlider;
        [SerializeField] private TMP_InputField rotationInput;
        public void Show(CameraComponent cameraComponent)
        {
            _cameraComponent = cameraComponent;
            rotationSpeedInput.text = cameraComponent.rotationSpeed.ToString();
            turnaroundTimeInput.text = cameraComponent.turnaroundTime.ToString();
            rotationSlider.value = _cameraComponent.rotation;
            rotationInput.text = _cameraComponent.rotation.ToString("F1", CultureInfo.CurrentCulture);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnSetRotationSpeed(string value)
        {
            int parsedValue = int.Parse(value);
            int clamped = Mathf.Clamp(parsedValue, 1, 100);
            _cameraComponent.rotationSpeed = clamped;
            rotationSpeedInput.text = clamped.ToString();
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void OnTurnaroundTimeInput(string value)
        {
            int parsedValue = int.Parse(value);
            int clamped = Mathf.Clamp(parsedValue, 1, 10);
            _cameraComponent.turnaroundTime = clamped;
            turnaroundTimeInput.text = clamped.ToString();
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void OnRotationInput(string value)
        {
            float parsed = float.Parse(value);
            _cameraComponent.rotation = Mathf.Clamp(parsed, 0f, 360f);
            rotationSlider.value = Mathf.Clamp(parsed, 0f, 360f);
            // LevelEditorManager.Instance.UpdateUI();
            LevelEditorFileManager.Instance.QuickSave();
        }

        private int previousMappedValue = 0;
        
        public void OnRotationSliderChange(float value)
        {
            _cameraComponent.rotation = value;
            rotationInput.text = value.ToString("F1", CultureInfo.CurrentCulture);
            if (previousMappedValue != (int) (value / 13))
            {
                previousMappedValue = (int) (value / 13);
                LevelEditorManager.Instance.UpdateUI();           
            }
            LevelEditorFileManager.Instance.QuickSave();
        }
    }
}