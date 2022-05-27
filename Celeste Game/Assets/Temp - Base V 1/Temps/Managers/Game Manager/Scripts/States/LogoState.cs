using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class LogoState : GameBaseState
	{
        public override void EnterState()
        {
            GameManager.instance.MainMenuSnowParticle.SetActive(true);
        }

		public override void ExitState()
        {
            GameManager.instance.MainMenuSnowParticle.SetActive(false);
        }
    }
}