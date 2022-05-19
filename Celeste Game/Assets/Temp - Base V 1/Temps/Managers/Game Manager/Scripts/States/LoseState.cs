using UnityEngine;

namespace SystemManager.GameManagement
{
	public class LoseState : GameBaseState
	{
        public override void EnterState()
        {
            startBase += GameManager.instance.Data.GameplayUIController.HasFail;
        }
        
		public override void ExitState()
        {
            startBase -= GameManager.instance.Data.GameplayUIController.HasFail;
        }
    }
}