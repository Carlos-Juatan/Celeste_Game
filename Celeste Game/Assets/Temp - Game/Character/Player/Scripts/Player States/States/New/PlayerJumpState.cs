using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {

#region Var
        [Header("Jump Up")]
        [SerializeField] float _jumpSpeed = 28f;

        [Header("Jump Reduce")]
        [SerializeField] float _reduceMultiplier = 2f;
        [SerializeField] float _releaseReduceMult = 8f;
        [SerializeField] float _maxReduceSpeed = 28f;

        Vector2 _currentVelocity;
        float _currentReduce;
        bool _resetJumpTimer = false;
#endregion

#region Getters and Setters
        public bool ResetJumpTimer { get { return _resetJumpTimer; } set { _resetJumpTimer = value; } }
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
        protected override void UpdateState(){}

        protected override void CheckSwitchStates(){
            // If Rigidbody Vertical velocity less equal than zero switch to fall state
            //if(_currentVelocity.y <= 0){ // Applaying vertical calculations as apex maybe
            if(_player.Data.Rigidbody2D.velocity.y < 0f){
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Fall));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){
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
            if(!hasPressed){
                // If jump input release cancel the jump.
                CancelJump();

            // Else if jump input is pressed
            }else{

                // If can wall jump switch to wall jump
                if(_player.Data.PlayerPhysics.WallJumpCheckDetection()){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallJump));

                // Else if can jump start a new jump
                }else if(_player.Data.CurrentJumpCount > 0){
                    StatJump();
                }
            }
        }
#endregion

#region Jump
        // Execute a new jump
        void StatJump(){
            _currentVelocity.y = _jumpSpeed;
            _currentReduce = _reduceMultiplier;
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
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, -_maxReduceSpeed);
        }

        void CancelJump(){
            _currentReduce = _releaseReduceMult;
        }

#endregion

    }
}