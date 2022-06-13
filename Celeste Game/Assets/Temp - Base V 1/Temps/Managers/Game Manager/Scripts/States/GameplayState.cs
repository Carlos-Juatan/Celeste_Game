using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class GameplayState : GameBaseState
	{
        //Timer _timer;
        public override void EnterState()
        {
            // Enable Snow particles
            GameManager.instance.GameplaySnowParticle.SetActive(true);

            // Find gameplay objects
            GameManager.instance.Data.FindGameplayComponents();

            // Link input Manager
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(inputManager._InputGameplayState);

            // Link Player
            updateBase += GameManager.instance.Data.PlayerController.UpdatePlayer;
            fixedUpdateBase += GameManager.instance.Data.PlayerController.FixedUpdatePlayer;

            // Link Follow Camera
            GameManager.instance.Data.FollowCamera.GameplayStart();
            fixedUpdateBase += GameManager.instance.Data.FollowCamera.GameplayFixedUpdate;
        }

		public override void ExitState()
        {
            // desable the snow particles
            GameManager.instance.GameplaySnowParticle.SetActive(false);

            // Unlink input Manager
            InputManager inputManager = GameManager.instance.Data.InputManager;
            inputManager.SwitchState(null);

            // Unlink player
            updateBase -= GameManager.instance.Data.PlayerController.UpdatePlayer;
            fixedUpdateBase -= GameManager.instance.Data.PlayerController.FixedUpdatePlayer;

            // Unlink follow camera
            fixedUpdateBase += GameManager.instance.Data.FollowCamera.GameplayFixedUpdate;
        }
    }
}