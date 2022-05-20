using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using SystemManager.GameManagement;
using SystemManager;

namespace SystemManager.LevelLoader
{
	public class LoadLevel : MonoBehaviour
	{
		[SerializeField] GameObject loadingScreen;
		[SerializeField] Slider slider;
		[SerializeField] TMP_Text progressText;
		
		[SerializeField] Animator transitions;
		static readonly int anim_Start = Animator.StringToHash("Start");
		static readonly int anim_End = Animator.StringToHash("End");
		
		[SerializeField] float transitionTime = 1f;

		[SerializeField] float _timeToShowTheLogo = 2f;
		
		LoadSteps loadSteps = new LoadSteps();
		
#region Unity Methods.
		void Awake() => Initialize();
#endregion

#region Public Methods.
		public void Initialize()
		{
			SystemDebug("["+this.name+":] Has Initialize.");
			loadSteps = LoadSteps.FadeIn;
			if (SceneManager.GetSceneByBuildIndex((int)ScenesIndexes.Logo).isLoaded)
			{
				InitializeSystem.InitializeGame reference = FindObjectOfType<InitializeSystem.InitializeGame>();
				reference.GetComponent<Canvas>().worldCamera = Camera.main;
				Invoke("FinishingLogo", _timeToShowTheLogo);
			}
		}
		
		public void LoadMainMenu(){
			SceneManager.LoadSceneAsync((int)ScenesIndexes.MainMenu, LoadSceneMode.Additive);
		}
		
		public void ReloadLevel(GameBaseState state)
		{
			if(loadSteps == LoadSteps.FadeIn)
			{
				loadSteps = LoadSteps.StartLoad;
				int[] level = new int[]{
					GameManager.instance.Data.GameLevelIndex,
					(int)ScenesIndexes.Character
				};
				StartCoroutine(LoadLevelAnimations(level, level, state));
			}
		}
		
		public void LoadNextLevel(GameBaseState state)
		{
			if(loadSteps == LoadSteps.FadeIn)
			{
				loadSteps = LoadSteps.StartLoad;
				int[] unloadLevel = new int[]{
					GameManager.instance.Data.GameLevelIndex,
					(int)ScenesIndexes.Character
				};

				int[] nextLevel = new int[]{
					GameManager.instance.Data.GameLevelIndex + 1,
					(int)ScenesIndexes.Character
				};
				
				if(UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings > GameManager.instance.Data.GameLevelIndex + 1)
				{
					//GameManager.instance.Data.GameLevelIndex += 1;
					StartCoroutine(LoadLevelAnimations(unloadLevel, nextLevel, state));
				}else
				{
					int[] loadScene = new int[] { (int)ScenesIndexes.MainMenu };
					StartCoroutine(LoadLevelAnimations(unloadLevel, loadScene, GameManager.instance.MainMenuState));
				}
			}
		}
		
		public void LoadIndexLevel(int[] UnloadIndex, int[] sceneIndex, GameBaseState state)
		{
			if(loadSteps == LoadSteps.FadeIn)
			{
				int currentScene = 0;
				foreach(int scene in sceneIndex){
					if(scene != (int)ScenesIndexes.Character)
						currentScene = scene;
				}
				GameManager.instance.Data.GameLevelIndex = currentScene;
				loadSteps = LoadSteps.StartLoad;
				StartCoroutine(LoadLevelAnimations(UnloadIndex, sceneIndex, state));
			}
		}
		
#endregion

#region Private Methods.
		void FinishingLogo()
		{
			StartCoroutine(TriggerTrasitionAnimation(anim_Start, 0f));
			
			Invoke("UnloadLogo", 1);
			Invoke("LoadMainMenu", 1);
			
			StartCoroutine(TriggerTrasitionAnimation(anim_End, 1f));
		}
		
		void UnloadLogo() => SceneManager.UnloadSceneAsync((int)ScenesIndexes.Logo);

		void SystemDebug(string msg) => GameDebug.Debug(SystemManager.DebugType.LoadLevelOnly, msg);
#endregion

#region Coroutine Methods.
		IEnumerator TriggerTrasitionAnimation(int animationCode, float timeToStart)
		{
			yield return new WaitForSeconds(timeToStart);
			transitions.SetTrigger(animationCode);
		}

		IEnumerator LoadLevelAnimations(int[] UnloadIndex, int[] sceneIndex, GameBaseState state)
		{
			GameManager.instance.SwitchState(GameManager.instance.LoadingState);
			transitions.SetTrigger(anim_Start);
			
			yield return new WaitForSeconds(transitionTime);
			
			if(loadSteps == LoadSteps.StartLoad)
			{
				loadSteps = LoadSteps.FadeOut;
				StartCoroutine(LoadAsynchronously(UnloadIndex, sceneIndex, state));
			}
		}
		
		List<AsyncOperation> loadOperation = new List<AsyncOperation>();

		IEnumerator LoadAsynchronously(int[] UnloadIndex, int[] sceneIndex, GameBaseState state)
		{
			// Add Scenes to Unload.
			if(UnloadIndex != null){
				if(UnloadIndex.Length >= 1){
					foreach(int id in UnloadIndex){
						loadOperation.Add(SceneManager.UnloadSceneAsync(id));
			}}}

			// Add Scenes to Load.
			if(sceneIndex != null){
				if(sceneIndex.Length >= 1){
					foreach(int id in sceneIndex){
						loadOperation.Add(SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive));
			}}}
			
			loadingScreen.SetActive(true);
			
			int lenth = loadOperation.Count;
			for(int i = 0; i < lenth; i++)
			{
				while(!loadOperation[i].isDone)
				{
					float totalProgress = 0;
					
					foreach(AsyncOperation operation in loadOperation)
					{
						totalProgress += operation.progress;
					}
					
					float progress = Mathf.Clamp01((totalProgress / lenth) / .9f);
					
					slider.value = progress;
					progressText.text = progress * 100f + "%";
					
					if(progress == 1f)
					{
						if(loadSteps == LoadSteps.FadeOut)
						{
							loadSteps = LoadSteps.FadeIn;
							StartCoroutine(EndTrasitionAnimation(state));
						}
						
					}
					
					yield return null;
				}
			}
		}
		
		IEnumerator EndTrasitionAnimation(GameBaseState state)
		{
			transitions.SetTrigger(anim_End);
			
			yield return new WaitForSeconds(transitionTime);
			
			GameManager.instance.SwitchState(state);

			SystemDebug("["+this.name+":] Fade Out Has Finishing.");
		}
#endregion
	}
	
	enum LoadSteps
	{
		FadeIn,
		StartLoad,
		FadeOut
	}
}
