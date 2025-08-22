using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class CollectablePlacer: MonoBehaviour, IPlaceable<CollectableComponent>
    {
        public void Place(CollectableComponent component, params object[] args)
        {
            transform.position = new Vector3(component.position.y, .5f, component.position.x);
            GetComponent<Collectable>().Init(component.type);
        }
    }
}