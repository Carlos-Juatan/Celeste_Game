using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
#region Var
        [Header("Fall")]
        [SerializeField] float _fallMultiplier = 3f; // 12 frames = 3f | 10 frames = 2f
        [SerializeField] float _maxFallSpeed = 20f; // 12 frames = 20f | 10 frames = 14f
        [SerializeField] float _holdingBonusTime = 0.1f;
        
        Vector2 _currentVelocity;
        float _currentFallMult;
        float _holdingBonusTimer;
        bool _holdingJumpBonus;
        bool _jumpInputBuffer;
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

            if(_holdingJumpBonus){
                _holdingBonusTimer = _holdingBonusTime;
            
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

            if(_holdingBonusTimer <= 0f){
                _holdingJumpBonus = false;
            }
        }

        protected override void CheckSwitchStates(){
            // If is grounded change to Grounded State.
            if(_player.Data.IsGrounded){

                // if jump input buffer start a new jump
                if(false){

                    // Input buffer =======================
                    


                // else Switch to ground state
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
            _currentVelocity.y -= _fallMultiplier;
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, -_maxFallSpeed);

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

            // If Input jump has pressed
            if(hasPressed){
                // Applying the higher half gravity bonus for a little time
                _jumpInputBuffer = true;

            // Else if jump input is release
            }else{
                // If jump input release cancel the jump half gravity bonus
                _jumpInputBuffer = false;
                _holdingJumpBonus = false;
            }
        }
#endregion

    }
}