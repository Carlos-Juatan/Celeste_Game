using UnityEngine;

namespace GameAssets.Player.Hierarchically
{
    public class PlayerFallState : PlayerBaseState, IRootState
    {
        public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory){
            IsRootState = true;
        }

        public override void EnterState(){
            InitializeSubState();
            Ctx.Animator.SetBool(Ctx.IsFallingHash, true);
        }

        public override void UpdateState(){
            HandleGravity();
        }

        public void HandleGravity(){
            float previousYvelocity = Ctx.CurrentMovimentY;
            Ctx.CurrentMovimentY = Ctx.CurrentMovimentY + Ctx.Gravity * Time.deltaTime;
            Ctx.AppliedMovimentY = Mathf.Max((previousYvelocity + Ctx.CurrentMovimentY) * .5f, -20.0f);
        }

        public override void ExitState(){
            Ctx.Animator.SetBool(Ctx.IsFallingHash, false);
        }

        public override void CheckSwitchStates(){
            // if player is grownded, switch to the grounded state
            if(Ctx.CharacterController.isGrounded){
                SwitchState(Factory.Grounded());
            }
        }

        public override void InitializeSubState(){
            if (!Ctx.IsMovimentPressed && !Ctx.IsRunPressed){
                SetSubState(Factory.Idle());
            } else if (Ctx.IsMovimentPressed && !Ctx.IsRunPressed){
                SetSubState(Factory.Walk());
            } else{
                SetSubState(Factory.Run());
            }
        }
    }
}