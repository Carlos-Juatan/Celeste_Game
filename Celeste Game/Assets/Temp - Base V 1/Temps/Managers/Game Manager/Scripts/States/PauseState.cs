using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class PauseState : GameBaseState
	{
        public override void EnterState()
        {
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(inputManager._InputPauseState);
        }
        
		public override void ExitState()
        {
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(null);
        }
    }
}