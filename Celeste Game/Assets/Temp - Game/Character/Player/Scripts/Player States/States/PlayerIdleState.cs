using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
#region Constructor
        public PlayerIdleState(PlayerController currentPlayer) : base (currentPlayer){}
#endregion

#region Stating
        protected override void EnterState(){
            // Set Idle Animation

        }
#endregion

#region Updating
        protected override void CheckSwitchStates(){
            // If player is moving swicth state to MoveState
            if (_player.Data.AxisInput.x != 0){
                SwitchState(_player.Data.Factory.SelectState(PlayerStates.Move));
            }
        }
#endregion
    }
}