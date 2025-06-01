using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using __ProjectMain.Scripts.LevelEditor;
using __ProjectMain.Scripts.UI;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectMain.Scripts.Managers
{
    public class LevelFileManager : MonoBehaviour
    {
        public static LevelFileManager Instance { get; private set; }
        public LevelData LevelToLoad { get; private set; }

        public void Awake()
        {
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void EditLevel(int index)
        {
            LevelToLoad = GetLevels()[index];
            try
            {
                GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine
                    .LevelEditorState);
            }
            catch
            {
                SceneManager.LoadScene("LevelEditor");
            }
        }
        
        public void PlayLevel(int index)
        {
            LevelToLoad = GetLevels()[index];
            try
            {
                GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine
                    .IngameState);
            }
            catch
            {
                SceneManager.LoadScene("LevelLoader");
            }
        }

        public List<LevelData> GetLevels() => LevelDataUtils.GetAvailableLevels();
        
        public void CreateAndLoadLevel(string levelName, int size) {
            Debug.Log(levelName);
            try
            {
                LevelToLoad = LevelDataUtils.LoadFile(levelName);
            }
            catch (FileNotFoundException)
            {
                LevelDataUtils.SaveFile(new LevelData(levelName, size), false);
                LevelToLoad = LevelDataUtils.LoadFile(levelName); 
            }
            finally
            {
                try
                {
                    GlobalGameManager.Instance.GameStateMachine.ChangeState(GlobalGameManager.Instance.GameStateMachine
                        .LevelEditorState);
                }
                catch
                {
                    SceneManager.LoadScene("LevelEditor");
                }
            }
        }
        
    }
}