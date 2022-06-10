using UnityEngine;

namespace SystemManager.GameManagement
{
	public class PlayerIsDeadState : GameBaseState
	{
        public override void EnterState()
        {
            GameManager.instance.Data.LoadLevel.ReloadLevel(GameManager.instance.GameplayState);
        }
        
		public override void ExitState()
        {
            
        }
    }
}