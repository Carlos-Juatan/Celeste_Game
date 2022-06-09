using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
#region Var
        Vector2 _currentVelocity;
        float _coyoteTimer;
        float _currentFallMult;
        float _holdingBonusTimer;
        float _jumpInputBuffer;
        bool _holdingJumpBonus;
        bool _fallingByPlatform;
        bool _jumpInputHolding;
#endregion

#region Getters and Setters
        public bool HoldingJumpBonus { get { return _holdingJumpBonus; } set { _holdingJumpBonus = value; } }
#endregion

#region Constructor
        public PlayerFallState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating
        protected override void EnterState(){

            // Starting Current Velocity
            _currentVelocity = Vector2.zero;

            // Verify if in the last frame was on the ground
            _fallingByPlatform = _player.Data.WasGrounded;
            if(_fallingByPlatform){
                _coyoteTimer = _player.Data.CoyoteTime;
            }

            if(_holdingJumpBonus){
                _holdingBonusTimer = _player.Data.HoldingBonusTime;
            
            }else{
                _holdingBonusTimer = 0f;
            }

            // Run the player Fall animation and effects
            _player.Data.PlayerAnimations.StartFall();
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
            _holdingBonusTimer -= Time.deltaTime;
            _coyoteTimer -= Time.deltaTime;
            _jumpInputBuffer -= Time.deltaTime;

            if(_holdingBonusTimer <= 0f){
                _holdingJumpBonus = false;
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

                }else{
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Grounded));
                }

            // Else If interact with a wall
            }else{
                // Wall Slider
                if(_player.Data.WallSliderInteratc){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallSlider));
                }
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){

            // Calculate Horizontal velocity;
            _currentVelocity.x = _player.Data.Rigidbody2D.velocity.x;

            // If vertical velocity more than max fall velocity add volocity
            _currentVelocity.y -= _player.Data.FallMultiplier;
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, -_player.Data.MaxFallSpeed);

            // Applying the Holding jump bonus on higher of the jump for some time
            Vector2 jumpHalfGravityBonus = _currentVelocity;
            if(_holdingJumpBonus){
                jumpHalfGravityBonus.y /= 2f;
            }

            // Applying Final Velocity
            _player.Data.Rigidbody2D.velocity = jumpHalfGravityBonus;
        }
#endregion

#region Exiting States
        protected override void ExitState(){

            if(!_player.Data.WallSliderInteratc){
                // Ending the player Fall animation and effects if don't start wall slider.
                _player.Data.PlayerAnimations.EndFall();
            }

        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){

            _jumpInputHolding = hasPressed;

            // If Input jump has pressed
            if(hasPressed){

                // If can wall jump switch to wall jump
                if(_player.Data.PlayerPhysics.WallJumpCheckDetection()){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallJump));

                // Else if player can jump, switch to jump state
                }else if(_player.Data.CurrentJumpCount > 0 || _coyoteTimer > 0){
                    // Reset Input buffer and coyote time if Coyote time has used
                    _jumpInputBuffer = 0f;
                    _coyoteTimer = 0f;
                    // Jump has pressed and can jump. Jump again.
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Jump));

                }else{
                    // Update Jump Input buffer on falling to the ground
                    _jumpInputBuffer = _player.Data.JumpInputBuffer;
                }

            // Else if jump input is release
            }else{
                // If jump input release cancel the jump half gravity bonus
                _holdingJumpBonus = false;
            }
        }
#endregion

    }
}