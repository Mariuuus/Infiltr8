using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class LaserWallPlacer : MonoBehaviour, IPlaceable<LaserWallComponent>
    {

        public void Place(LaserWallComponent component, params object[] args)
        {
            var laserWallController = GetComponent<LaserWallController>();
            laserWallController.maxMoveStart = LevelEditorUtils.ExpandToThreeDimensions(component.startPosition);
            laserWallController.maxMoveEnd = LevelEditorUtils.ExpandToThreeDimensions(component.endPosition);
            laserWallController.speed = component.speed;
            laserWallController.isVertical = component.startPosition.x != component.endPosition.x;
            laserWallController.Init();
        }
    }
}