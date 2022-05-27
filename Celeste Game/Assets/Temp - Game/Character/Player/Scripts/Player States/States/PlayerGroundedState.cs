using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerGroundedState : PlayerBaseState, IRootState
    {
#region Constructor.
        public PlayerGroundedState(PlayerController currentPlayer) : base (currentPlayer){
            IsRootState = true;

            // Reset Vertical Velocity
            _velocity.y = 0;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            HandleGravity();

            Player.Data.CurrentJumpCount = Player.Data.JumpCount;

            //Debug.Log("Player Grounded");
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
            // if player isn't grounded , switch to fall state
            if(!Player.Data.IsGrounded){
                // Adding Coyote time
                Player.Data.CurrentJumpCount--;
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Fall));
            }
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            if(hasPressed && Player.Data.CurrentJumpCount > 0){
                // Jump has pressed and can jump. Switch to JumpState
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Jump));
            }
        }
#endregion

    }
}