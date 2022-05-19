using System.Collections.Generic;
using UnityEngine;
using SystemManager.GameManagement;

namespace SystemManager.InputManagement
{
    public class InputGameplayState : InputBaseState 
    {
        Dictionary<KeyCode, BaseInputs> inputs = new Dictionary<KeyCode, BaseInputs>(){
            {KeyCode.Escape, new PauseInput()},
            {KeyCode.Mouse0, new PosAxisInput()}
        };

        public override void EnterState()
        {
            foreach (var key in inputs.Keys){
                inputs[key].EnterInput();
            }
        }

        public override void UpdateInputs(Event e)
        {
            // Check if input type is Keyboard or Gamepad and update the input type
            //if (e.type == EventType.KeyDown){
            if(e.isKey || e.isMouse){

                // Send the mouse position on mouse0 click.
                if(Input.GetMouseButtonDown(0))
                    if(inputs.ContainsKey(KeyCode.Mouse0)){
                        Camera currentCamera = GameManager.instance.Data.MainCamera;
                        inputs[KeyCode.Mouse0].UseInput(new InputUse(currentCamera.ScreenToWorldPoint(Input.mousePosition)));
                    }

                // Check if have a path of the input Down.
                if(Input.GetKeyDown(e.keyCode))
                    if(inputs.ContainsKey(e.keyCode))
                        inputs[e.keyCode].UseInput(new InputUse());
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