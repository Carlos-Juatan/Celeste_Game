using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerWallJumpState : PlayerBaseState, IRootState
    {

#region Var
        //Vector2 _currentVelocity;
        //float _currentReduce;
        float _minStayTimer;
        //bool _cancelJumpOrder = false;
        bool _holdingJumpBonus;
#endregion

#region Getters and Setters
        //public bool CancelJumpOrder { get { return _cancelJumpOrder; } set { _cancelJumpOrder = value; } }
#endregion

#region Constructor.
        public PlayerWallJumpState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            StartWallJump();

            // Set Wall Jump animation, maybe the same jump animation in this case
            _player.Data.PlayerAnimations.StartWallJump();
        }

        // Don't Starts with a sub-state because, the player can't control the movement of the wall jump for a little time.
        protected override void InitializeSubState(){}
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
            if((/*_cancelJumpOrder &&*/ _minStayTimer <= 0f) || !_player.Data.PlayerPhysics.RoofEdgeDetection()){
                CancelWallJump();
            }

            // Run Jump
            ExecuteWallJump();
        }
#endregion

#region Exiting States
        protected override void ExitState(){
            // Finish the Wall Jump animation
            _player.Data.PlayerAnimations.EndWallJump();
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){

            // If jump input is pressed
            if(hasPressed){

                // If can wall jump Execute a new wall jump
                if(_player.Data.PlayerPhysics.WallJumpCheckDetection()){
                    _holdingJumpBonus = true;
                    StartWallJump();

                // Else if can jump Switch to the jump state
                }else if(_player.Data.CurrentJumpCount > 0){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.Jump));
                }

            }else{
                // If jump input release cancel the jump.
                CancelWallJump();
            }
        }
#endregion

#region Jump
        void StartWallJump(){

        }

        // Execute a new jump
        void ExecuteWallJump(){
        
        }

        void CancelWallJump(){

        }

#endregion

    }
}