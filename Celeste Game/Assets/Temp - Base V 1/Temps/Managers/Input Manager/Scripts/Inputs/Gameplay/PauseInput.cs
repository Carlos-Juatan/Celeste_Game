using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class PauseInput : BaseSimpleInput 
    {
        public override void EnterInput()
        {
            inputBase += GameManager.instance.Data.GameplayUIController.ChangePauseStates;
        }
        
        public override void ExitInput()
        {
            inputBase -= GameManager.instance.Data.GameplayUIController.ChangePauseStates;
        }
    }
}