using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerWallJumpState : PlayerBaseState, IRootState
    {

#region Var
        //[Header("Wall Jump")]
        //[SerializeField] float _player.Data.WallJumpSpeed = 26f;
        //[SerializeField] float _player.Data.MaxDistance = 14f;
        //[SerializeField] float _player.Data.ReduceWallJumpMultiplier = 1.8f;
        //[SerializeField] float _player.Data.ReleaseWallJumpReduceMult = 4f;
        //[SerializeField] float _player.Data.MinWallJumpStayTime = 0.1f;

        Vector2 _currentVelocity;
        float _currentReduce;
        float _minStayTimer;
        float _hForce;
        int _jumpDirection;
        bool _cancelJumpOrder = false;
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
            _player.Data.PlayerAnimations.StartWallJump(_jumpDirection);

            //Debug.Log("Enter Wall Jump");
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
                //fallState.SetSubState = false;
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
                CancelWallJump();
            }

            // Run Jump
            ExecuteWallJump();
        }
#endregion

#region Exiting States
        protected override void ExitState(){

            // Stop horizontal velocity if it's on idle state
            if(Mathf.Abs(_hForce) < _player.Data.MaxDistance){
                _currentVelocity.x = 0f;
                _currentVelocity.y = _player.Data.Rigidbody2D.velocity.y;
                _player.Data.Rigidbody2D.velocity = _currentVelocity;
            }

            // Turn off the Wall Interaction
            _player.Data.WallSliderInteract = false;

            // Finish the Wall Jump animation
            _player.Data.PlayerAnimations.EndWallJump(_jumpDirection);
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
                _holdingJumpBonus = false;
                _cancelJumpOrder = true;
            }
        }
#endregion

#region Jump
        void StartWallJump(){
            
            // Set Jump Direction
            if(_player.Data.RightWallJump){
                _jumpDirection = -1;

            }else if(_player.Data.LeftWallJump){
                _jumpDirection = 1;
            }

            // If start a new jump without a order of cancel the jump active the possible hold input bonus 
            if(!_cancelJumpOrder){
                _holdingJumpBonus = true;
            }
            
            _hForce = _jumpDirection * (_player.Data.AxisInput.x != 0 ? _player.Data.MaxDistance : _player.Data.MaxDistance / 2);
            _currentVelocity.x = _hForce;
            _currentVelocity.y = _player.Data.WallJumpSpeed;
            _currentReduce = _player.Data.ReduceWallJumpMultiplier;
            _minStayTimer = _player.Data.MinWallJumpStayTime;
        }

        // Execute a new jump
        void ExecuteWallJump(){
            // Calculate Horizontal Velocity =================================================
            //_currentVelocity.x = _player.Data.Rigidbody2D.velocity.x;

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

        void CancelWallJump(){
            _cancelJumpOrder = false;
            _currentReduce = _player.Data.ReleaseWallJumpReduceMult;
        }

#endregion

    }
}