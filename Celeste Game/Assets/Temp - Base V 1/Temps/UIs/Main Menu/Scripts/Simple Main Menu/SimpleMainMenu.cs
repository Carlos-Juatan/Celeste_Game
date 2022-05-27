using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemManager;
using SystemManager.GameManagement;

namespace GameSystems.SimpleUI.MainMenu
{
	public class SimpleMainMenu : MonoBehaviour
	{
		[SerializeField] Button play_BTN, options_BTN, quit_BTN, levelBack_BTN, optionsBack_BTN, DeletSave_Btn;
		
		[SerializeField] GameObject mainPanel, levelPlanel, optionsPanel;
		GameObject currentPainel, lastMainButton;
		
		bool canBack = false;
		
#region Unity Methods.
	void Awake() => Initialize();
#endregion

#region Unity hierarchically Methods.
        // Called by Input Event when press esc button
		public void BackButton(bool hasPress)
		{
			if(hasPress){
				if(canBack)
				{
					ChangePanel(mainPanel, currentPainel, lastMainButton);
			}}
		}
#endregion

#region Public Methods.
		public void Initialize()
		{
			SystemDebug("["+this.name+"] Has initialized.");
			
			Canvas canvas = GetComponent<Canvas>();
			canvas.worldCamera = Camera.main;
			canvas.sortingLayerName = "UI";
			
			// Buttons AddListeners.
			play_BTN.onClick.AddListener(PlayPress);
			options_BTN.onClick.AddListener(OptionsPress);
			quit_BTN.onClick.AddListener(QuitPress);
			DeletSave_Btn.onClick.AddListener(DeletSave);
			// AddListener With Parameters.
			levelBack_BTN.onClick.AddListener(delegate{BackButton(true);});
			optionsBack_BTN.onClick.AddListener(delegate{BackButton(true);});
			
			// Others.
			currentPainel = mainPanel;
			//ButtonSelector.UIButtonSelector.instance.Select(play_BTN.gameObject);
			
			Color color;
			if ( ColorUtility.TryParseHtmlString("#43165C", out color)){
				Camera.main.backgroundColor = color;
			}

			try { this.GetComponent<LevelSelectorManager.SelectLevel>().Initialize(); }
			catch {SystemDebug("["+this.name+"] SelectLevel Don't Found.");}

#if UNITY_EDITOR
			GameManager.instance.SwitchState(GameManager.instance.MainMenuState);
#endif
		}
#endregion

#region Private Methods.
		void PlayPress()
		{
			lastMainButton = play_BTN.gameObject;
			ChangePanel(levelPlanel, currentPainel, levelBack_BTN.gameObject);
		}
		
		void OptionsPress()
		{
			lastMainButton = options_BTN.gameObject;
			ChangePanel(optionsPanel, currentPainel, optionsBack_BTN.gameObject);
		}
		
		void QuitPress()
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
		
		void DeletSave()
		{
			PlayerPrefs.SetInt("UnlockLevelID", 1);
			var p = GetComponent<LevelSelectorManager.SelectLevel>();
			if(p != null) { p.ReloadLevels(); }
		}
		
		void ChangePanel(GameObject onPanel = null, GameObject offPanel = null, GameObject buttonSelect = null)
		{
			if(offPanel != null) { offPanel.SetActive(false); }
			if(onPanel != null) { onPanel.SetActive(true); currentPainel = onPanel; }
			if(onPanel != mainPanel) { canBack = true; } else { canBack = false; }
			ButtonSelector.UIButtonSelector.instance.Select(buttonSelect);
		}

		void SystemDebug(string msg) => GameDebug.Debug(SystemManager.DebugType.MainMenuOnly, msg);
#endregion
	}
}