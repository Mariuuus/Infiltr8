using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Utilities.Exceptions;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class FireWallSettings : MonoBehaviour
    {
        
        private FireWallComponent _fireWallComponent;


        private int _formAmount = 0;
        private HackStatus _formHackColor;
        
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject listElement;
        

        public void Show(FireWallComponent fireWallComponent)
        {
            this._fireWallComponent = fireWallComponent;
            this.gameObject.SetActive(true);
            RenderRequirements();
        }
        
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void OnOptionSelect(Int32 option)
        {
            this._formHackColor = (HackStatus)option;
        }
        
        public void OnAmountChanged(String amount)
        {
            this._formAmount = Int32.Parse(amount);
        }
        
        public void AddRequirement()
        {
            if (_formAmount <= 0) throw new InvalidLevelEditorActionException("Invalid Amount!");
            //Debug.Log(_formAmount);
            //Debug.Log(_formHackColor);
            _fireWallComponent.AddRequirement(new HackStatusAmount(_formHackColor, _formAmount));
            RenderRequirements();
        }

        private void RenderRequirements()
        {
            for (int i = container.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }

            foreach (var hamounts in _fireWallComponent.doorUnlockRequirements)
            {
                GameObject newElement = Instantiate(listElement, container.transform);
                newElement.GetComponent<FireWallRequirementsList>().Init(hamounts.hackStatus.ToString().Replace("Hacked", ""), hamounts.amount.ToString());
            }
        }

    }
}