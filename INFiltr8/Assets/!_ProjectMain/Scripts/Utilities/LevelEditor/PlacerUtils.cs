using System;
using UnityEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.Utilities.LevelEditor
{
    public class PlacerUtils
    {
        public static float CalcDifference(int c1, int c2)
        {
            return Math.Abs(c1 - c2);
        }

        public static Vector3 CalcScale(Vector2Int p1, Vector2Int p2, float previousScaleY)
        {
            float diffY = CalcDifference(p2.y, p1.y);
            float diffX = CalcDifference(p2.x, p1.x);
            
            return new Vector3(diffY!=0? 1+diffY : 1,previousScaleY,diffX!=0? 1+diffX : 1);
        }

        public static Vector3 CalcPosition(Vector2Int p1, Vector2Int p2, float height)
        {
            float diffY = CalcDifference(p2.y, p1.y);
            float diffX = CalcDifference(p2.x, p1.x);
            
            int smallerX = Math.Min(p1.x, p2.x);
            int smallerY = Math.Min(p1.y, p2.y);
            
            return new Vector3(smallerY+diffY/2, height,smallerX+diffX/2);
        }
    }
}