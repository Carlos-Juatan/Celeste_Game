using System.Collections;
using UnityEngine;

namespace GameAssets.Player.Hierarchically
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {
        IEnumerator IJumpResetCoroutine(){
            yield return new WaitForSeconds(.5f);
            Ctx.JumpCount = 0;
        }

        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory){
            IsRootState = true;
        }
        
        public override void EnterState(){
            InitializeSubState();
            HandleJump();
        }

        public override void UpdateState(){
            HandleGravity();
        }

        public override void ExitState(){
            Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
            if(Ctx.IsJumpPressed){
                Ctx.RequireNewJumpPress = true;
            }
            Ctx.CurrentJumpResetRoutine = Ctx.StartCoroutine(IJumpResetCoroutine());
            //if(Ctx.JumpCount == 3){
                Ctx.JumpCount = 0;
                Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
            //}
        }

        public override void CheckSwitchStates(){
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

        void HandleJump(){
            if(Ctx.JumpCount < 3 && Ctx.CurrentJumpResetRoutine != null){
                Ctx.StopCoroutine(Ctx.CurrentJumpResetRoutine);
            }

            Ctx.IsGrounded = false;
            Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
            Ctx.IsJumping = true;
            Ctx.JumpCount += 1;
            Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
            Ctx.CurrentMovimentY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
            Ctx.AppliedMovimentY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
        }

        public void HandleGravity(){
            bool isFalling = Ctx.CurrentMovimentY <= 0.0f || !Ctx.IsJumpPressed;
            float fallMultiplier = 2.0f;
            float previousYvelocity = Ctx.CurrentMovimentY;

            if(isFalling){
                Ctx.CurrentMovimentY = Ctx.CurrentMovimentY + (Time.deltaTime * fallMultiplier * Ctx.JumpGravities[Ctx.JumpCount]);
                Ctx.AppliedMovimentY = Mathf.Max((previousYvelocity + Ctx.CurrentMovimentY) * .5f, -20.0f);
            } else {
                Ctx.CurrentMovimentY = Ctx.CurrentMovimentY + (Time.deltaTime * Ctx.JumpGravities[Ctx.JumpCount]);
                Ctx.AppliedMovimentY = (previousYvelocity + Ctx.CurrentMovimentY) * .5f;
            }
        }
    }
}