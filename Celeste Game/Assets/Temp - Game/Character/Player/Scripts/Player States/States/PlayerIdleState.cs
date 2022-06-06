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

            // Run the player Idle animation and effects
            _player.Data.PlayerAnimations.StartIdle();

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

#region Exiting States
        protected override void ExitState(){

            // Ending the player Move animation and effects
            _player.Data.PlayerAnimations.EndIdle();

        }
#endregion

    }
}