using __ProjectMain.Scripts.LevelEditor.StateMachine;
using __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace __ProjectMain.Scripts.Managers.LevelEditor
{
    public class LevelEditorManager : MonoBehaviour
    {
        public static LevelEditorManager Instance { get; private set; }
        public BuildState[] _buildStates { get; private set; }
        
        private LevelEditorStateMachine _levelEditorStateMachine;
        
        [Header("General Settings")]
        public TMP_Text lookAtText;
        public TMP_Text currentStateText;
        public Grid grid;
        public GameObject buildButton;        
        public GameObject buildUIContainer;    
        
        [Header("Tiles")]
        public Tile hoverTile;

        [Header("Build Menu Sprites")]
        public Sprite WallBuildSprite;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _levelEditorStateMachine.ChangeState(_levelEditorStateMachine.SpectateState);
        }
        
        public void ChangeBuildMode(BuildState buildState) => _levelEditorStateMachine.ChangeState(buildState);
        public void ChangeToSpectator() => _levelEditorStateMachine.ChangeState(_levelEditorStateMachine.SpectateState);
        public void Init()
        {
            Instance = this;
            
            Debug.Log("LevelUIManager::Init");
            
            _levelEditorStateMachine = new LevelEditorStateMachine();
            _buildStates = new BuildState[]
            {
                _levelEditorStateMachine.WallBuildState,
            };
            
            foreach (var state in _buildStates)
            {
                var newObject = Instantiate(buildButton, buildUIContainer.transform, false);
                newObject.GetComponent<BuildButton>().Init(state);
            }
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            LevelManager.Instance.UpdateMap();
        }

        private void Update()
        {
            _levelEditorStateMachine.Update();
        }
        
        public void OnClick(InputAction.CallbackContext ctx)
        {
            _levelEditorStateMachine.OnClick(ctx);
        }
        
        public void OnEsc(InputAction.CallbackContext ctx)
        {
            _levelEditorStateMachine.OnEsc(ctx);
        }
        
        public Vector3Int GetMousePosition () {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return grid.WorldToCell(mouseWorldPos);
        }
    }
}