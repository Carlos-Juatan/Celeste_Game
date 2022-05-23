using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
#region Var.
        float _moveHSpeed;
#endregion

#region Constructor.
        public PlayerMoveState(PlayerController currentPlayer) : base (currentPlayer){}
#endregion

#region Stating.
        protected override void EnterState(){
            Player.Data.IsMoving = true;

            // Set Move Animation
            Player.Animator.SetBool(PlayerController.Anim_IsMoving, true);

            _moveHSpeed = 0;
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

            //Running and Friction
            float mult = Player.Data.IsGrounded ? 1 : Player.Data.AirMult;
            int moveDir = Player.Data.AxisInput.x;

            if(moveDir != 0){
                if(moveDir > 0 && _moveHSpeed <= Player.Data.MaxMoveSpeed){
                    _moveHSpeed += Player.Data.MoveAcceleration * mult;
                    _moveHSpeed = Mathf.Min(_moveHSpeed, Player.Data.MaxMoveSpeed);
                }
                else if (moveDir < 0 && _moveHSpeed >= Player.Data.MaxMoveSpeed * -1){
                    _moveHSpeed -= Player.Data.MoveAcceleration * mult;
                    _moveHSpeed = Mathf.Max(_moveHSpeed, Player.Data.MaxMoveSpeed * -1);
                }
            }
            else{
                if(_moveHSpeed == 0){
                    Player.Data.IsMoving = false;

                    // Set Move Animation false
                    Player.Animator.SetBool(PlayerController.Anim_IsMoving, false);
                }
                else if(_moveHSpeed > 0){
                    _moveHSpeed -= Player.Data.MoveReduce * mult;
                    _moveHSpeed = Mathf.Max(0, _moveHSpeed);
                }
                else{
                    _moveHSpeed += Player.Data.MoveReduce * mult;
                    _moveHSpeed = Mathf.Min(0, _moveHSpeed);
                }
            }

            Player.Data.Rigidbody2D.velocity = new Vector2(_moveHSpeed, Player.Data.Rigidbody2D.velocity.y);
        }

        void MoveForceCalc(float force){
            
        }
#endregion
    }
}