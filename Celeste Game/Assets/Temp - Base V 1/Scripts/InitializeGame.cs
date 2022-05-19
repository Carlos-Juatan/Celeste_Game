using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SystemManager.GameManagement;

namespace SystemManager.InitializeSystem
{
	public class InitializeGame : MonoBehaviour
	{
#region Unity Methods.
		void Awake() => Initialize();
#endregion

#region Private Methods.
		void Initialize()
		{
			SceneManager.LoadSceneAsync(
				(int)SystemManager.LevelLoader.ScenesIndexes.PersistentScene,LoadSceneMode.Additive
			);
		}
#endregion
	}
}