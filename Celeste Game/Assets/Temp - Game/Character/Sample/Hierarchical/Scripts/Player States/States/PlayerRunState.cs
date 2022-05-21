namespace GameAssets.Player.Hierarchically
{
    public class PlayerRunState : PlayerBaseState
    {
        public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory) {}
        
        public override void EnterState(){
            Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
            Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
        }

        public override void UpdateState(){
            Ctx.AppliedMovimentX = Ctx.CurrentMovimentInput.x  * Ctx.MoveSpeed * Ctx.RunMultiplier;
        }
        public override void ExitState() { }

        public override void CheckSwitchStates(){
            if (!Ctx.IsMovimentPressed){
                SwitchState(Factory.Idle());
            } else if (Ctx.IsMovimentPressed && !Ctx.IsRunPressed){
                SwitchState(Factory.Walk());
            }
        }

        public override void InitializeSubState() { }
    }
}