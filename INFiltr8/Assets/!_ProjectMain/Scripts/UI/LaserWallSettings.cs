using System;
using System.Globalization;
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
            this.speedInputField.text = this._laserWallComponent.speed.ToString(CultureInfo.CurrentCulture);
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void OnSetLaserWallSpeed(string speed)
        {
            float converted = float.Parse(speed);

            if (converted is <= 0f or > 15.0f)
            {
                throw new InvalidLevelEditorActionException("Laser wall speed cannot be under 0 or over 15!");
            }
            
            _laserWallComponent.speed = converted;
            LevelEditorFileManager.Instance.QuickSave();
        }  
    }
}