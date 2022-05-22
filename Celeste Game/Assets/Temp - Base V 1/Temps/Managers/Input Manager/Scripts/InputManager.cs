using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemManager.InputManagement
{
    public class InputManager : MonoBehaviour
    {
        InputBaseState _currentState;
        public InputMainMenuState _InputMainMenuState = new InputMainMenuState();
        public InputGameplayState _InputGameplayState = new InputGameplayState();
        public InputPauseState _InputPauseState = new InputPauseState();

        public void OnGUI() { if(_currentState != null) { _currentState.UpdateInputs(Event.current); } }

        public void SwitchState(InputBaseState newState)
        {
            if(_currentState != null)
                _currentState.ExitState();

            _currentState = newState;
            
            string stateName = newState == null ? "Null" : newState.GetType().Name;
			GameDebug.Debug(DebugType.InpuManagerOnly, "["+this.name+"] Change InputState for " + stateName);

            if(_currentState != null)
                _currentState.EnterState();
        }
    }
}