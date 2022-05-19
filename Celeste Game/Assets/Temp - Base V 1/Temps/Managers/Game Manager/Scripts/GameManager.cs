using UnityEngine;

namespace SystemManager.GameManagement
{
	public class GameManager : MonoBehaviour
	{
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
			_currentState = LogoState;

			// Finding components.
			_data.FindConsistentComponents();

			// Set the Debug type
			GameDebug.currentDebugType = _data.DebugType;

			// Starting the begin state.
			_currentState.EnterState();
		}

		void Start() => _currentState.StartState();

		void Update() => _currentState.UpdateState();

		void FixedUpdate() => _currentState.FixedUpdateState();

		void LateUpdate() => _currentState.LateUpdateState();
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
	}
}