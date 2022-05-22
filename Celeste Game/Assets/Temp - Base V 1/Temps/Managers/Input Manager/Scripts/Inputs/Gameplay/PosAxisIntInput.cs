using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class PosAxisIntInput : BaseVector2IntInput 
    {
        public override void EnterInput()
        {
            vector2IntInputBase += GameManager.instance.Data.PlayerController.UpdateAxisInput;
        }
        
        public override void ExitInput()
        {
            vector2IntInputBase -= GameManager.instance.Data.PlayerController.UpdateAxisInput;
        }
    }
}