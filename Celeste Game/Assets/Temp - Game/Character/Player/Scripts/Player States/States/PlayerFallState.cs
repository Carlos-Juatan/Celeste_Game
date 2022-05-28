using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
#region Var
        float _vertVelocity;
#endregion

#region Constructor
        public PlayerFallState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating
        protected override void EnterState(){

            //Debug.Log("_player Fall");
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
            // If is grounded change to Grounded State.
            if(_player.Data.IsGrounded){
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Grounded));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){
            // Fall faster and allow small jumps. _jumpVelocityFalloff is the point at which we start adding extra gravity. Using 0 causes floating
            if (_player.Data.Rigidbody2D.velocity.y < _player.Data.JumpVelocityFalloff){
                _vertVelocity = _player.Data.Rigidbody2D.velocity.y + (_player.Data.FallMultiplier * Physics.gravity.y);
                _player.Data.Rigidbody2D.velocity = new Vector2(_player.Data.Rigidbody2D.velocity.x, _vertVelocity);
            }
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            // if player can jump and jump is pressed, switch to jump state
            if(hasPressed && _player.Data.CurrentJumpCount > 0){
                // Jump has pressed and can jump. Jump again.
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Jump));
            }
        }
#endregion

    }
}