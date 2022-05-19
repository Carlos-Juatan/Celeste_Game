using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SystemManager.GameManagement
{
#if UNITY_EDITOR
	[CustomEditor(typeof(GameManager))]
	public class GameManagerEditor : Editor
	{
		GameManager gameManager;
		
#region Unity Methods.
		void OnEnable()
		{
			gameManager = (GameManager)target;
		}
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			//SystemManager.GameDebug.currentDebugType = gameManager.GetDebugType();
		}
		
		public void OnSceneGUI()
		{
			//SystemManager.GameDebug.currentDebugType = gameManager.GetDebugType();
		}
#endregion
	}
#endif
}
