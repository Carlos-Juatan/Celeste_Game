using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
#region Constructor.
        public PlayerFallState(PlayerController currentPlayer) : base (currentPlayer){
            IsRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            HandleGravity();

            //Debug.Log("Player Fall");
        }

        public void HandleGravity(){}

        protected override void InitializeSubState(){
            // If player is moving switch sub state to PlayerMoveState else switch sub state to PlayerIdleState
            if (Player.Data.IsMoving){
                SetSubState(Player.Data.Factory.SelectState(PlayerStates.Move));
            }
            else{
                SetSubState(Player.Data.Factory.SelectState(PlayerStates.Idle));
            }
        }
#endregion

#region Updating.
        protected override void CheckSwitchStates(){
            // If is grounded change to Grounded State.
            if(Player.Data.IsGrounded){
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Grounded));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){
            // Fall faster and allow small jumps. _jumpVelocityFalloff is the point at which we start adding extra gravity. Using 0 causes floating
            if (Player.Data.Rigidbody2D.velocity.y < Player.Data.JumpVelocityFalloff){
                _velocity.y = Player.Data.Rigidbody2D.velocity.y + (Player.Data.FallMultiplier * Physics.gravity.y);
                Player.Data.Rigidbody2D.velocity = _velocity;
            }
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            // if player can jump and jump is pressed, switch to jump state
            if(hasPressed && Player.Data.CurrentJumpCount > 0){
                // Jump has pressed and can jump. Jump again.
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Jump));
            }
        }
#endregion

    }
}