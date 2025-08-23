using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.Managers
{
    public class MainMenuCollectableShelve: MonoBehaviour
    {
        [SerializeField] private CollectableMainMenuStatue[] availablePlaces;
        private void Start()
        {
            var data = GameDataManager.Instance.GetCollectables();

            if (availablePlaces.Length < data.Count)
            {
                Debug.LogError("Problem, not enough places on shelf");
            }

            for (int i = 0; i < availablePlaces.Length; i++)
            {
                if (i < data.Count)
                {
                    availablePlaces[i].Init(data[i].distroType, data[i].collect);
                }
                else
                {
                    Destroy(availablePlaces[i].gameObject);
                }
            }
        }
    }
}