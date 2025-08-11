using System;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class PortalPlacer : MonoBehaviour, IPlaceable<PortComponent>
    {  
        [SerializeField]
        public GameObject portalObject;
        public void Place(PortComponent component, params object[] args)
        {
            Vector2Int pos = args[0] as Vector2Int? ?? default;
            transform.position = new Vector3(pos.y, 0.5f, pos.x);
            GetComponent<PortalController>().Init(args[1] as PortalController);
            
            var rend = portalObject.GetComponent<Renderer>();
            Material mat = rend.material;
            mat.SetColor("_Color", args[2] as Color? ?? default);  
        }
    }
}