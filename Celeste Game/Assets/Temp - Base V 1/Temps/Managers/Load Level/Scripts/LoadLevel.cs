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
			if (SceneManager.GetSceneByBuildIndex((int)SystemManager.LevelLoader.ScenesIndexes.Logo).isLoaded)
			{
				SystemManager.InitializeSystem.InitializeGame reference = FindObjectOfType<SystemManager.InitializeSystem.InitializeGame>();
				reference.GetComponent<Canvas>().worldCamera = Camera.main;
				Invoke("FinishingLogo", _timeToShowTheLogo);
			}
		}
		
		public void LoadMainMenu(){
			SceneManager.LoadSceneAsync((int)SystemManager.LevelLoader.ScenesIndexes.MainMenu, LoadSceneMode.Additive);
		}
		
		public void ReloadLevel(GameBaseState state)
		{
			if(loadSteps == LoadSteps.FadeIn)
			{
				loadSteps = LoadSteps.StartLoad;
				int level = GameManager.instance.Data.GameLevelIndex;
				StartCoroutine(LoadLevelAnimations(level, level, state));
			}
		}
		
		public void LoadNextLevel(GameBaseState state)
		{
			if(loadSteps == LoadSteps.FadeIn)
			{
				loadSteps = LoadSteps.StartLoad;
				int level = GameManager.instance.Data.GameLevelIndex;
				
				if(UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings > level + 1)
				{
					GameManager.instance.Data.GameLevelIndex = level +1;
					StartCoroutine(LoadLevelAnimations(level, level +1, state));
				}else
				{
					StartCoroutine(LoadLevelAnimations(level,
					(int)SystemManager.LevelLoader.ScenesIndexes.MainMenu, GameManager.instance.MainMenuState));
				}
			}
		}
		
		public void LoadIndexLevel(int UnloadIndex, int sceneIndex, GameBaseState state)
		{
			if(loadSteps == LoadSteps.FadeIn)
			{
				GameManager.instance.Data.GameLevelIndex = sceneIndex;
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
		
		void UnloadLogo() => SceneManager.UnloadSceneAsync((int)SystemManager.LevelLoader.ScenesIndexes.Logo);

		void SystemDebug(string msg) => GameDebug.Debug(SystemManager.DebugType.LoadLevelOnly, msg);
#endregion

#region Coroutine Methods.
		IEnumerator TriggerTrasitionAnimation(int animationCode, float timeToStart)
		{
			yield return new WaitForSeconds(timeToStart);
			transitions.SetTrigger(animationCode);
		}

		IEnumerator LoadLevelAnimations(int UnloadIndex, int sceneIndex, GameBaseState state)
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

		IEnumerator LoadAsynchronously(int UnloadIndex, int sceneIndex, GameBaseState state)
		{
			loadOperation.Add(SceneManager.UnloadSceneAsync(UnloadIndex));
			loadOperation.Add(SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive));
			
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
