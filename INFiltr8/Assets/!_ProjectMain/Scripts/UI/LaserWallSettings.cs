using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Exceptions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class LaserWallSettings : MonoBehaviour
    {
        private LaserWallComponent _laserWallComponent;
        [SerializeField] private TMP_InputField speedInputField;

        public void Show(LaserWallComponent laserWallComponent)
        {
            this._laserWallComponent = laserWallComponent;
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void OnSetLaserWallSpeed(string speed)
        {
            _laserWallComponent.speed = float.Parse(speed);
            LevelEditorFileManager.Instance.QuickSave();
        }  
    }
}