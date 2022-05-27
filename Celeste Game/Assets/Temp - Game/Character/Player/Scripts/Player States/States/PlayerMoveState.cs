using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
#region Var.
        
#endregion

#region Constructor.
        public PlayerMoveState(PlayerController currentPlayer) : base (currentPlayer){}
#endregion

#region Stating.
        protected override void EnterState(){
            Player.Data.IsMoving = true;

            // Set Move Animation
            Player.Animator.SetBool(PlayerController.Anim_IsMoving, true);

            _velocity.x = 0;
        }
#endregion

#region Updating.
        protected override void CheckSwitchStates(){
            // If player isn't moving swicth state to IdleState
            if (!Player.Data.IsMoving){
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Idle));
            }
        }
#endregion

#region Physics Calculating States
        protected override void FisicsCalculateState(){

            //Running and with acceleration and friction
            float mult = Player.Data.IsGrounded ? 1 : Player.Data.AirMult;
            int moveDir = Player.Data.AxisInput.x;

            if(moveDir != 0){
                // Set horizontal move speed
                _velocity.x += moveDir * Player.Data.MoveAcceleration * mult;

                // clamped by max frame movement
                _velocity.x = Mathf.Clamp(_velocity.x, -Player.Data.MaxMoveSpeed, Player.Data.MaxMoveSpeed);

                /* TarodevController script function
                // Apply bonus at the apex of a jump
                var apexBonus = Mathf.Sign(Input.X) * _apexBonus * _apexPoint;
                _currentHorizontalSpeed += apexBonus * Time.deltaTime;
                */
            }
            else{
                // No input. Let's slow the character down
                _velocity.x = Mathf.MoveTowards(_velocity.x, 0, Player.Data.MoveReduce * mult);

                if(_velocity.x == 0){
                    // If move has stoped. Let's set the movement to false.
                    Player.Data.IsMoving = false;

                    // Set Move Animation to false
                    Player.Animator.SetBool(PlayerController.Anim_IsMoving, false);
                }
            }

            // Apply on Rigidbody the final velocity;
            Player.Data.Rigidbody2D.velocity = new Vector2(_velocity.x, Player.Data.Rigidbody2D.velocity.y);
        }

        void MoveForceCalc(float force){
            
        }
#endregion

    }
}