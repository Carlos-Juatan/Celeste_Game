namespace GameAssets.Player.Hierarchically
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory) {}
        
        public override void EnterState(){
            Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
            Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        }

        public override void UpdateState(){
            Ctx.AppliedMovimentX = Ctx.CurrentMovimentInput.x * Ctx.MoveSpeed;
        }

        public override void ExitState() { }

        public override void CheckSwitchStates(){
            if (!Ctx.IsMovimentPressed){
                SwitchState(Factory.Idle());
            } else if (Ctx.IsMovimentPressed && Ctx.IsRunPressed){
                SwitchState(Factory.Run());
            }
            
        }

        public override void InitializeSubState() { }
    }
}