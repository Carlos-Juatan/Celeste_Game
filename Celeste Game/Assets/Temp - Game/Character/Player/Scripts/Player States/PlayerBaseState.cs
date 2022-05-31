using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerBaseState
    {
#region Var
        // States
        protected bool _isRootState = false;
        protected PlayerController _player;
        PlayerBaseState _currentSuperState;
        PlayerBaseState _currentSubState;
#endregion

#region Constructor
        public PlayerBaseState(PlayerController currentPlayer){
            _player = currentPlayer;
        }
#endregion

#region Stating States
        public void EnterStates(){
            InitializeSubState();
            EnterState();
        }

        protected virtual void InitializeSubState(){}

        protected virtual void EnterState(){}
#endregion

#region Updating States
        public void UpdateStates(){
            UpdateState();
            if(_currentSubState != null){
                _currentSubState.UpdateStates();
            }
            CheckSwitchStates(); // Put on the bottom of all updates to check if switch on the final of the all.
        }

        protected virtual void UpdateState(){}

        protected virtual void CheckSwitchStates(){}
#endregion

#region Physics Calculating States
        public void FisicsCalculateStates(){
            FisicsCalculateState();
            if(_currentSubState != null){
                _currentSubState.FisicsCalculateState();
            }
        }

        protected virtual void FisicsCalculateState(){}
#endregion

#region Exiting States
        protected virtual void ExitState(){}
#endregion

#region Switching States
        protected void SwitchState(PlayerBaseState newState){
            // current state exits state
            ExitState();

            // new state enters state
            newState.EnterStates();

            if(_isRootState){
                // switch current state of context
                _player.Data.CurrentState = newState;
            } else if (_currentSuperState != null){
                // set the current super states sub state to the new state
                _currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSubState(PlayerBaseState newSubState){
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }

        protected void SetSuperState(PlayerBaseState newSuperState){
            _currentSuperState = newSuperState;
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public virtual void JumpingInput(bool hasPressed){}
#endregion

    }
}