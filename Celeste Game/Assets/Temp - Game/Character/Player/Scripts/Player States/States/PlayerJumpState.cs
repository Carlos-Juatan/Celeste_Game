using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {

#region Var
        float _jumpHBoost = 0;
        float _shortJumpMult = 0.2f;
        Vector2 _currentVelocity;
#endregion

#region Constructor.
        public PlayerJumpState(PlayerController currentPlayer) : base (currentPlayer){
            IsRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            HandleGravity();

            ApllyJump();
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
        protected virtual void UpdateState(){}

        protected override void CheckSwitchStates(){
            // If Rigidbody Vertical velocity less equal than zero switch to fall state
            //if(_currentVelocity.y <= 0){ // Applaying vertical calculations as apex maybe
            if(Player.Data.Rigidbody2D.velocity.y <= 0){
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Fall));
            }
        }
#endregion

#region Physics Calculating States
        protected virtual void FisicsCalculateState(){

        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            if(!hasPressed){
                // If jump input release cancel the jump.
                CancelJump();
            }
        }
#endregion

#region Applying Jump

        void ApllyJump(){
            
            // Applying Jump
            Player.Data.CurrentJumpCount--;
            _currentVelocity.x = Player.Data.Rigidbody2D.velocity.x + _jumpHBoost;
            _currentVelocity.y = Player.Data.MaxJumpHeight;
            Player.Data.Rigidbody2D.velocity = _currentVelocity;
        }

        void CancelJump(){
            // Cancel Jump

            _currentVelocity.x = Player.Data.Rigidbody2D.velocity.x;
            _currentVelocity.y = Player.Data.Rigidbody2D.velocity.y * _shortJumpMult;
            Player.Data.Rigidbody2D.velocity = _currentVelocity;
            SwitchState(Player.Data.Factory.SelectState(PlayerStates.Fall));
        }

#endregion

    }
}