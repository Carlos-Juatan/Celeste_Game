using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SystemManager.GameManagement;

namespace GameSystems.SimpleUI.LevelSelectorManager
{
	public class LevelButton : MonoBehaviour
	{
		[SerializeField] Button level_Btn;
		[SerializeField] TMP_Text lvlText;
		[SerializeField] int MainMenuIndex = 2;
		
		int id = 0;
		
#region Public Methods.
		public void Initialize(int id, int referenceId)
		{
			this.id = id;
			lvlText.text = referenceId.ToString();
			level_Btn.onClick.AddListener(PressButton);
		}
#endregion

#region Private Methods.
		void PressButton()
		{
			var loadLvl = GameManager.instance.Data.LoadLevel;
			if(loadLvl != null) loadLvl.LoadIndexLevel(MainMenuIndex, id, GameManager.instance.GameplayState);
			//loadLvl.SendMessage("LoadIndexLevel", id, GameState.Gameplay);
		}
#endregion
	}
}