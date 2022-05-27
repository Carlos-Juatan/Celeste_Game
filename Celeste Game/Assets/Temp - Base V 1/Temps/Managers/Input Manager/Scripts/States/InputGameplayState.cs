using System.Collections.Generic;
using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class InputGameplayState : InputBaseState 
    {
        PosAxisIntInput _axisIntInput = new PosAxisIntInput();

        InputUse _useInput = new InputUse();

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
            _useInput.SetValue((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
            _axisIntInput.UseInput(_useInput);

            // Check if input type is Keyboard or Gamepad and update the input type
            //if (e.type == EventType.KeyDown){
            if(e.isKey || e.isMouse){

                // Check if have a path of the input Down.
                if(Input.GetKeyDown(e.keyCode)){
                    if(inputs.ContainsKey(e.keyCode)){
                        _useInput.press = true;
                        inputs[e.keyCode].UseInput(_useInput);
                }}

                // Check if have a path of the input up.
                if(Input.GetKeyUp(e.keyCode)){
                    if(inputs.ContainsKey(e.keyCode)){
                        _useInput.press = false;
                        inputs[e.keyCode].UseInput(_useInput);
                }}
            }
        }

        public override void ExitState()
        {
            _axisIntInput.ExitInput();

            foreach (var key in inputs.Keys){
                inputs[key].ExitInput();
            }
        }
    }
}