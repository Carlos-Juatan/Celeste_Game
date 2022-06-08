using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {

#region Var
        [Header("Jump Up")]
        [SerializeField] float _jumpSpeed = 26f; // 12 frames = 28f | 10 frames = 26f

        [Header("Jump Reduce")]
        [SerializeField] float _reduceMultiplier = 1.8f; // 12 frames = 2f | 10 frames = 1.8f
        [SerializeField] float _releaseReduceMult = 8f; // 12 frames = 8f | 10 frames = 26f
        [SerializeField] float _maxReduceSpeed = 26f; // 12 frames = 28f | 10 frames = 26f
        [SerializeField] float _minStayTime = 0.05f;

        Vector2 _currentVelocity;
        float _currentReduce;
        float _minStayTimer;
        //bool _resetJumpTimer = false;
        bool _holdingJumpBonus;
#endregion

#region Getters and Setters
        //public bool ResetJumpTimer { get { return _resetJumpTimer; } set { _resetJumpTimer = value; } }
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
            // If Rigidbody Vertical velocity less equal than zero switch to fall state
            //if(_currentVelocity.y <= 0){ // Applaying vertical calculations as apex maybe
            if(_player.Data.Rigidbody2D.velocity.y < 0f && _minStayTimer <= 0f){
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Fall));
                

                // Verify if jump input stay pressed on switch to jump state
                PlayerFallState fallState = _player.Data.Factory.SelectState(PlayerStates.Fall) as PlayerFallState;
                fallState.HoldingJumpBonus = _holdingJumpBonus;
                SwitchState(fallState);
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

            // If Input jump has pressed
            if(hasPressed){

                // If can wall jump switch to wall jump
                if(_player.Data.PlayerPhysics.WallJumpCheckDetection()){
                    SwitchState(_player.Data.Factory.SelectState(PlayerStates.WallJump));

                // Else if can jump start a new jump
                }else if(_player.Data.CurrentJumpCount > 0){
                    StatJump();
                    _holdingJumpBonus = true;
                }

            // Else if jump input is release
            }else{
                // If jump input release cancel the jump.
                CancelJump();
                _holdingJumpBonus = false;
            }
        }
#endregion

#region Jump
        // Execute a new jump
        void StatJump(){
            _currentVelocity.y = _jumpSpeed;
            _currentReduce = _reduceMultiplier;
            _minStayTimer = _minStayTime;
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