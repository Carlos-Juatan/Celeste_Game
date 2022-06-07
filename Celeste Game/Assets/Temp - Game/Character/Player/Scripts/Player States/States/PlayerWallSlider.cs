using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerWallSlider : PlayerBaseState, IRootState
    {
#region Var
        float _currentGravity;
        bool _wallSlider;

        Vector2 _fallVelocity;
#endregion

#region Constructor
        public PlayerWallSlider(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating
        protected override void EnterState(){

            _currentGravity = _player.Data.Rigidbody2D.gravityScale;
            _player.Data.Rigidbody2D.gravityScale = 0f;
            _player.Data.Rigidbody2D.velocity = Vector2.zero;

            _fallVelocity = Vector2.zero;
            _fallVelocity.y = -Mathf.Abs(_player.Data.WallSliderVelocity);

            // Run the player Wall Slider animation and effects
            _player.Data.PlayerAnimations.StartWallSlider();

            //Debug.Log("_player Wall Slider");
        }

        protected override void InitializeSubState(){}
#endregion

#region Updating
        protected override void UpdateState(){}

        protected override void CheckSwitchStates(){
            // If is grounded change to Grounded State.
            if(_player.Data.IsGrounded){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Grounded));

            }else{
                // Wall Slider
                if(!_wallSlider){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Fall));
                }
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){

            // Verifying wall slider
            _wallSlider = _player.Data.PlayerPhysics.WallSliderCheckDetection();

            // Applying fall velocity on wall
            _fallVelocity.x = _player.Data.Rigidbody2D.velocity.x;
            _player.Data.Rigidbody2D.velocity = _fallVelocity;
        }
#endregion

#region Exiting States
        protected override void ExitState(){

            _player.Data.Rigidbody2D.gravityScale = _currentGravity;

            // Ending the player Wall Slider animation and effects
            _player.Data.PlayerAnimations.EndWallSlider();

        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){

            // if jump is pressed
            if(hasPressed){
                // If can wall jump switch to wall jump
                if(_player.Data.PlayerPhysics.WallJumpCheckDetection()){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallJump));
                }
            }
        }
#endregion

    }
}