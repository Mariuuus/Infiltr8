using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class LaptopPlacer : MonoBehaviour, IPlaceable<LaptopComponent>
    {
        public void Place(LaptopComponent component, params object[] args)
        {
            transform.position = new Vector3(component.position.y, 0, component.position.x);
            var hackableObject = GetComponent<HackableDevice>();
            hackableObject.Init(component);
        }
    }
}