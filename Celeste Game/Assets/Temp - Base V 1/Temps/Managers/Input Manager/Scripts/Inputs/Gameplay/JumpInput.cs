using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class JumpInput : BaseValueInput 
    {
        public override void EnterInput()
        {
            inputBase += GameManager.instance.Data.PlayerController.JumpingInput;
        }
        
        public override void ExitInput()
        {
            inputBase -= GameManager.instance.Data.PlayerController.JumpingInput;
        }
    }
}