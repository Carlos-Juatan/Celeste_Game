using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class ReturnMainMenuInput : BaseSimpleInput 
    {
        public override void EnterInput()
        {
            inputBase += GameManager.instance.Data.SimpleMainMenu.BackButton;
        }
        
        public override void ExitInput()
        {
            inputBase -= GameManager.instance.Data.SimpleMainMenu.BackButton;
        }
    }
}