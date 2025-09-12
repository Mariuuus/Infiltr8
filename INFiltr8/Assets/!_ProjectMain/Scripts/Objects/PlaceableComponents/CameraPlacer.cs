using __ProjectMain.Scripts.LevelEditor.Components;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class CameraPlacer : MonoBehaviour, IPlaceable<CameraComponent>
    {
        public void Place(CameraComponent component, params object[] args)
        {
            bool inEditor = args[0] is bool ? (bool) args[0] : false;
            GetComponent<CameraController>().rotationCooldownTime = component.turnaroundTime;
            GetComponent<CameraController>().rotationSpeed = component.rotationSpeed;
            GetComponent<CameraController>().transform.rotation = Quaternion.Euler(0f, component.rotation, 0f);
            GetComponent<CameraController>().transform.position = inEditor ? 
                new Vector3(component.position.y + 0.5f, 1f, component.position.x + 0.5f) :
                new Vector3(component.position.y, 0f, component.position.x);
            GetComponent<CameraController>().Init();

            if (inEditor)
            {
                GetComponent<CameraController>().disableRotation = true;
            }
        }
    }
}