using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.SimpleUI.ButtonSelector
{
	public class UIButtonSelector : MonoBehaviour
	{
		public static UIButtonSelector instance { get; private set; }
		
		[SerializeField] EventSystem eventSystem;
		
		GameObject currentSelect, lastSelect;

#region Unity Methods.
		void OnEnable()
		{
			if(instance == null) { instance = this; }
			else { Destroy(this.gameObject); }
		}
		void Update() => CurrentUpdate();
#endregion

#region Public Methods.
		public void Select(GameObject selection)
		{
			eventSystem.SetSelectedGameObject(selection);
			lastSelect = selection;
		}
#endregion

#region Private Methods.
		void CurrentUpdate()
		{
			currentSelect = eventSystem.currentSelectedGameObject;
			if(currentSelect == null)
			{
				eventSystem.SetSelectedGameObject(lastSelect);
			}
			else
			{
				lastSelect = currentSelect;
			}
		}
#endregion
	}
}