using __ProjectMain.Scripts.LevelEditor.Components;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class DecorationPlacer: MonoBehaviour, IPlaceable<DecorationComponent>
    {
        public void Place(DecorationComponent component, params object[] args)
        {
            transform.position = new Vector3(component.position.y, 0f, component.position.x);
            GetComponent<DecorationController>().setDecoration(component.decoration);
        }
    }
}