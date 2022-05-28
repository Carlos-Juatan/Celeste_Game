using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerJumpState : PlayerBaseState, IRootState
    {

#region Var
        float _jumpTimer;
        float _jumpForce;
        float _holdJumpTimer;
        float _curtInertia;
        bool _isJumping;
        bool _isHoldingJump;
        bool _jumpHasFinished;
#endregion

#region Constructor.
        public PlayerJumpState(PlayerController currentPlayer) : base (currentPlayer){
            _isRootState = true;
        }
#endregion

#region Stating.
        protected override void EnterState(){
            ExecuteJump();
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
             _jumpTimer -= Time.deltaTime;
             _holdJumpTimer -= Time.deltaTime;
        }

        protected override void CheckSwitchStates(){
            // If Rigidbody Vertical velocity less equal than zero switch to fall state
            //if(_currentVelocity.y <= 0){ // Applaying vertical calculations as apex maybe
            if(!_isJumping){
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Fall));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){
            if(_jumpTimer > 0){

                // Slow down the force of the jump.
                _jumpForce = Mathf.MoveTowards(_jumpForce, 0, _player.Data.JumpTime);

                // Apply the force of the jump
                _player.Data.Rigidbody2D.velocity = new Vector2(_player.Data.Rigidbody2D.velocity.x, _jumpForce);
            }
            else{
                // If jump has finished.
                if(!_jumpHasFinished){
                    // Invert to enter just once.
                    _jumpHasFinished = true;

                    // Set on Rigidbody velocity some inertia to stop the move slowly
                    float jumpInertia = _player.Data.Rigidbody2D.velocity.y / _curtInertia;
                    _player.Data.Rigidbody2D.velocity = new Vector2(_player.Data.Rigidbody2D.velocity.x, jumpInertia);

                    // If is holding the jump button use the celeste asset.
                    if(_isHoldingJump){
                        // celeste have a asset has set on the top of the jump half of gravity for some quickly time
                        _holdJumpTimer = _player.Data.HoldJumpTime;
                        _player.Data.Rigidbody2D.gravityScale = 0.5f;
                    }
                }

                // set gravity to default after this celeste asset.
                if(_holdJumpTimer < 0f){
                    _isHoldingJump = false;
                    _isJumping = false;
                    _player.Data.Rigidbody2D.gravityScale = 1f;
                }
            }
        }
#endregion

#region External Events Inputs
        // Called by InputManager envery time the Jump input has pressed or release
        public override void JumpingInput(bool hasPressed){
            if(!hasPressed){
                // If jump input release cancel the jump.
                CancelJump();
            }
            else if(_player.Data.CurrentJumpCount > 0){
                // If jump input is pressed start a new jump
                ExecuteJump();
            }
        }
#endregion

#region Jump
        // Execute a new jump
        void ExecuteJump(){
            _player.Data.CurrentJumpCount--;
            _jumpTimer = _player.Data.JumpTime;
            _jumpForce = _player.Data.JumpForce;
            _isJumping = true;
            _isHoldingJump = true;
            _jumpHasFinished = false;
            _curtInertia = _player.Data.CurtInertiaHightJump;
        }

        void CancelJump(){
            _curtInertia = _player.Data.CurtInertiaLowJump;
            _jumpTimer = 0;
            _isHoldingJump = false;
            _player.Data.Rigidbody2D.gravityScale = 1f;
        }

#endregion

    }
}