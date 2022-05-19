using UnityEngine;

namespace SystemManager.InputManagement
{
    public abstract class InputBaseState 
    {
        public abstract void EnterState();
        public abstract void UpdateInputs(Event e);
        public abstract void ExitState();
    }
}