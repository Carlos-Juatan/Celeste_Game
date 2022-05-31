using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAssets.Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
#region Var
        [Header("Player Data"), SerializeField]
        PlayerData _data;
#endregion

#region Getters and Setters
        public PlayerData Data { get { return _data; } }
#endregion

#region Initializing
        // Defaut MonoBehaviour Function.
        void Awake(){
            _data.Initialize(this);
            _data.CurrentState = _data.Factory.SelectState(PlayerStates.Grounded);
            _data.CurrentState.EnterStates();
        }

        void Start() {
            
        }
#endregion

#region Updating
        // Called every frame by GameManager Gameplay State On Update Delegate..
        public void UpdatePlayer(){

            // Update all states
            _data.CurrentState.UpdateStates();

            // Update the player Animations.
            _data.PlayerAnimations.UpdateAnimations();

            // Update the physics with delay in the same frame after all.
            _data.PlayerPhysics.UpdatePhysicsAfterAll();
        }
#endregion

#region External Events Inputs
        // Called every frame by AxisInput of the InputManager.
        public void UpdateAxisInput(Vector2Int input) => _data.AxisInput = input;

        // Called by InputManager envery time the Jump input has pressed or release
        public void JumpingInput(bool hasPressed) => _data.CurrentState.JumpingInput(hasPressed);
#endregion

#region Physics Calculatings
        // Called on fixed frames by GameManager Gameplay State On FixedUpdate Delegate..
        public void FixedUpdatePlayer(){
            _data.PlayerPhysics.PhysicsCalculatings();
            
            _data.CurrentState.FisicsCalculateStates();
        }
#endregion
    }
}