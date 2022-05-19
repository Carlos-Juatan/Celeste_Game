using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class MainMenuState : GameBaseState
	{
        public override void EnterState()
        {
            GameManager.instance.Data.FindMainMenuComponents();
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(inputManager._InputMainMenuState);
        }
        
		public override void ExitState()
        {
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(null);
        }
    }
}