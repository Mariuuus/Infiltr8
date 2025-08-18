using System.Collections.Generic;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI
{
    public class LaptopSettings : MonoBehaviour
    {
        [SerializeField] private GameObject redToggle;
        [SerializeField] private GameObject blueToggle;
        [SerializeField] private GameObject greenToggle;
        [SerializeField] private GameObject yellowToggle;

        private LaptopComponent _laptopComponent;
        private bool _suppressToggleCallbacks = false;

        private void ToggleHackStatus(bool active, HackStatus hackStatus)
        {   
            if (_laptopComponent.possibleHacks.Contains(hackStatus) == active) return;
            if (active)
            {
                _laptopComponent.possibleHacks.Add(hackStatus);
            }
            else
            {
                _laptopComponent.possibleHacks.Remove(hackStatus);
            }

            LevelEditorFileManager.Instance.QuickSave();
        }

        public void ShowAndLoad(LaptopComponent laptop)
        {
            _laptopComponent = laptop;

            Debug.Log(string.Join(", ", _laptopComponent.possibleHacks));

            _suppressToggleCallbacks = true;

            redToggle.GetComponent<Toggle>().isOn = false;
            blueToggle.GetComponent<Toggle>().isOn = false;
            greenToggle.GetComponent<Toggle>().isOn = false;
            yellowToggle.GetComponent<Toggle>().isOn = false;

            foreach (var hack in _laptopComponent.possibleHacks.ToList()) // Defensive copy
            {
                switch (hack)
                {
                    case HackStatus.BlueHacked:
                        blueToggle.GetComponent<Toggle>().isOn = true;
                        break;
                    case HackStatus.GreenHacked:
                        greenToggle.GetComponent<Toggle>().isOn = true;
                        break;
                    case HackStatus.YellowHacked:
                        yellowToggle.GetComponent<Toggle>().isOn = true;
                        break;
                    case HackStatus.RedHacked:
                        redToggle.GetComponent<Toggle>().isOn = true;
                        break;
                }
            }

            _suppressToggleCallbacks = false;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnChangeRed(bool active)
        {
            if (_suppressToggleCallbacks) return;
            ToggleHackStatus(active, HackStatus.RedHacked);
        }

        public void OnChangeBlue(bool active)
        {
            if (_suppressToggleCallbacks) return;
            ToggleHackStatus(active, HackStatus.BlueHacked);
        }

        public void OnChangeGreen(bool active)
        {
            if (_suppressToggleCallbacks) return;
            ToggleHackStatus(active, HackStatus.GreenHacked);
        }

        public void OnChangeYellow(bool active)
        {
            if (_suppressToggleCallbacks) return;
            ToggleHackStatus(active, HackStatus.YellowHacked);
        }
    }
}
