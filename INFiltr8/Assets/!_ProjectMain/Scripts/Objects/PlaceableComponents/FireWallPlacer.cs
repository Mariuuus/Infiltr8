using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using Object = System.Object;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class FireWallPlacer : MonoBehaviour, IPlaceable<FireWallComponent>
    {
        public void Place(FireWallComponent component, params Object[] args)
        {
            transform.localScale = PlacerUtils.CalcScale(component.startPosition, component.endPosition, transform.localScale.y);
            transform.position = PlacerUtils.CalcPosition(component.startPosition, component.endPosition,0);
            
            var doorController = GetComponent<DoorController>();
            doorController.requiredHackStatusAmounts = component.doorUnlockRequirements;
            doorController.updateDoorUI();
        }
    }
}