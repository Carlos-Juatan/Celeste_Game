using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SimpleUI.GameplayUI
{
	public class GameplayUITest : MonoBehaviour
	{
		[SerializeField] KeyCode winKey, loseKey;
		
		GameplayUIController uiController => GetComponent<GameplayUIController>();
		
#region Unity Methods.
		void Update()
		{
			if(Input.GetKeyDown(winKey))
			{
				uiController.HasWin();
			}
			
			if(Input.GetKeyDown(loseKey))
			{
				uiController.HasFail();
			}
		}
#endregion
	}
}