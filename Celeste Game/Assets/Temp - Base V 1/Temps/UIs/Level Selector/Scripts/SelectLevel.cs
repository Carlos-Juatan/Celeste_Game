using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems.SimpleUI.LevelSelectorManager
{
	public class SelectLevel : MonoBehaviour
	{
		[SerializeField] GameObject levelBtn;
		[SerializeField] Transform content;
		
		List<GameObject> allButtons = new List<GameObject>();
		int crescentId = 1;
		
#region Public Methods
		public void Initialize()
		{
			if(PlayerPrefs.GetInt("UnlockLevelID") < 1) PlayerPrefs.SetInt("UnlockLevelID", 1);
			UpdateScreen();
		}
		public void ReloadLevels() => UpdateScreen();
#endregion

#region Private Methods
		void UpdateScreen()
		{
			ClearScreen();
			var managerData = SystemManager.GameManagement.GameManager.instance.Data;
			int allLevel = SceneManager.sceneCountInBuildSettings - managerData.NoLevelScenesCount;
			if(allLevel > 0)
			{
				for(int i = 0; i < allLevel; i++)
				{
					CreateBtn(i + managerData.LevelScenestartBy, crescentId++);
					if(i + 1 == PlayerPrefs.GetInt("UnlockLevelID")) { break; }
				}
			}
		}
		
		void ClearScreen()
		{
			crescentId = 1;
			foreach(GameObject obj in allButtons)
			{
				Destroy(obj);
			}
		}
		
		void CreateBtn(int id, int referenceId)
		{
			var btn = Instantiate(levelBtn, content.position, Quaternion.identity, content);
			allButtons.Add(btn);
			btn.GetComponent<LevelButton>().Initialize(id, referenceId);
		}
#endregion
	}
}