using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using Object = System.Object;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class WallPlacer : MonoBehaviour, IPlaceable<WallComponent>
    {
        public void Place(WallComponent component, params Object[] args)
        {
            transform.localScale = PlacerUtils.CalcScale(component.startPosition, component.endPosition, 2);
            transform.position = PlacerUtils.CalcPosition(component.startPosition, component.endPosition, 0.5f);
        }
    }
}