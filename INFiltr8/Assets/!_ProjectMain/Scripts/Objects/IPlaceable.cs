using System;
using __ProjectMain.Scripts.LevelEditor.Components;

namespace __ProjectMain.Scripts.Objects
{
    public interface IPlaceable<T> where T : LevelComponent
    {
        public void Place(T component, params Object[] args);
    }
}