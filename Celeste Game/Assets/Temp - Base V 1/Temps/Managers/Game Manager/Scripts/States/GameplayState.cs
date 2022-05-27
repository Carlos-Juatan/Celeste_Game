using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class GameplayState : GameBaseState
	{
        //Timer _timer;
        public override void EnterState()
        {
            GameManager.instance.GameplaySnowParticle.SetActive(true);

            GameManager.instance.Data.FindGameplayComponents();

            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(inputManager._InputGameplayState);

            updateBase += GameManager.instance.Data.PlayerController.UpdatePlayer;
            fixedUpdateBase += GameManager.instance.Data.PlayerController.FixedUpdatePlayer;
        }

		public override void ExitState()
        {
            GameManager.instance.GameplaySnowParticle.SetActive(false);

            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(null);

            updateBase -= GameManager.instance.Data.PlayerController.UpdatePlayer;
            fixedUpdateBase -= GameManager.instance.Data.PlayerController.FixedUpdatePlayer;
        }
    }
}