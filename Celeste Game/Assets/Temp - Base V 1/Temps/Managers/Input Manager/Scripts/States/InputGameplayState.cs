using System.Collections.Generic;
using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class InputGameplayState : InputBaseState 
    {
        PosAxisIntInput _axisIntInput = new PosAxisIntInput();
        InputUse axisUseInput = new InputUse();

        Dictionary<KeyCode, BaseInputs> inputs = new Dictionary<KeyCode, BaseInputs>(){
            {KeyCode.Escape, new PauseInput()},
            {KeyCode.Space, new JumpInput()}
        };

        public override void EnterState()
        {
            _axisIntInput.EnterInput();

            foreach (var key in inputs.Keys){
                inputs[key].EnterInput();
            }
        }

        public override void UpdateInputs(Event e)
        {
            // Send the Vertical and Horizontal axis.
            axisUseInput.SetValue((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
            _axisIntInput.UseInput(axisUseInput);

            // Check if input type is Keyboard or Gamepad and update the input type
            //if (e.type == EventType.KeyDown){
            if(e.isKey || e.isMouse){

                // Check if have a path of the input Down.
                if(Input.GetKeyDown(e.keyCode))
                    if(inputs.ContainsKey(e.keyCode))
                        inputs[e.keyCode].UseInput(null);
            }
        }

        public override void ExitState()
        {
            foreach (var key in inputs.Keys){
                inputs[key].ExitInput();
            }
        }
    }
}