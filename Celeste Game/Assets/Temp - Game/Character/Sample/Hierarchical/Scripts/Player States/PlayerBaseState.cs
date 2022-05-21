namespace GameAssets.Player.Hierarchically
{
    public class PlayerBaseState
    {
        private bool _isRootState = false;
        private PlayerStateMachine _ctx;
        private PlayerStateFactory _factory;
        private PlayerBaseState _currentSuperState;
        private PlayerBaseState _currentSubState;

        protected bool IsRootState { get { return _isRootState; } set { _isRootState = value; } }
        protected PlayerStateMachine Ctx { get { return _ctx; } }
        protected PlayerStateFactory Factory { get { return _factory; } }

        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory){
            _ctx = currentContext;
            _factory = playerStateFactory;
        }

        public virtual void EnterState(){}

        public virtual void UpdateState() { }

        public virtual void FisicsCalculateState() { }

        public virtual void ExitState() { }

        public virtual void CheckSwitchStates() { }

        public virtual void InitializeSubState() { }

        public void UpdateStates(){
            UpdateState();
            if(_currentSubState != null){
                _currentSubState.UpdateStates();
            }
            CheckSwitchStates(); // Put on the bottom of all updates to check if switch on the final of the all.
        }

        public void ExitStates(){
            ExitState();
            if(_currentSubState != null){
                _currentSubState.ExitStates();
            }
        }

        protected void SwitchState(PlayerBaseState newState){
            // current state exits state
            ExitStates();

            // new state enters state
            newState.EnterState();

            if(_isRootState){
                // switch current state of context
                _ctx.CurrentState = newState;
            } else if (_currentSuperState != null){
                // set the current super states sub state to the new state
                _currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSuperState(PlayerBaseState newSuperState){
            _currentSuperState = newSuperState;
        }

        protected void SetSubState(PlayerBaseState newSubState){
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}