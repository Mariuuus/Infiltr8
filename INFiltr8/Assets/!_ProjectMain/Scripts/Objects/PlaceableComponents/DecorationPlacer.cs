using System;
using __ProjectMain.Scripts.LevelEditor.Components;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class DecorationPlacer: MonoBehaviour, IPlaceable<DecorationComponent>
    {
        public void Place(DecorationComponent component, params object[] args)
        {
            float xRotation = component.decoration == Decorations.TubeLamp ? -90f : 0f;
            bool inEditor = args[0] is bool ? (bool) args[0] : false;
            transform.position = inEditor
                ? new Vector3(component.position.y + 0.5f, 0f, component.position.x + 0.5f)
                : new Vector3(component.position.y, 0f, component.position.x);
            GetComponent<DecorationController>().variant = component.variant;
            GetComponent<DecorationController>().setDecoration(component.decoration);
            GetComponent<DecorationController>().transform.rotation = Quaternion.Euler(xRotation, component.rotation, 0f);
            if (component.decoration == Decorations.TubeLamp && inEditor)
            {
                GetComponent<DecorationController>().SetYPosition(0.65f);
            }
        }
    }
}