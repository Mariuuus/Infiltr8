# INFiltr8

## Prequisites

- `Unity`, version `6000.0.48f1` `LTS`

## Adding a new Component to the Game

### LevelEditor Presents

1. Add a new LevelComponent at `Scripts/LevelEditor/Components`.
    This Class needs to inhared (directly or indirect) from `LevelComponent`.
    It is recommended that you at least inherited from `OnePointLevelComponent` or `TwoPointLevelComponent`. The following Example will add a `OnePointLevelComponent` named `GoalLevelComponent`. This can then look like this:

    ```csharp
    using UnityEngine;
    namespace __ProjectMain.Scripts.LevelEditor.Components
    {
        [System.Serializable]
        public class GoalComponent : OnePointLevelComponent
        {
            // just for deserialization
            public GoalComponent() {}
            
            public GoalComponent(Vector2Int position, LevelData levelData) : base(position, levelData)
            {
                // Throw Exceptions if needed (One Point/ Two Point Component will check for boundaries etc.)
            }
        }
    }
    ```

2. Add a BuildState (optional, but often requiered).
    This sould again inherit from the corresponding BuildState (`OnePointsBuildState` or `TwoPointsBuildState`).
    This should also implement the `ISelectableState` Interface, if there should be a button which can be used to switch to the mode in the level editor. This can look like this, but you have to look at the specific implementation of the inhereted class. You will probably need to implementent a build methode that is allready wrapped to enhance easier implementation of new models. The State of the previous Component can look like this:

    ```csharp
    using __ProjectMain.Scripts.LevelEditor.Components;
    using __ProjectMain.Scripts.Managers;
    using __ProjectMain.Scripts.Managers.LevelEditor;
    using __ProjectMain.Scripts.Utilities.LevelEditor;
    using UnityEngine;

    namespace __ProjectMain.Scripts.LevelEditor.StateMachine.BuildStates
    {
        public class GoalBuildState : OnePointsBuildState, ISelectableState
        {
            
            protected override bool Build(Vector3Int pos)
            {
                var goal = new GoalComponent(LevelEditorUtils.ReduceToTwoDimensions(pos),
                    LevelEditorFileManager.Instance.levelData);
                LevelEditorFileManager.Instance.levelData.components.Add(goal);
                return base.Build(pos);
            }
            
            public Sprite GetIcon()
            {
                return LevelEditorManager.Instance.goalSprite;
            }

            public string GetName()
            {
                return "Goal";
            }
        }
    }
    ```
    > in this implementation add the goalSprite in the `LevelEditorManager` Singleton

3. Add the Placeholder in the `LevelEditorManager` Singleton class
    Add the Sprite under `[Header("Build Menu Sprites")]` and a Tile under         `[Header("Tiles")]`
    ```csharp
    [Header("Tiles")]
    //...
    public Tile goalTile;
    //...
    [Header("Build Menu Sprites")]
    //...
    public Sprite goalSprite;
    //...
    ```
4. Add to State Machine
    Add an Object reference in the `LevelEditorStateMachine` class.
    ```csharp
    public GoalBuildState GoalBuildState { get; } = new();
    ```
    (Optional) If you implemented the interface `ISelectableState` you need to add this reference to the `_selectableState` in the `Init` methode of the `LevelEditorManager` class
    ```csharp
    _selectableStates = new ISelectableState[]
        {
            _levelEditorStateMachine.WallBuildState,
            _levelEditorStateMachine.DeleteComponentsState,
            _levelEditorStateMachine.SpawnPointBuildState,
            _levelEditorStateMachine.GoalBuildState,
            _levelEditorStateMachine.FireWallBuildState,
            _levelEditorStateMachine.AdjustComponentState,
            _levelEditorStateMachine.LaptopBuildState
        };
    ```

5. Add representation in the `ShowRepresentationInTilemap()` methode of the `LevelEditorManager` class
    If you had added a special component that should be representated by a Tile (like the one we created). You should a foreach loop here. This can look like this

    ```csharp
    foreach (var goalComponent in LevelEditorUtils.FilterComponents(LevelEditorFileManager.Instance.levelData.components, typeof(GoalComponent)).Select(component => ((GoalComponent)component)))
    {
        LevelManager.Instance.levelEditorRepresentationTilemap.SetTile(LevelEditorUtils.ExpandToThreeDimensions(goalComponent.position), goalTile);
    }
    ```
> This is all. The Component is now present in the `Level Editor`


### Ingame Presents

For the highest possible modularity of this system you will need to add a `ComponentPlacer` or add a simple Place Snippet.

1. (Optional) Create a the PlacerClass. This may only be necessary if the Component requires scaling etc. for performance or if it  needs connection with other components.

2. Within the `Init()` methode of the `LevelLoaderManager` class add a new case in the switch statement add the end of the  methode. You can see how to call the Placer there. In our case we do not need a Placer. We just Instantiate the Object and change the position
    > keep in mind that you need to create this Object as a field in this class