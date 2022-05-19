using UnityEngine;

namespace SystemManager.GameManagement
{
	public class GameBaseState
	{
		public delegate void StartBase();
		public event StartBase startBase;

		public delegate void UpdateBase();
		public event UpdateBase updateBase;

		public delegate void FixedUpdateBase();
		public event FixedUpdateBase fixedUpdateBase;

		public delegate void LateUpdateBase();
		public event LateUpdateBase lateUpdateBase;
		
#region Unity Methods.
		public virtual void StartState() { startBase?.Invoke(); }

		public virtual void UpdateState() { updateBase?.Invoke(); }

		public virtual void FixedUpdateState() { fixedUpdateBase?.Invoke(); }

		public virtual void LateUpdateState() { lateUpdateBase?.Invoke(); }

#endregion

#region Public Methods.
		public virtual void EnterState() { }
		public virtual void ExitState() {}
#endregion
	}
}