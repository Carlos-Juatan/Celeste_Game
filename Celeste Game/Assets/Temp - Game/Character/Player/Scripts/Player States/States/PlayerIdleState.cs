using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
#region Constructor.
        public PlayerIdleState(PlayerController currentPlayer) : base (currentPlayer){}
#endregion

#region Stating.
        protected override void EnterState(){
            // Set Idle Animation

            // Reset the moviment.

            Debug.Log("Player Idle");
        }
#endregion

#region Updating.
        protected override void CheckSwitchStates(){
            // If player is moving swicth state to MoveState
            if (Player.Data.IsMoving){
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Move));
            }
        }
#endregion
    }
}