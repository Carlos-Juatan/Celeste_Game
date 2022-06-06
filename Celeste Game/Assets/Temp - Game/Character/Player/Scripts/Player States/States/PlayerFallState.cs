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
        bool _fallingByPlatform;
        bool _wallSlider;
#endregion

#region Constructor
        public PlayerFallState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating
        protected override void EnterState(){

            // Verify if in the last frame was on the ground
            _fallingByPlatform = _player.Data.WasGrounded;
            if(_fallingByPlatform){
                _coyoteTimer = _player.Data.CoyoteTime;
            }

            // Run the player Fall animation and effects
            _player.Data.PlayerAnimations.StartFall();

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

            if(_coyoteTimer < 0f && _fallingByPlatform){
                _fallingByPlatform = false;
                _player.Data.CurrentJumpCount--;
            }
        }

        protected override void CheckSwitchStates(){
            // If is grounded change to Grounded State.
            if(_player.Data.IsGrounded){
                _coyoteTimer = 0f;

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
            }else{
                // Wall Slider
                if(_wallSlider){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallSlider));
                }
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){

            // Verifying wall slider
            _wallSlider = _player.Data.PlayerPhysics.WallSliderCheckDetection();

            // Fall faster and allow small jumps. _jumpVelocityFalloff is the point at which we start adding extra gravity. Using 0 causes floating
            if (_player.Data.Rigidbody2D.velocity.y < _player.Data.JumpVelocityFalloff){
                _vertVelocity = _player.Data.Rigidbody2D.velocity.y + (_player.Data.FallMultiplier * Physics.gravity.y);
                _player.Data.Rigidbody2D.velocity = new Vector2(_player.Data.Rigidbody2D.velocity.x, _vertVelocity);
            }
        }
#endregion

#region Exiting States
        protected override void ExitState(){

            if(!_wallSlider){
                // Ending the player Fall animation and effects
                _player.Data.PlayerAnimations.EndFall();
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
                    // Reset Input buffer and coyote time if Coyote time has used
                    _jumpInputBuffer = 0f;
                    _coyoteTimer = 0f;
                    // Jump has pressed and can jump. Jump again.
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Jump));
                }
            }
        }
#endregion

    }
}