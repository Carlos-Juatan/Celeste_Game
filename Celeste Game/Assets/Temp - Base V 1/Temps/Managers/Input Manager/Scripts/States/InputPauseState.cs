using System.Collections.Generic;
using UnityEngine;

namespace SystemManager.InputManagement
{
    public class InputPauseState : InputBaseState 
    {
        Dictionary<KeyCode, BaseInputs> inputs = new Dictionary<KeyCode, BaseInputs>(){
            {KeyCode.Escape, new ReturnPauseInput()}
        };

        public override void EnterState()
        {
            foreach (var key in inputs.Keys)
            {
                inputs[key].EnterInput();
            }
        }

        public override void UpdateInputs(Event e)
        {
            if(e.isKey){
                // Check if have a path of the input Down.
                if(Input.GetKeyDown(e.keyCode))
                    if(inputs.ContainsKey(e.keyCode))
                        inputs[e.keyCode].UseInput(new InputUse());
            }
        }

        public override void ExitState()
        {
            foreach (var key in inputs.Keys)
            {
                inputs[key].ExitInput();
            }
        }
    }
}