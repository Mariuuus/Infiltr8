using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class ActivationPlateSettings : MonoBehaviour
    {

        private ActivationComponent _activationComponent;
        public TMP_InputField amountField;
        
        public void Show(ActivationComponent activationComponent)
        {
            this._activationComponent = activationComponent;
            this.gameObject.SetActive(true);
            UpdateUI();
        }
        
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }


        public void UpdateUI()
        {
            amountField.text = _activationComponent.maxDevices.ToString();
        }

        public void OnChange(string newAmount)
        {
            var amount = int.Parse(newAmount);
            _activationComponent.maxDevices = amount;
            LevelEditorFileManager.Instance.QuickSave();
            UpdateUI();
        }

    }
}