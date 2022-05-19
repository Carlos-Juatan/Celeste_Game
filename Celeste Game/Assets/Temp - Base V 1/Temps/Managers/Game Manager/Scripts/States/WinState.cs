using UnityEngine;

namespace SystemManager.GameManagement
{
	public class WinState : GameBaseState
	{
        public override void EnterState()
        {
            // Get All Scenes length on Build
            var managerData = GameManager.instance.Data;
            int allScenesCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            int allLevelsCount = allScenesCount - managerData.NoLevelScenesCount;
            int lastLevelUnlocked = PlayerPrefs.GetInt("UnlockLevelID");

            if(lastLevelUnlocked < allLevelsCount){
                if(lastLevelUnlocked -1 < managerData.GameLevelIndex - (managerData.LevelScenestartBy -1) ){
                    PlayerPrefs.SetInt("UnlockLevelID", lastLevelUnlocked +1);
            }}
            
            startBase += managerData.GameplayUIController.HasWin;
        }
        
		public override void ExitState()
        {
            var managerData = GameManager.instance.Data;
            startBase -= managerData.GameplayUIController.HasWin;
        }
    }
}