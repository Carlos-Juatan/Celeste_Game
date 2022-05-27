using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SystemManager.GameManagement;
using SystemManager;

namespace GameSystems.SimpleUI.GameplayUI
{
	public class GameplayUIController : MonoBehaviour
	{
		[Header("Panels")]
		[SerializeField] GameObject gameplayPanel;
		[SerializeField] GameObject pausePainel;
		[SerializeField] GameObject failPainel;
		[SerializeField] GameObject winPainel;
		
		[Header("Main Page Buttons")]
		[SerializeField] Button pause_Btn;
		[SerializeField] Button resume_Btn;
		[SerializeField] Button repeat_Btn;
		[SerializeField] Button options_Btn;
		[SerializeField] Button pauseQuit_Btn;

		[Header("Win Page Buttons")]
		[SerializeField] Button next_Btn;
		[SerializeField] Button winQuit_Btn;

		[Header("Lose Page Buttons")]
		[SerializeField] Button failQuit_Btn;
		[SerializeField] Button failRepeat_Btn;
		
		[Header("Timer Options")]
		[SerializeField] TMP_Text timeText;
		
		[SerializeField] TimerType timerType;
		[SerializeField] int startTime = 120;
		
		Timer timer;
		
		GameObject lastPanel;
		
		bool hasPaused = false, hasWin = false, hasFail = false;
		
#region Unity Methods.
		void Start() => Initialize();
#endregion

#region External Reaction Methods.
		// Called by Return Buttons "Escape" or UI Return Button.
		public void ChangePauseStates(bool hasPress)
		{
			if(hasPress){
				if(!hasFail && !hasWin)
				{
					hasPaused = !hasPaused;
					gameplayPanel.SetActive(!hasPaused);
					pausePainel.SetActive(hasPaused);
					lastPanel = hasPaused ? pausePainel : gameplayPanel;
					//GameBaseState status = hasPaused ? GameManager.instance.PauseState : GameManager.instance.GameplayState;
					GameBaseState state;
					if(hasPaused){
						state = GameManager.instance.PauseState;
					}else{
						state = GameManager.instance.GameplayState;
					}
					GameManager.instance.SwitchState(state);
					ButtonSelector.UIButtonSelector.instance.Select(hasPaused ? resume_Btn.gameObject : null);
			}}
		}

		// Called by GameManager Win State
		public void HasWin()
		{
			if(!hasFail)
			{
				hasWin = !hasWin;
				lastPanel.SetActive(!hasWin);
				winPainel.SetActive(hasWin);
				GameObject selectOff = null;
				if(lastPanel == pausePainel) { selectOff = resume_Btn.gameObject; }
				ButtonSelector.UIButtonSelector.instance.Select(hasWin ? next_Btn.gameObject : selectOff);
			}
		}
		
		// Called by GameManager Lose State
		public void HasFail()
		{
			if(!hasWin)
			{
				hasFail = !hasFail;
				lastPanel.SetActive(!hasFail);
				failPainel.SetActive(hasFail);
				GameObject selectOff = null;
				if(lastPanel == pausePainel) { selectOff = resume_Btn.gameObject; }
				ButtonSelector.UIButtonSelector.instance.Select(hasFail ? failRepeat_Btn.gameObject : selectOff);
			}
		}
#endregion

#region Buttons Methods.
		void pausePress() => ChangePauseStates(true);
		void resumePress() => ChangePauseStates(true);

		void repeatPress()
		{
			var loadLvl = GameManager.instance.Data.LoadLevel;
			if(loadLvl != null) loadLvl.ReloadLevel(GameManager.instance.GameplayState);
		}
		void nextPress()
		{
			var loadLvl = GameManager.instance.Data.LoadLevel;
			if(loadLvl != null) loadLvl.LoadNextLevel(GameManager.instance.GameplayState);
		}
		void optionsPres()
		{
			// Do Something.
		}
		void QuitPress()
		{
			int id = (int)SystemManager.LevelLoader.ScenesIndexes.MainMenu;
			int[] unloadScenes = {GameManager.instance.Data.GameLevelIndex, (int)SystemManager.LevelLoader.ScenesIndexes.Character};
			int[] loadScenes = {id};

			var loadLvl = GameManager.instance.Data.LoadLevel;
			if(loadLvl != null) loadLvl.LoadIndexLevel(unloadScenes, loadScenes, GameManager.instance.MainMenuState);
		}
#endregion

#region Private Methods.
		void Initialize()
		{
			SystemDebug("["+this.name+"] Has initialized.");
			
			Canvas canvas = GetComponent<Canvas>();
			canvas.worldCamera = Camera.main;
			canvas.sortingLayerName = "UI";

			timer = new Timer(timerType, timeText, startTime);
			
			// Buttons AddListeners.
				// Main Page Buttons.
			pause_Btn.onClick.AddListener(pausePress);
			resume_Btn.onClick.AddListener(resumePress);
			repeat_Btn.onClick.AddListener(repeatPress);
			options_Btn.onClick.AddListener(optionsPres);
			pauseQuit_Btn.onClick.AddListener(QuitPress);
				// Win Page Buttons.
			next_Btn.onClick.AddListener(nextPress);
			winQuit_Btn.onClick.AddListener(QuitPress);
				// Lose Page Buttons.
			failRepeat_Btn.onClick.AddListener(repeatPress);
			failQuit_Btn.onClick.AddListener(QuitPress);
			
			lastPanel = gameplayPanel;

			Color color;
			if ( ColorUtility.TryParseHtmlString("#000000", out color)){
				Camera.main.backgroundColor = color;
			}
		}

		void SystemDebug(string msg) => GameDebug.Debug(SystemManager.DebugType.GameplayUIOnly, msg);
#endregion
	}
}