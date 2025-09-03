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
            laserWallController.maxMoveStart = new Vector3(component.startPosition.y, 0f, component.startPosition.x);
            laserWallController.maxMoveEnd = new Vector3(component.endPosition.y, 0f, component.endPosition.x);
            laserWallController.speed = component.speed;
            laserWallController.Init();
        }
    }
}