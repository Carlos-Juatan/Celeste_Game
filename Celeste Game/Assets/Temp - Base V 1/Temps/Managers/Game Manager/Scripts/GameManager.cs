using UnityEngine;

namespace SystemManager.GameManagement
{
	public class GameManager : MonoBehaviour
	{
#region Var.
		public static GameManager instance { get; private set; }

		[SerializeField] GameData _data;
		public GameData Data { get{ return _data; } }

		[SerializeField]GameBaseState _currentState;
		public LogoState LogoState { get; private set; }
		public MainMenuState MainMenuState { get; private set; }
		public LoadingState LoadingState { get; private set; }
		public GameplayState GameplayState { get; private set; }
		public PauseState PauseState { get; private set; }
		public LoseState LoseState { get; private set; }
		public WinState WinState { get; private set; }
		public PlayerIsDeadState PlayerIsDeadState { get; private set; }
		
        [Header("Visual Effects")]
        [SerializeField] GameObject _gameplaySnowParticle;
        [SerializeField] GameObject _mainMenuSnowParticle;

#endregion

#region Getters and Setters.

        // Effects
        public GameObject GameplaySnowParticle { get { return _gameplaySnowParticle; } }
        public GameObject MainMenuSnowParticle { get { return _mainMenuSnowParticle; } }

#endregion

#region Unity Methods.
		void Awake()
		{
			// Create a instance of this class.
			if(instance == null) { instance = this; } else { Destroy(this.gameObject); }

			// Create a instance of the states.
			LogoState = new LogoState();
			MainMenuState = new MainMenuState();
			LoadingState = new LoadingState();
			GameplayState = new GameplayState();
			PauseState = new PauseState();
			LoseState = new LoseState();
			WinState = new WinState();
			PlayerIsDeadState = new PlayerIsDeadState();
			_currentState = LogoState;

			// Finding components.
			_data.FindConsistentComponents();

			// Set the Debug type
			GameDebug.currentDebugType = _data.DebugType;

			// Starting the begin state.
			_currentState.EnterState();

			Invoke("CheckIfIsOnLevelScene", 1);
		}

		void Start() => _currentState.StartState();

		void Update() => _currentState.UpdateState();

		void FixedUpdate() => _currentState.FixedUpdateState();

		void LateUpdate() => _currentState.LateUpdateState();

		void OnGUI() => _currentState.OnGUIState();
#endregion

#region Public Methods.
		public void SwitchState(GameBaseState newState)
		{
			_currentState.ExitState();
			_currentState = newState;
			GameDebug.Debug(DebugType.GameManagerOnly, "["+this.name+"] Change GameState for " + newState.GetType().Name);
			_currentState.EnterState();
			_currentState.StartState();
		}
#endregion

#region Private Methods
		void CheckIfIsOnLevelScene(){
#if UNITY_EDITOR
			int allSceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
			UnityEngine.SceneManagement.Scene[] allScenes = UnityEngine.SceneManagement.SceneManager.GetAllScenes();

			for(int i = GameManager.instance.Data.LevelScenestartBy; i < allSceneCount; i++){
				foreach (var scene in allScenes){
					if(scene.buildIndex == i){
						GameManager.instance.Data.GameLevelIndex = i;
						GameManager.instance.SwitchState(GameManager.instance.GameplayState);
			}}}
#endif
		}
#endregion
	}
}