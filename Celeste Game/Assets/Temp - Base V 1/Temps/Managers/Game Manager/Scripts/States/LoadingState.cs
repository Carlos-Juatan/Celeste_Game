using UnityEngine;
using SystemManager.InputManagement;

namespace SystemManager.GameManagement
{
	public class LoadingState : GameBaseState
	{
        public override void EnterState()
        {
            //startBase += GameManager.instance.Data.test.StartPause;
            //updateBase += GameManager.instance.Data.test.UpdatePause;
        }
        
		public override void ExitState()
        {
            //startBase -= GameManager.instance.Data.test.StartPause;
            //updateBase -= GameManager.instance.Data.test.UpdatePause;
        }
    }
}