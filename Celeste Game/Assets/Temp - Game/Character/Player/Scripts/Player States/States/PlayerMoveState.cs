using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
#region Var
        float _horVelocity;
#endregion

#region Constructor
        public PlayerMoveState(PlayerController currentPlayer) : base (currentPlayer){}
#endregion

#region Stating
        protected override void EnterState(){
            _player.Data.IsMoving = true;
            _horVelocity = _player.Data.Rigidbody2D.velocity.x;

            // Run the player Move animation and effects
            _player.Data.PlayerAnimations.StartMove();
        }
#endregion

#region Updating
        protected override void CheckSwitchStates(){
            // If player isn't moving swicth state to IdleState
            if (!_player.Data.IsMoving){
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Idle));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){

            // Detect Wall
            _player.Data.WallSliderInteratc = _player.Data.PlayerPhysics.WallSliderCheckDetection();

            //Running and with acceleration and friction
            float mult = _player.Data.IsGrounded ? 1 : _player.Data.AirMult;
            int moveDir = _player.Data.AxisInput.x;

            if(moveDir != 0){
                // Set horizontal move speed
                _horVelocity += moveDir * _player.Data.MoveAcceleration * mult;

                // clamped by max frame movement
                _horVelocity = Mathf.Clamp(_horVelocity, -_player.Data.MaxMoveSpeed, _player.Data.MaxMoveSpeed);
            }
            else{
                // No input. Let's slow the character down
                _horVelocity = Mathf.MoveTowards(_horVelocity, 0, _player.Data.MoveReduce * mult);

                if(_horVelocity == 0){
                    // If move has stoped. Let's set the movement to false.
                    _player.Data.IsMoving = false;
                }
            }

            // Apply on Rigidbody the final velocity;
            _player.Data.Rigidbody2D.velocity = new Vector2(_horVelocity, _player.Data.Rigidbody2D.velocity.y);
        }
#endregion

#region Exiting States
        protected override void ExitState(){

            // Set Wall slide false because have no horizontal inputs any more
            _player.Data.WallSliderInteratc = false;

            // Ending the player Move animation and effects
            _player.Data.PlayerAnimations.EndMove();

        }
#endregion

    }
}