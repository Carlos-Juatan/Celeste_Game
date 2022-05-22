using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {
#region Constructor.
        public PlayerJumpState(PlayerController currentPlayer) : base (currentPlayer){
            IsRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            HandleGravity();

            Debug.Log("Player Jump");
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
            // if player is grounded and jump is pressed, switch to jump state
            //if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress){
            //    SwitchState(Factory.Jump());
            //} else if (!Ctx.CharacterController.isGrounded){
            //    SwitchState(Factory.Fall());
            //}
        }
#endregion

    }
}