using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
#region Constructor.
        public PlayerMoveState(PlayerController currentPlayer) : base (currentPlayer){}
#endregion

#region Stating.
        protected override void EnterState(){
            // Set Idle Animation

            Debug.Log("Player Move");
        }
#endregion

#region Updating.
        protected override void UpdateState(){

            // Calcule the moviment.

        }

        protected override void CheckSwitchStates(){
            // If player isn't moving swicth state to IdleState
            if (!Player.Data.IsMoving){
                SwitchState(Player.Data.Factory.SelectState(PlayerStates.Idle));
            }
        }
#endregion
    }
}