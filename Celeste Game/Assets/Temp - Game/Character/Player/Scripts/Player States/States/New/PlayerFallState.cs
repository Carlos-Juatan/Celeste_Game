using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
#region Var
        [Header("Fall")]
        [SerializeField] float _fallMultiplier = 2f;
        [SerializeField] float _maxFallSpeed = 30f;
        
        Vector2 _currentVelocity;
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
        protected override void UpdateState(){}

        protected override void CheckSwitchStates(){
            // If is grounded change to Grounded State.
            if(_player.Data.IsGrounded){

                // if jump input buffer start a new jump
                if(false){

                    // Input buffer =======================
                    


                // else Switch to ground state
                }else{
                    Debug.Log(_currentVelocity);
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

            // Applying Final Velocity
            _player.Data.Rigidbody2D.velocity = _currentVelocity;
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
        public override void JumpingInput(bool hasPressed){}
#endregion

    }
}