using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {

#region Var
        Vector2 _currentVelocity;
        float _currentReduce;
        float _minStayTimer;
        bool _cancelJumpOrder = false;
        bool _holdingJumpBonus;
#endregion

#region Getters and Setters
        public bool CancelJumpOrder { get { return _cancelJumpOrder; } set { _cancelJumpOrder = value; } }
#endregion

#region Constructor.
        public PlayerJumpState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            StatJump();

            // Run the player jump animation and effects
            _player.Data.PlayerAnimations.StartJump();
        }

        protected override void InitializeSubState(){
            // If player is moving switch sub state to PlayerMoveState else switch sub state to PlayerIdleState
            if (_player.Data.IsMoving){
                SetSubState(_player.Data.Factory.SelectState(PlayerStates.Move));
            }
            else{
                SetSubState(_player.Data.Factory.SelectState(PlayerStates.Idle));
            }
        }
#endregion

#region Updating.
        protected override void UpdateState(){
            _minStayTimer -= Time.deltaTime;
        }

        protected override void CheckSwitchStates(){
            // If Rigidbody Vertical velocity less than zero switch to fall state
            //(If commpare equal zero, the corner correction don't works right)
            if(_player.Data.Rigidbody2D.velocity.y < 0f && _minStayTimer <= 0f){

                // Verify if jump input stay pressed on switch to jump state
                PlayerFallState fallState = _player.Data.Factory.SelectState(PlayerStates.Fall) as PlayerFallState;
                fallState.HoldingJumpBonus = _holdingJumpBonus;
                SwitchState(fallState);
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){

            // If collider with a roof, and can't make the corner correction, cancel the jump
            bool collideWithRoof = _player.Data.PlayerPhysics.RoofEdgeDetection();

            // Cancel the jump if have a orer to cancel and have pass the min time to stay on jump or if collider with a roof
            if((_cancelJumpOrder && _minStayTimer <= 0f) || !collideWithRoof){
                CancelJump();

                // Se Bateu a cabeça, talvez corrija a coroutine de modificação da queda, pra nn atravessar o chao.
            }

            // Run Jump
            ExecuteJump();
        }
#endregion

#region Exiting States
        protected override void ExitState(){
            // Ending the player jump animation and effects
            _player.Data.PlayerAnimations.EndJump();
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){

            // If Input jump has pressed
            if(hasPressed){

                // If can wall jump switch to wall jump
                if(_player.Data.PlayerPhysics.WallJumpCheckDetection()){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallJump));

                // Else if can jump start a new jump
                }else if(_player.Data.CurrentJumpCount > 0){
                    _holdingJumpBonus = true;
                    StatJump();
                }

            // Else if jump input is release
            }else{
                // If jump input release cancel the jump.
                _holdingJumpBonus = false;
                _cancelJumpOrder = true;
            }
        }
#endregion

#region Jump
        // Execute a new jump
        void StatJump(){

            // If on the same frame has start a new jump was on ground
            if(_player.Data.WasGrounded){

                // If start a new jump without a order of cancel the jump active the possible hold input bonus 
                if(!_cancelJumpOrder){
                    _holdingJumpBonus = true;
            }}
            
            _player.Data.CurrentJumpCount--;
            _currentVelocity.y = _player.Data.JumpSpeed;
            _currentReduce = _player.Data.ReduceMultiplier;
            _minStayTimer = _player.Data.MinStayTime;
        }

        void ExecuteJump(){
            // Calculate Horizontal Velocity
            _currentVelocity.x = _player.Data.Rigidbody2D.velocity.x;

            // Calculate Vertical Velocity
            HundleGravity();

            // Applying Final Velocity
            _player.Data.Rigidbody2D.velocity = _currentVelocity;
        }

        void HundleGravity(){
            // If vertical velocity more than max fall velocity add volocity
            _currentVelocity.y -= _currentReduce;
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, -_player.Data.MaxReduceSpeed);
        }

        void CancelJump(){
            _cancelJumpOrder = false;
            _currentReduce = _player.Data.ReleaseReduceMult;
        }

#endregion

    }
}