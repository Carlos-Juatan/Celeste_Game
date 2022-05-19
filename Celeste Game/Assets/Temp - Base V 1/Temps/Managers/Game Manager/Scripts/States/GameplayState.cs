using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class GameplayState : GameBaseState
	{
        //Timer _timer;
        public override void EnterState()
        {
            GameManager.instance.Data.FindGameplayComponents();
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(inputManager._InputGameplayState);
        }

		public override void ExitState()
        {
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(null);
        }
    }
}