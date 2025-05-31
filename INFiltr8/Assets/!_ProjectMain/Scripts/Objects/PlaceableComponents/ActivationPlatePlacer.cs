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
        private ActiviationPlateController _activationController;
        public void Place(ActivationComponent component, params Object[] args)
        {
            transform.localScale = PlacerUtils.CalcScale(component.startPosition, component.endPosition, transform.localScale.y);
            transform.position = PlacerUtils.CalcPosition(component.startPosition, component.endPosition,
                (0 - (1-transform.localScale.y)+ 2*transform.localScale.y));
            
            _activationController = GetComponent<ActiviationPlateController>();
            var fireWall = args[0] as GameObject;
            _activationController.activationDoor = fireWall;
            
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