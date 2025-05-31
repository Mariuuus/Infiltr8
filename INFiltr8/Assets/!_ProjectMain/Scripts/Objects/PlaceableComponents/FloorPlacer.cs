using System;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class FloorPlacer : MonoBehaviour
    {
        public void PlaceFloor(LevelData lvlData)
        {
            transform.localScale = PlacerUtils.CalcScale(lvlData.wallPointOne, lvlData.wallPointTwo, transform.localScale.y);
            transform.position = PlacerUtils.CalcPosition(lvlData.wallPointOne, lvlData.wallPointTwo, -1);
        }
    }
}