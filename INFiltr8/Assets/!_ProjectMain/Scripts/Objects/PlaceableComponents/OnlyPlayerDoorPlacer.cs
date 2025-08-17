using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class OnlyPlayerDoorPlacer : MonoBehaviour, IPlaceable<OnlyPlayerDoorComponent>
    {  
        [SerializeField] private new Collider collider;
        public void Place(OnlyPlayerDoorComponent component, params object[] args)
        {
            transform.localScale = PlacerUtils.CalcScale(component.startPosition, component.endPosition, 1);
            transform.position = PlacerUtils.CalcPosition(component.startPosition, component.endPosition, 0.5f);

            Physics.IgnoreCollision(collider, args[0] as Collider, true);
        }
    }
}