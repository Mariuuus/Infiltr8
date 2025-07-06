using System;
using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using Object = System.Object;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class ActivationPlatePlacer : MonoBehaviour, IPlaceable<ActivationComponent>
    {
        private ActivationPlateController _activationController;
        public void Place(ActivationComponent component, params Object[] args)
        {
            //Debug.Log(component.startPosition + "|" + component.endPosition);
            
            
            transform.localScale = PlacerUtils.CalcScale(component.startPosition, component.endPosition, transform.localScale.y);
            transform.position = PlacerUtils.CalcPosition(component.startPosition, component.endPosition,
                (0 - (1-transform.localScale.y)+ 2*transform.localScale.y));
            
            _activationController = GetComponent<ActivationPlateController>();
            var fireWall = args[0] as GameObject;
            _activationController.activationDoor = fireWall;

            if (component.maxDevices == 0)
            {
                //max amount possible calculation ig 
                var fireWallComponent = args[1] as FireWallComponent;
                var sum = 0;
                if (fireWallComponent != null)
                    foreach (var req in fireWallComponent.doorUnlockRequirements)
                    {
                        sum += req.amount;
                    }
                _activationController.SetMaxDevices(sum);
            }
            else
            {
                _activationController.SetMaxDevices(component.maxDevices);
            }
            
            var updateUI = WaitAndUpdateUI();
            StartCoroutine(updateUI);
        }
        
        //TODO: fix this, this is not a solution
        IEnumerator WaitAndUpdateUI()
        {
            yield return new WaitForSeconds(2);
            _activationController.UpdateUI();
        }
    }
}