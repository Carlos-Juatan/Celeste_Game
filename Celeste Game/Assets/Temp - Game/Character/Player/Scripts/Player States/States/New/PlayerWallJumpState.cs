using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerWallJumpState : PlayerBaseState, IRootState
    {

#region Var
    float _currentTimer;
#endregion

#region Getters and Setters
#endregion

#region Constructor.
        public PlayerWallJumpState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            ExecuteJump();

            // Set Wall Jump animation, maybe the same jump animation in this case
            _player.Data.PlayerAnimations.StartJump();
        }

        // Don't Starts with a sub-state because, the player can't control the movement of the wall jump for a little time.
        protected override void InitializeSubState(){}
#endregion

#region Updating.
        protected override void UpdateState(){
            _currentTimer -= Time.deltaTime;
        }

        protected override void CheckSwitchStates(){
            // If Rigidbody Vertical velocity less equal than zero switch to fall state
            if(_player.Data.Rigidbody2D.velocity.y < 0f || _currentTimer <= 0f){
                CancelJump();
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Fall));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){}
#endregion

#region Exiting States
        protected override void ExitState(){}
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            if(!hasPressed){
                // If jump input release cancel the jump.
                //CancelJump();
            }
            else if(_player.Data.CurrentJumpCount > 0){
                // If jump input is pressed start a new jump
                ExecuteJump();
            }
        }
#endregion

#region Jump
        // Execute a new jump
        void ExecuteJump(){
            
            if(_player.Data.RightWallJump){
                _player.Data.FacingDirection = -1;

            }else if(_player.Data.LeftWallJump){
                _player.Data.FacingDirection = 1;
            }
            _player.Data.OnWallJump = true;

            Vector2 force = Vector2.zero;
            force.x = _player.Data.WallJumpForce * _player.Data.FacingDirection;
            force.y = _player.Data.WallJumpForce;

            _player.Data.Rigidbody2D.velocity = Vector2.zero;

            _player.Data.Rigidbody2D.AddForce(force, ForceMode2D.Impulse);

            _currentTimer = _player.Data.WallJumpTime;
        }

        void CancelJump(){

            _player.Data.OnWallJump = false;

            _player.Data.Rigidbody2D.velocity = Vector2.zero;
        }

#endregion

    }
}