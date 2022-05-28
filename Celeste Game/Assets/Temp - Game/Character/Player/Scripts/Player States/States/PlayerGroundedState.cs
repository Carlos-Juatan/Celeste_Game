using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerGroundedState : PlayerBaseState, IRootState
    {
#region Constructor
        public PlayerGroundedState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating
        protected override void EnterState(){

            _player.Data.CurrentJumpCount = _player.Data.JumpCount;

            //Debug.Log("_player Grounded");
        }

        protected override void InitializeSubState(){
            // If player is moving switch sub state to PlayerMoveState else switch sub state to PlayerIdleState
            if (_player.Data.IsMoving){
                SetSubState(_player.Data.Factory.SelectState(PlayerStates.Move));
            }
            else{
                SetSubState(_player.Data.Factory.SelectState(PlayerStates.Idle));
            }
        }
#endregion

#region Updating
        protected override void CheckSwitchStates(){
            // if player isn't grounded , switch to fall state
            if(!_player.Data.IsGrounded){
                // Adding Coyote time
                _player.Data.CurrentJumpCount--;
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Fall));
            }
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            if(hasPressed && _player.Data.CurrentJumpCount > 0){
                // Jump has pressed and can jump. Switch to JumpState
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Jump));
            }
        }
#endregion

    }
}