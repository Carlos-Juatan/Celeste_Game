using UnityEngine;

namespace GameAssets.Player.Hierarchically
{
    public class PlayerGroundedState : PlayerBaseState, IRootState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory){
            IsRootState = true;
        }

        public void HandleGravity(){
            Ctx.CurrentMovimentY = Ctx.Gravity;
            Ctx.AppliedMovimentY = Ctx.Gravity;
        }

        public override void EnterState(){
            InitializeSubState();
            HandleGravity();
        }

        public override void UpdateState(){}

        public override void ExitState() { }

        public override void InitializeSubState(){
            if (!Ctx.IsMovimentPressed && !Ctx.IsRunPressed){
                SetSubState(Factory.Idle());
            } else if (Ctx.IsMovimentPressed && !Ctx.IsRunPressed){
                //SetSubState(Factory.Walk());
            } else{
                //SetSubState(Factory.Run());
            }
        }

        public override void CheckSwitchStates(){
            // if player is grounded and jump is pressed, switch to jump state
            if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress){
                SwitchState(Factory.Jump());
            } else if (!Ctx.CharacterController.isGrounded){
                SwitchState(Factory.Fall());
            }
        }

    }
}