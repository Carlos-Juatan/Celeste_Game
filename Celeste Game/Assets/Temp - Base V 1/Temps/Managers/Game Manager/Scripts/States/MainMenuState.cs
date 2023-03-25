using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class MainMenuState : GameBaseState
	{
        public override void EnterState()
        {
            GameManager.instance.MainMenuSnowParticle.SetActive(true);

            GameManager.instance.Data.FindMainMenuComponents();
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(inputManager._InputMainMenuState);

        }
        
		public override void ExitState()
        {
            GameManager.instance.MainMenuSnowParticle.SetActive(false);

            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(null);
        }
    }
}