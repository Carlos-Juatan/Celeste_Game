using UnityEngine;

using SystemManager.SoundManagement;
using SystemManager.InputManagement;
using SystemManager.LevelLoader;

using GameSystems.SimpleUI.GameplayUI;
using GameSystems.SimpleUI.MainMenu;

//using GameAssets.PlayerSystem;
//using GameAssets.MapManagement.EventInteractions;

namespace SystemManager.GameManagement
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/Game Management/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [Header("Game Debug and State Type")]
        [SerializeField] DebugType _debugType;

        [Header("Load and Select level")]
        public int GameLevelIndex;
        public int NoLevelScenesCount = 4;
        public int LevelScenestartBy = 4;

        // Components.
        SoundsManager _soundsManager;
        LoadLevel _loadLevel;
        InputManager _inputSystem;
        Camera _mainCamera;
        GameplayUIController _gameplayUIController;
        SimpleMainMenu _simpleMainMenu;

#region Gets And Sets.
        // Components.
        public DebugType DebugType          { get { return _debugType; } }
        public SoundsManager SoundsManager  { get { return _soundsManager; } }
        public LoadLevel LoadLevel          { get { return _loadLevel; } }
        public InputManager InputManager    { get { return _inputSystem; } }
        public Camera MainCamera            { get { return _mainCamera; } }
        public GameplayUIController GameplayUIController { get { return _gameplayUIController; } }
        public SimpleMainMenu SimpleMainMenu             { get { return _simpleMainMenu; } }
#endregion

#region Find Components.
        // Responsable to find components on Consistent scene start
        public void FindConsistentComponents()
        {
            _mainCamera = Camera.main;
            _soundsManager = FindObjectOfType<SoundsManager>();
			_loadLevel = FindObjectOfType<LoadLevel>();
			_inputSystem = FindObjectOfType<InputManager>();
        }

        // Responsable to find components on Main Menu scene start
        public void FindMainMenuComponents()
        {
            _simpleMainMenu = FindObjectOfType<SimpleMainMenu>();
        }

        // Responsable to find components on Gameplay scene start
        public void FindGameplayComponents()
        {
			_gameplayUIController = FindObjectOfType<GameplayUIController>();
        }
#endregion
    }
}