namespace GameAssets.Player.Hierarchically
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory) {}

        public override void EnterState(){
            Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
            Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
            Ctx.AppliedMovimentX = 0;
        }

        public override void UpdateState() { }

        public override void ExitState() { }

        public override void CheckSwitchStates(){
            if (Ctx.IsMovimentPressed && Ctx.IsRunPressed){
                SwitchState(Factory.Run());
            } else if (Ctx.IsMovimentPressed){
                SwitchState(Factory.Walk());
            }
        }

        public override void InitializeSubState() { }
    }
}