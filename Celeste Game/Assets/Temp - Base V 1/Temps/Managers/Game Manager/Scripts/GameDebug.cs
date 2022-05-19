using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemManager
{
	public static class GameDebug
	{
		public static DebugType currentDebugType;

#region Public Methods.
		
		public static void Debug(DebugType type, string msg)
		{
			if(currentDebugType == DebugType.AllDebugs || currentDebugType == type)
			{
#if UNITY_EDITOR
				UnityEngine.Debug.Log(msg);
#endif
			}
			
		}
		
		public static void DebugWarning(DebugType type, string msg)
		{
			if(currentDebugType == DebugType.AllDebugs || currentDebugType == type)
			{
#if UNITY_EDITOR
				UnityEngine.Debug.LogWarning(msg);
#endif
			}
		}
		
		public static void DebugError(DebugType type, string msg)
		{
			if(currentDebugType == DebugType.AllDebugs || currentDebugType == type)
			{
#if UNITY_EDITOR
				UnityEngine.Debug.LogError(msg);
#endif
			}
		}
#endregion
	}

	public enum DebugType
	{
		None,
		GameManagerOnly,
		SoundsManagerOnly,
		LoadLevelOnly,
		MainMenuOnly,
		GameplayUIOnly,
		InpuManagerOnly,
		InteractionsOnly,
		AllDebugs
	}
}
