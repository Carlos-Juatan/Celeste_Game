using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
#region Var
        float _vertVelocity;
        float _coyoteTimer;
        float _jumpInputBuffer;
        bool _jumpInputHolding;
#endregion

#region Constructor
        public PlayerFallState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating
        protected override void EnterState(){

            // Verify if in the last frame was on the ground
            if(_player.Data.WasGrounded){
                _coyoteTimer = _player.Data.CoyoteTime;
            }

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
        protected override void UpdateState(){
            _coyoteTimer -= Time.deltaTime;
            _jumpInputBuffer -= Time.deltaTime;
        }

        protected override void CheckSwitchStates(){
            // If is grounded change to Grounded State.
            if(_player.Data.IsGrounded){

                // Verify if has pressed jump input before collider on the ground
                if(_jumpInputBuffer > 0){
                    _jumpInputBuffer = 0f;
                    _player.Data.CurrentJumpCount = _player.Data.JumpCount;

                    // Verify if jump input stay pressed on switch to jump state
                    PlayerJumpState jumpState = _player.Data.Factory.SelectState(PlayerStates.Jump) as PlayerJumpState;
                    jumpState.ResetJumpTimer = !_jumpInputHolding;
                    SwitchState(jumpState);
                }
                else{
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Grounded));
                }
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

            _jumpInputHolding = hasPressed;

            // if jump is pressed
            if(hasPressed){
                // Update Jump Input buffer
                _jumpInputBuffer = _player.Data.JumpInputBuffer;
                
                // if player can jump, switch to jump state
                if(_player.Data.CurrentJumpCount > 0 || _coyoteTimer > 0){
                    // Jump has pressed and can jump. Jump again.
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Jump));
                    // Reset Input buffer if Coyote time has used
                    _jumpInputBuffer = 0f;
                }
            }
        }
#endregion

    }
}