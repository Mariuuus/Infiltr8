using System;
using System.Collections.Generic;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.UI.Slots
{
    public class GameSlotsManager : MonoBehaviour
    {
        private List<GameSlot> _gameSlots;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject slotContainer;

        private void ReRender()
        {
            foreach (Transform child in slotContainer.transform) {
                Destroy(child.gameObject);
            }
            
            _gameSlots = new List<GameSlot>();
            
            for (int i = 0; i < 3; i++)
            {
                var newObj = Instantiate(slotPrefab, slotContainer.transform);
                _gameSlots.Add(newObj.GetComponent<GameSlot>());
                newObj.GetComponent<GameSlot>().Init(this, (i + 1).ToString(), GameDataUtils.Exists((i + 1).ToString()), LevelDataUtils.GetAvailableLevels().Count);
            }
        }
        private void Start()
        {
            ReRender();
        }

        public void LoadFile(string postFix)
        {
            GameDataManager.Instance.postFix = postFix;
            GameDataManager.Instance.gameData = GameDataUtils.LoadData(postFix);
            GameDataManager.Instance.QuickSave();
            if (!GameDataManager.Instance.gameData.introDone)
            {
                SceneManager.LoadScene("!_ProjectMain/Scenes/Intro");
            }
            else
            {
                SceneManager.LoadScene("!_ProjectMain/Scenes/LevelSelection");
            }
        }

        public void DeleteFile(string postFix)
        {
            GameDataManager.Instance.gameData = null;
            GameDataUtils.DeleteData(postFix);
            ReRender();
        }
    }
}